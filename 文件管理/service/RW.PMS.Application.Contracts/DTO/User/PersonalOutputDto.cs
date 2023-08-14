using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.User
{
    /// <summary>
    /// 个人中心 输出信息
    /// </summary>
    public class PersonalOutputDto
    {

        public PersonalUserDto UserInfo { get; set; }

        public PersonalInfoDto Personal { get; set; }
    }

    public class PersonalInfoDto
    {
        public string Avatar { get; set; }
        public string Fullname { get; set; }
        public string Sex { get; set; }

        public string Telnum { get; set; }
        public string Description { get; set; }
    }

    public class PersonalUserDto
    {
        public string Account { get; set; }
        public string Organization { get; set; }
        public DateTime LastLogin { get; set; }
        public int LoginCount { get; set; }
        public string Role { get; set; }
    }
}
