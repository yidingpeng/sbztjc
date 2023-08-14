namespace RW.PMS.CrossCutting.Security.User
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }

        /// <summary>
        /// 用户标识
        /// </summary>
        int Id { get; }

        /// <summary>
        /// 姓名
        /// </summary>
        string RealName { get; }

        /// <summary>
        /// 角色Id
        /// </summary>
        int[] RoleId { get; }
    }
}