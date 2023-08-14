﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    public class MaterialSearchDto : PagedQuery
    {
        public int? Id { get; set; }

        /// <summary>
        ///     物料编码
        /// </summary>
        public string MtlCode { get; set; }
        public string MtlName { get; set; }
    }
}
