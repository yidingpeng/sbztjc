using System;
using System.Linq;

namespace RW.PMS.Application.Contracts.DTO.Project
{
	/// <summary>
	///      项目质保金信息表单实体
	/// </summary>
	public class ProjectRetMoneyDto
	{
		/// <summary>
		///     主键
		/// </summary>
		public int? Id { get; set; }

		/// <summary>
		///     交付信息单据号
		/// </summary>
		//public int DeliveryID { get; set; }

		/// <summary>
		///     项目编号
		/// </summary>
		public int ProjectID { get; set; }

		/// <summary>
		///     质保金比例
		/// </summary>
		public decimal RetMoneyProportion { get; set; }

		/// <summary>
		///     质保金金额
		/// </summary>
		public decimal RetMoneyAmount { get; set; }

		/// <summary>
		///     获取质保期限
		/// </summary>
		public object WarrantyPeriodObject { get; set; }
		/// <summary>
		///		质保期限
		/// </summary>
		public string WarrantyPeriod { get; set; }
		/// <summary>
		///     质保到期日
		/// </summary>
		public string ExpirationDate { get; set; }

		/// <summary>
		///     已到期质保金金额
		/// </summary>
		public decimal AlrExpirationMoney { get; set; }

		/// <summary>
		///     备注
		/// </summary>
		public string Remark { get; set; }
	}
}
