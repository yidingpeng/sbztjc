using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Model.FileRequst
{
    /// <summary>
    /// 设备状态上传实体类
    /// </summary>
    public class RootRequest
    {
        /// <summary>
        /// 接口名。固定值:OPC_TO_MES_SEND_EQUIP_STATUS
        /// </summary>
        public string INTERFACE_ID { get; set; }
        /// <summary>
        /// 工厂代码。固定值:1010(Y)
        /// </summary>
        public string FACTORY { get; set; }
        /// <summary>
        /// 鉴权用户名。填写工厂MES定义的产线编码(Y)
        /// </summary>
        public string USER { get; set; }
        /// <summary>
        /// 鉴权密码。固定值:Qcmes.2021(Y)
        /// </summary>
        public string PASSWORD { get; set; }
        /// <summary>
        /// 作业员ID。设备触发填写MACHIEN(Y)
        /// </summary>
        public string TRAN_USER_ID { get; set; }
        /// <summary>
        /// 作业时间。数据格式:yyyyMMddhhmmss(Y)
        /// </summary>
        public string TRAN_TIME { get; set; }
        /// <summary>
        /// 详细的设备状态数据(Y)
        /// </summary>
        public RootData DATA { get; set; }
    }

    public class RootData
    {
        /// <summary>
        /// 设备归属产线ID(Y)
        /// </summary>
        public string LINE_ID { get; set; }
        /// <summary>
        /// 设备所在工序的文本码。当设备编码存在时，该字段无效。(N)
        /// </summary>
        public string OPER { get; set; }
        /// <summary>
        /// 设备编码,与设备台帐保持一致(Y)
        /// </summary>
        public string MACHINE_ID { get; set; }
        /// <summary>
        /// 设备状态变更事件(Y)
        /// FREE_01 产线关机
        /// MAIN_01 产品试制
        /// MAIN_02 功能调试
        /// MAIN_03 诊断改善
        /// MAIN_04 首件确认
        /// PROD_01 生产执行
        /// SCDN_01 例行检修/维护
        /// SCDN_02 休息
        /// SCDN_03 例行会议
        /// SCDN_04 产前准备
        /// SCDN_05 其他安排
        /// STAN_01 生产完工(等待)
        /// STAN_02 设备开机
        /// STAN_03 产中准备(换线换型)
        /// USCD_01 设备故障
        /// USCD_02 质量问题
        /// USCD_03 物料短缺
        /// USCD_04 工艺文件
        /// USCD_05 系统故障
        /// </summary>
        public string EVENT_ID { get; set; }
        /// <summary>
        /// 备注(N)
        /// </summary>
        public string COMMENT { get; set; }
    }
}
