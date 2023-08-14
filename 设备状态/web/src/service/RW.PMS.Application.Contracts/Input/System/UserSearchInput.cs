using RW.PMS.Application.Contracts.DTO;

namespace RW.PMS.Application.Contracts.Input.System
{
    public class UserSearchInput : PagedQuery
    {
        /// <summary>
        ///     真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        ///     账号
        /// </summary>
        public string Account { get; set; }

    }
}
