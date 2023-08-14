using RW.PMS.Application.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Input.BaseInfo
{
    public class Contract_InfoSearchInput: PagedQuery
    {

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ct_code { get; set; }

    }
}
