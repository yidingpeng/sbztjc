namespace RW.PMS.Application.Contracts.Output.Message
{
	/// <summary>
    ///     消息内容输出实体
    /// </summary>
	public class MessageContentOutput
	{
		/// <summary>
		///     主键
		/// </summary>
		public int Id { get; set; }

        public string MessageType { get; set; }

		public string MessageLevel { get; set; }

        /// <summary>
        ///     提醒方式
        /// </summary>
        public string RemindWay { get; set; }

		/// <summary>
		///     消息标题
		/// </summary>
		public string Title { get; set; }
	
		/// <summary>
		///     Content
		/// </summary>
		public string Content { get; set; }
	}
}
