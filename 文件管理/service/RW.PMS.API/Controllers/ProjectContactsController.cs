using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.Security.User;

namespace RW.PMS.API.Controllers
{
    public class projectcontactsController : RWBaseController
    {
        private readonly IProjectContactsService _projectContactsService;
        private readonly IUserService _userService;
        private readonly IProjectBasicsService _projectBasicsService;
        private readonly IDictionaryService _dictionaryService;
        private readonly IProjectDynamicService _projectDynamicService;
        public readonly ICurrentUser _CurrentUser;

        public projectcontactsController(IProjectContactsService projectContactsService, IUserService userService, ICurrentUser CurrentUser
            , IProjectDynamicService projectDynamicService, IProjectBasicsService projectBasicsService, IDictionaryService dictionaryService)
        {
            _projectContactsService = projectContactsService;
            _userService = userService;
            _projectBasicsService = projectBasicsService;
            _dictionaryService = dictionaryService;
            _projectDynamicService = projectDynamicService;
            _CurrentUser = CurrentUser;
        }
        /// <summary>
        /// 联系人分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("List")]
        public IResponseDto GetList([FromQuery] projectcontactsSearchDto input)
        {
            var result = _projectContactsService.GetPagedList(input);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 获取联系人表联系人
        /// </summary>
        /// <returns></returns>
        [HttpGet("Contact")]
        public IResponseDto GetContactList(string contactsName)
        {
            var result = _userService.GetList().Where(a=>a.Account.Contains(contactsName)).ToList();
            List<BaseCommonOutput> list = new List<BaseCommonOutput>();
            for (int i = 0; i < result.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = result[i].Account,
                    value = result[i].Id    
                };
                list.Add(baseCommonOutput);
            }
            return ResponseDto.Success(list);
        }


        /// <summary>
        /// 获取项目基础信息表项目名称
        /// </summary>
        /// <returns></returns>
        [HttpGet("Project")]
        public IResponseDto GetprojectList(string pName)
        {
            var result = _projectBasicsService.GetList().Where(a=>a.ProjectName.Contains(pName)).ToList();
            List<BaseCommonOutput> list = new List<BaseCommonOutput>();
            for (int i = 0; i < result.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = result[i].ProjectName,
                    value = result[i].Id
                };
                list.Add(baseCommonOutput);
            }
            return ResponseDto.Success(list);
        }
        /// <summary>
        /// 获取字典负责板块
        /// </summary>
        /// <returns></returns>
        [HttpGet("Plate")]
        public IResponseDto GetPlateList()
        {
            var result = _dictionaryService.GetSubItemList("FZBKId");
            List<BaseCommonOutput> list = new List<BaseCommonOutput>();
            for (int i = 0; i < result.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = result[i].DictionaryText,
                    value = result[i].Id
                };
                list.Add(baseCommonOutput);
            }
            return ResponseDto.Success(list);
        }
        /// <summary>
        /// 添加联系人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public IResponseDto DoAdd([FromBody] projectcontactsListDto input)
        {
            var IsExist = _projectContactsService.IsExist(input);
            if (IsExist)
            {
                return ResponseDto.Error(500, "已存在相同数据！");
            }
            var result = _projectContactsService.Insert(input);
            string content = "（新增项目团队）" + _CurrentUser?.RealName + "新增了项目编号为" +
                _projectBasicsService.GetList().Where(t => t.Id == input.PID).First().ProjectCode + "的项目团队，团队成员是：" +
                _userService.GetList().Where(t => t.Id == input.ContactsID).First().RealName + "，项目角色是：" +
                _dictionaryService.GetList().Where(t => t.Id == input.FZBKId).First().DictionaryText;
            _projectDynamicService.Insert(new ProjectDynamicView { dyType = 219, projectID = input.PID, operationContent = content });
            return ResponseDto.Success("添加成功！");
        }
        /// <summary>
        /// 修改联系人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public IResponseDto DoEdit([FromBody] projectcontactsListDto input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            var IsExist = _projectContactsService.IsExist(input);
            if (IsExist)
            {
                return ResponseDto.Error(500, "已存在相同数据！");
            }
            _projectContactsService.Update(input);
            return ResponseDto.Success("修改成功！");
        }
        /// <summary>
        /// 删除联系人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            foreach (var item in ids)
            {
                var ProId = _projectContactsService.GetList().Where(t => t.Id == item).First().PID;
                var ConId = _projectContactsService.GetList().Where(t => t.Id == item).First().ContactsID;
                var FZBK = _projectContactsService.GetList().Where(t => t.Id == item).First().FZBKId;
                string content = "（删除项目团队）" + _CurrentUser?.RealName + "删除了项目编号为" +
               _projectBasicsService.GetList().Where(t => t.Id == ProId).First().ProjectCode + "的项目团队，用户名称是：" +
               _userService.GetList().Where(t => t.Id == ConId).First().RealName + "，负责板块是：" +
               _dictionaryService.GetList().Where(t => t.Id == FZBK).First().DictionaryText;
                _projectDynamicService.Insert(new ProjectDynamicView { dyType = 219, projectID = ProId, operationContent = content });
            }
            int count = _projectContactsService.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }

        /// <summary>
        /// 根据项目ID查询项目联系人
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByIdList")]
        public IResponseDto GetByIdList(int Id)
        {
            var list = _projectContactsService.GetByIdList(Id);
            return ResponseDto.Success(list);
        }

        /// <summary>
        /// 根据ID查询项目联系人
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetConListById")]
        public IResponseDto GetConListById(int Id)
        {
            var list = _projectContactsService.GetConListById(Id);
            return ResponseDto.Success(list);
        }
    }
}
