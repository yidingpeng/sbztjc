using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace RW.PMS.Application.Contracts.Input.System
{
    [Obsolete]
    public class RoleInput
    {
        public int? Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }


    }
}
