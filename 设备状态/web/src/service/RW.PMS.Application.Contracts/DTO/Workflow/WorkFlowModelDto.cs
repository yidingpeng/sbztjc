using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection;
using RW.PMS.CrossCutting.Exceptions;
using System.Text.Json.Nodes;
using RW.PMS.CrossCutting.Extender;
using RW.PMS.Application.Contracts.DTO.Workflow.Converter;

namespace RW.PMS.Application.Contracts.DTO.Workflow
{
    /*
     * 工作流实体描述：
     * 节点：共有4种节点，
     * 1、发起人 2、条件分支 3、审批人 4、抄送人
     * 
     * 审批人选择：
     * 1 指定人员 2 主管 3角色 4发起人自选 5发起人自己 7连续多级主管  
     * 
     * 多人审批方式：
     * 1、连续审批 2、会签 3、或签
     */

    public class WorkflowModelDto : Promoter
    {

    }

    /// <summary>
    /// 发起人节点，第一节点
    /// </summary>
    public class Promoter : BaseFlowModel, IRoleItem
    {
        public List<IdNameDto> NodeRoleList { get; set; }
    }

    /// <summary>
    /// 通用节点
    /// </summary>
    public class BaseFlowModel
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        public virtual int Type { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public BaseFlowModel ChildNode { get; set; }

        public static BaseFlowModel ToModel(string text)
        {
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new BaseFlowModelConverter());
            var data = JsonSerializer.Deserialize<BaseFlowModel>(text, options);

            return data;
        }

        public static string ToText(BaseFlowModel model)
        {
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new BaseFlowModelConverter());
            var data = JsonSerializer.Serialize(model);
            return data;
        }

        public static T FindNode<T>(BaseFlowModel node) where T : BaseFlowModel
        {
            if (node is T)
                return (T)node;
            if (node != null)
                return FindNode<T>(node.ChildNode);
            return null;
        }
    }

    /// <summary>
    /// 条件分支节点
    /// </summary>
    public class ConditionNode : BaseFlowModel
    {
        public override int Type { get => 4; }

        public List<ConditionItemNode> ConditionNodes { get; set; }
    }

    /// <summary>
    /// 条件分支项节点
    /// </summary>
    public class ConditionItemNode : BaseFlowModel
    {
        /// <summary>
        /// 节点类型，4表示条件分支
        /// </summary>
        public override int Type { get => 3; }
        /// <summary>
        /// 条件模式，1且，0或
        /// </summary>
        public int ConditionMode { get; set; }
        /// <summary>
        /// 优先级，数字越小越优先
        /// </summary>
        public int PriorityLevel { get; set; }
        /// <summary>
        /// 条件节点
        /// </summary>
        public List<Condition> ConditionList { get; set; }

        public override string ToString()
        {
            string str = ConditionList.ArrayToString();
            if (string.IsNullOrEmpty(str))
                str = "其他";
            return $"条件判断({str})";
        }
    }

    /// <summary>
    /// 组合条件
    /// 可组合成 x>y 类似的条件，
    /// 多个时，可按“或”、“且”进行串联
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 关联字段
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 操作方式
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Label}{Operator}{Value}";
        }

        public bool Execute(object obj)
        {
            if (Field == null) throw new LogicException($"[{ToString()}]未指定字段名。");
            object value1 = null;
            var detailProp = obj.GetType().GetProperty("Detail");
            if (detailProp != null)
            {
                if (detailProp.PropertyType == typeof(JsonNode))
                {
                    var node = detailProp.GetValue(obj) as JsonNode;
                    var nobj = node.AsObject();
                    if (nobj != null && nobj.ContainsKey(Field))
                        value1 = nobj[Field].ToString();
                }
                else
                {
                    var detail = detailProp.GetValue(obj);
                    var fieldProp = detailProp.GetType().GetProperty(Field, BindingFlags.IgnoreCase);
                    value1 = fieldProp.GetValue(detail);
                }
            }
            else
            {
                var prop = obj.GetType().GetProperty(Field, BindingFlags.IgnoreCase);
                if (prop == null) throw new LogicException(ExceptionCode.Failed, $"字段[{Field}]不存在，流程预处理失败。");
                value1 = prop.GetValue(obj);
            }
            return Compare(value1, Operator, Value);
        }

        bool Compare(object value1, string operate, object value2)
        {
            switch (operate)
            {
                case ">":
                    return value1.ToDecimal() > value2.ToDecimal();
                case ">=":
                    return value1.ToDecimal() >= value2.ToDecimal();
                case "<":
                    return value1.ToDecimal() < value2.ToDecimal();
                case "<=":
                    return value1.ToDecimal() <= value2.ToDecimal();
                case "=":
                    return Convert.ToString(value1) == Convert.ToString(value2);
                case "!=":
                    return Convert.ToString(value1) != Convert.ToString(value2);
                case "in":
                    return Convert.ToString(value1).Contains(Convert.ToString(value2));
                case "notin":
                    return !Convert.ToString(value1).Contains(Convert.ToString(value2));
                default:
                    throw new LogicException(ExceptionCode.Failed, $"无效的条件操作符{operate}");
            }
        }
    }

    /// <summary>
    /// 审核人节点
    /// </summary>
    public class ApprovideNode : BaseFlowModel, IUserItem, IRoleItem
    {
        public override int Type { get => 1; }

        /// <summary>
        /// 审批期限（0表示不生效）
        /// </summary>
        public int Term { get; set; }
        /// <summary>
        /// TermAuto==1后，自动审批后操作
        /// 0 自动通过 1 自动拒绝
        /// </summary>
        public int TermMode { get; set; }

        /// <summary>
        /// 是否超时自动审批
        /// </summary>
        public bool TermAuto { get; set; }

        /// <summary>
        /// 审批人类型
        /// 1 指定人员 2 主管 3角色 4发起人自选 5发起人自己 7连续多级主管  
        /// </summary>
        public int SetType { get; set; }

        public List<IdNameDto> NodeUserList { get; set; }
        public List<IdNameDto> NodeRoleList { get; set; }


        /// <summary>
        /// 审批方式
        /// 1 按顺序依次审批
        /// 2 会签
        /// 3 或签
        /// </summary>
        public int ExamineMode { get; set; }
        /// <summary>
        /// setType==2时，第几级主管
        /// </summary>
        public int ExamineLevel { get; set; }
        /// <summary>
        /// setType==7时，
        /// 0 直到最上层领导 1 自定义审批终点
        /// </summary>
        public int DirectorMode { get; set; }
        /// <summary>
        /// DirectorMode == 1 时，第几级主管
        /// </summary>
        public int DirectorLevel { get; set; }
        /// <summary>
        /// setType==4时，自选模式
        /// 1 自选一个人 2 自选多个人
        /// </summary>
        public int SelectMode { get; set; }
    }

    public class SendNode : BaseFlowModel, IUserItem
    {
        public override int Type { get => 2; }
        /// <summary>
        /// 允许发起人自选抄送人
        /// </summary>
        public bool UserSelectFlag { get; set; }
        public List<IdNameDto> NodeUserList { get; set; }
    }

    public interface IUserItem
    {
        List<IdNameDto> NodeUserList { get; set; }
    }

    public interface IRoleItem
    {
        List<IdNameDto> NodeRoleList { get; set; }
    }
}
