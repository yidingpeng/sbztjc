using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using RW.PMS.CrossCutting.Extensions;

namespace RW.PMS.CrossCutting.Security.User
{
    /// <summary>
    ///     当前用户
    /// </summary>
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _accessor;

        public CurrentUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public bool IsAuthenticated
        {
            get
            {
                var isAuthenticated = _accessor?.HttpContext?.User?.Identity?.IsAuthenticated;
                return isAuthenticated ?? false;
            }
        }

        /// <summary>
        ///     用户标识
        /// </summary>
        public int Id
        {
            get
            {
                var id = _accessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return id.To<int>();
            }
        }

        /// <summary>
        ///     用户姓名
        /// </summary>
        public string RealName => _accessor?.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;

        /// <summary>
        ///     角色Id
        /// </summary>
        public int[] RoleId
        {
            get
            {
                var role = _accessor?.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
                if (role.NotNullOrWhiteSpace())
                {
                    return role.Split(',').Select(t => t.To<int>()).ToArray();
                }
                return Array.Empty<int>();
            }
        }

    }
}