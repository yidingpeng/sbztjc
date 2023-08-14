using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.User
{
    public class UserOperationEditDto
    {
        /// <summary>
        ///     角色Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     操作编码
        /// </summary>
        public string[] OperationCode { get; set; }
    }
}
