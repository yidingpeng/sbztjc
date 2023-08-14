using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.User;
using RW.PMS.Application.Contracts.Input.System;
using RW.PMS.Application.Contracts.Output.System;
using RW.PMS.Domain.Entities.System;

namespace RW.PMS.Application.Contracts.System
{
    public interface IUserService : ICrudApplicationService<UserEntity, int>
    {
        /// <summary>
        ///     用户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        LoginResultDto Login(LoginDto input);

        /// <summary>
        /// 注销登录
        /// </summary>
        void Logout();

        /// <summary>
        ///     获取token
        /// </summary>
        /// <returns></returns>
        LoginResultDto GetToken();

        UserinfoDto GetUserInfo();

        PersonalOutputDto GetPersonal();

        bool SavePersonal(PersonalInputDto personal);

        /// <summary>
        ///     查询用户分类数据
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        PagedResult<UserListDto> GetPagedList(UserSearchDto input);

        /// <summary>
        ///     查询指定角色的用户数据
        /// </summary>
        List<UserAppOutput> GetUserList(int roleId);

        /// <summary>
        ///     判断用户账号是否存在
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool AccountExist(UserDetailDto input);

        UserDetailDto GetModel(int id);

        bool Insert(UserDetailDto input);
        bool Update(UserListDto input);

        /// <summary>
        /// 修改指定账号的密码
        /// </summary>
        bool ResetPassword(ResetPwdDto input);

        /// <summary>
        /// 修改当前用户的密码
        /// </summary>
        bool ChangePassword(ChangePwdDto input);

        bool UploadAvatar(string base64File);
        List<UserListDto> GetUserListByIds(int[] ids);
        UserListDto GetUserInfoById(int id);

        /// <summary>
        /// 单点登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        LoginResultDto SsoLogin(SSOLoginDto input);


        /// <summary>
        /// 根据id获取用户电话
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserListDto GetUserTelById(int id);

        /// <summary>
        /// 通过账号查询用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        UserListDto GetUserTelByAccount(string account);

        /// <summary>
        /// 根据角色id获取用户信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<UserListDto> GetUserTelByroleId(int roleId);

        /// <summary>
        /// 查询当前登录人的信息
        /// </summary>
        /// <returns></returns>
        public UserListDto GetUserById();
    }
}