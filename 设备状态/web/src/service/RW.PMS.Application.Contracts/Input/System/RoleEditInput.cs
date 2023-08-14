using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Input.System
{
    [Obsolete]
    public class RoleEditInput : RoleInput
    {

        /// <summary>
        /// 权限树列表
        /// </summary>
        public string[] TreeArray { get; set; }
    }
}
