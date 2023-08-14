using RW.PMS.Application.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class Project_infoSearchInput : PagedQuery
    {
        public int Id { get; set; }

        public int pt_Company { get; set; }
        /// <summary>
        ///    项目名称
        /// </summary>
        public string pt_Name { get; set; }

      

        /// <summary>
        ///    项目号
        /// </summary>
        public string pt_Code { get; set; }
    }
}
