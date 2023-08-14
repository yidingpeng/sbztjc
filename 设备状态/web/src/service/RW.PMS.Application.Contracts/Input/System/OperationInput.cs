namespace RW.PMS.Application.Contracts.Input.System
{
    public class OperationInput
    {
        /// <summary>
        /// 所属模块
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 操作编码
        /// </summary>
        public string OperationCode { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
    }
}