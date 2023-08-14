namespace RW.PMS.Application.Contracts.Output.Message
{
	/// <summary>
    ///     消息配置输出实体
    /// </summary>
	public class MessageConfigOutput
	{
		/// <summary>
		///     主键
		/// </summary>
		public int Id { get; set; }
	
		/// <summary>
		///     消息编码
		/// </summary>
		public string MessageCode { get; set; }
	
		/// <summary>
		///     消息类型
		/// </summary>
		public string MessageType { get; set; }

        /// <summary>
		///     消息级别
		/// </summary>
		public string MessageLevel { get; set; }
	
		/// <summary>
		///     提醒间隔
		/// </summary>
		public int? RepeatInterval { get; set; }
	
		/// <summary>
		///     提醒间隔单位
		/// </summary>
		public string IntervalUnit { get; set; }
	
		/// <summary>
		///     提前预警时间
		/// </summary>
		public int? AdvanceTime { get; set; }
	
		/// <summary>
		///     提前预警时间单位
		/// </summary>
		public string AdvanceUnit { get; set; }
	
		/// <summary>
		///     重复次数
		/// </summary>
		public int? RepeatTimes { get; set; }

        /// <summary>
		///     消息目标
		/// </summary>
		public string MessageTarget { get; set; }
	
		/// <summary>
		///     提醒方式
		/// </summary>
		public string RemindWay { get; set; }
    }
}
