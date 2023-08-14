using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.User
{
    /// <summary>
    /// 登录
    /// </summary>
    public class SSOLoginDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string EncryptionKey { get; set; }
    }
}
