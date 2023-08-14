﻿using RW.PMS.Application.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class ProjectRetMoneySearchDto: PagedQuery
    {
        /// <summary>
		///     项目名称
		/// </summary>
		public string ProjectName { get; set; }

		/// <summary>
		/// 项目编号
		/// </summary>
		public string ProjectCode { get; set; }

		/// <summary>
		/// 质保期期限字符串
		/// </summary>
		public string WarrantyPeriodT { get; set; }
	}
}
