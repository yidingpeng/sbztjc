using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.BOM
{
    /// <summary>
    /// BOM状态码，字符串
    /// </summary>
    public static class BOMStatusDesc
    {
        /// <summary>
        /// 刚创建，未进行保存
        /// </summary>
        public const string Creating = nameof(Creating);

        //designing/Approving/Purchasing/completed
        public const string Destroied = nameof(Destroied);
        /// <summary>
        /// 已驳回
        /// </summary>
        public const string Rejected = nameof(Rejected);
        /// <summary>
        /// 设计中，未提交审核允许修改
        /// </summary>
        public const string Designing = nameof(Designing);
        /// <summary>
        /// 审批中
        /// </summary>
        public const string Approving = nameof(Approving);
        /// <summary>
        /// 采购中
        /// </summary>
        public const string Purchasing = nameof(Purchasing);
        /// <summary>
        /// 已完成
        /// </summary>
        public const string Completed = nameof(Completed);
        /// <summary>
        /// 审核中
        /// </summary>
        public const string Auditing = nameof(Auditing);

        public static StatusText GetDesc(object type)
        {
            var t = Convert.ToString(type);
            switch (t)
            {
                case Creating: return new StatusText(t, "新建的", Color.LightGray);
                case Destroied: return new StatusText(t, "废弃的", Color.IndianRed);
                case Designing: return new StatusText(t, "设计中", Color.LightGreen);
                case Rejected: return new StatusText(t, "已驳回", Color.LightPink);
                case Approving: return new StatusText(t, "审批中", Color.LightSalmon);
                case Purchasing: return new StatusText(t, "采购审批中", Color.LightYellow);
                case Completed: return new StatusText(t, "已完成", Color.SeaGreen);
                case Auditing: return new StatusText(t, "审核中", Color.LightGoldenrodYellow);
                default: return new StatusText("", $"未知[{type}]", Color.Red);
            }
        }

        public static List<StatusText> GetAll()
        {
            var arr = new string[] {
                nameof(Creating), nameof(Destroied), nameof(Designing),
                nameof(Rejected), nameof(Approving), nameof(Purchasing),
                nameof(Completed), nameof(Auditing)
            };
            List<StatusText> lst = arr.Select(x => GetDesc(x)).ToList();
            return lst;

        }
    }

    public struct StatusText
    {
        public StatusText(string key, string text)
        {
            this.Key = key;
            this.Text = text;
            this.Color = Color.Empty;
        }

        public StatusText(string key, string text, Color color)
        {
            this.Key = key;
            this.Text = text;
            this.Color = color;
        }

        public string Key { get; set; }

        public string Text { get; set; }
        public Color Color { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
