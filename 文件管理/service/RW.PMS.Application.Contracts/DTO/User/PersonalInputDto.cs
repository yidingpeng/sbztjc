using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.User
{
    /// <summary>
    /// 个人信息
    /// </summary>
    public class PersonalInputDto
    {
        public string Fullname { get; set; }

        public string Sex { get; set; }

        public DateTime Born { get; set; }

        public string Telnum { get; set; }

        public string Description { get; set; }

    }
}
