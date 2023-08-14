using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.DTO.User;
using RW.PMS.Application.Contracts.System;

namespace RW.PMS.API.Controllers
{
    public class PersonalCenterController : RWBaseController
    {
        IUserService _userSerivce;
        IOrganizationService _organizationService;
        public PersonalCenterController(IUserService userService, IOrganizationService organizationService)
        {
            _userSerivce = userService;
            _organizationService = organizationService;
        }
        /// <summary>
        /// 个人中心密码修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[HttpPut("Password")]
        //public IResponseDto DoUpdatePwd(UserListDto input)
        //{
        //    var result = _userSerivce.UpdatePassword(input);
        //    if (result)
        //    {
        //        return ResponseDto.Success("成功更新了密码。");
        //    }
        //    else 
        //    {
        //        return ResponseDto.Success("原密码错误，请重新输入");
        //    }
        //}
        /// <summary>
        /// 个人中心基本信息修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[HttpPut("PersonalMsg")]
        //public IResponseDto DoUpdateMsg(UserListDto input)
        //{
        //    var result = _userSerivce.UpdatePersonalMsg(input);
        //    if (result)
        //    {
        //        return ResponseDto.Success("基本信息修改成功！");
        //    }
        //    else
        //    {
        //        return ResponseDto.Success("网络错误，基本信息修改失败！");
        //    }
        //}

        /// <summary>
        /// 获取机构表机构部门信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("OrganizationName")]
        public IResponseDto GetOrganizationNameList()
        {
            var result = _organizationService.GetList();
            List<BaseCommonOutput> list = new List<BaseCommonOutput>();
            for (int i = 0; i < result.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = result[i].OrganizationName,
                    value = result[i].Id
                };
                list.Add(baseCommonOutput);
            }
            return ResponseDto.Success(list);
        }
    }
}
