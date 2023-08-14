using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.System;

namespace RW.PMS.API.Controllers
{
    public class ContactController : RWBaseController
    {
        private readonly IContactsService _contactsService;
        private readonly IClientCompanyService _clientCompanyService;
        private readonly IDictionaryService _dictionaryService;
        IOrganizationService _organizationService;
        public ContactController(IContactsService contactsService, IClientCompanyService clientCompanyService, IDictionaryService dictionaryService, IOrganizationService organizationService)
        {
            _contactsService = contactsService;
            _clientCompanyService = clientCompanyService;
            _dictionaryService = dictionaryService;
            _organizationService = organizationService;
        }
        /// <summary>
        /// 联系人分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("List")]
        public IResponseDto GetList([FromQuery] ContractSearchDto input)
        {
            var result = _contactsService.GetPagedList(input);
            return ResponseDto.Success(result);
        }

        ///// <summary>
        ///// 单位编号
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("Company")]
        //public IResponseDto GetCodeList()
        //{
            
        //    var result = _clientCompanyService.GetList();
        //    List<BaseCommonOutput> list = new List<BaseCommonOutput>();
        //    for (int i = 0; i < result.Count; i++)
        //    {
        //        BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
        //        {
        //            label = result[i].CompanyCode,
        //            value = result[i].Id
        //        };
        //        list.Add(baseCommonOutput);
        //    }
        //    return ResponseDto.Success(list);
        //}

        /// <summary>
        /// 客户公司名称
        /// </summary>
        /// <returns></returns>
        [HttpGet("CompanyFullName")]
        public IResponseDto GetCompanyFullNameList(string CurCompany)
        {

            var result = _clientCompanyService.GetList().Where(a=>a.ClientName.Contains(CurCompany)).ToList();
            List<BaseCommonOutput> list = new List<BaseCommonOutput>();
            for (int i = 0; i < result.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = result[i].ClientName,
                    value = result[i].Id
                };
                list.Add(baseCommonOutput);
            }
            return ResponseDto.Success(list);
        }

        /// <summary>
        /// 获取字典联系人类别
        /// </summary>
        /// <returns></returns>
        [HttpGet("ContactsType")]
        public IResponseDto GetContactsTypeList()
        {
            var result = _dictionaryService.GetSubItemList("ContactsType");
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
        public IResponseDto DoAdd([FromBody] ContactListDto input)
        {
            if (input.Department==-1)
            {
                input.Department = AddNewOrg(input.Departmenttext);
            }
            bool resut = _contactsService.GetList().Where(o => o.ContactsName == input.ContactsName && o.CompanyId == input.CompanyId && o.Id != input.Id && o.IsDeleted == false).Count() > 0;
            if (resut) return ResponseDto.Error(500, "已存在相同干系人信息！");
            var result = _contactsService.Insert(input);
            return ResponseDto.Success("添加成功！");
        }
        /// <summary>
        /// 修改联系人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 
        [HttpPut]
        public IResponseDto DoEdit([FromBody] ContactListDto input)
        {
            if (input.Department == -1)
            {
                //int count = _organizationService.GetList().Where(s => (string.Equals(s.OrganizationName, input.Departmenttext) && s.IsDeleted == false)).Count();
                //if (count > 0) return ResponseDto.Error(500, "部门名称已存在！");
                input.Department = AddNewOrg(input.Departmenttext);
            }
            bool resut = _contactsService.GetList().Where(o => o.ContactsName == input.ContactsName && o.CompanyId == input.CompanyId && o.Id != input.Id && o.IsDeleted == false).Count() > 0;
            if (resut) return ResponseDto.Error(500, "已存在相同干系人信息！");
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            _contactsService.Update(input);
            return ResponseDto.Success("修改成功！");
        }

        [HttpGet]
        public int AddNewOrg(string orgName)
        {
            int result= _organizationService.addNewOrgnization(orgName);
            return result;
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
            int count = _contactsService.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }

        /// <summary>
        /// 联系人分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetPagedListByRol")]
        public IResponseDto GetPagedListByRol(string? contactsName, int rolId, int PageNo, int PageSize, string? QueryType)
        {
            var result = _contactsService.GetPagedListByRol(contactsName, rolId,PageNo,PageSize, QueryType);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 根据客户名称获取联系人数据
        /// </summary>
        /// <param name="ClientId"></param>
        /// <returns></returns>
        [HttpGet("GetListByClientId")]
        public IResponseDto GetListByClientId(int ClientId)
        {
            var result=_contactsService.GetListByClientId(ClientId);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 根据id获取联系人信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetListById")]
        public IResponseDto GetListById(int Id)
        {
            var result = _contactsService.GetListById(Id);
            return ResponseDto.Success(result);
        }

    }
}
