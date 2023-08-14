using Microsoft.AspNetCore.Mvc;
using RW.PMS.API.Filters;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Organization;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.System;
using RW.PMS.Domain.Entities.System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RW.PMS.API.Controllers
{
    public class OrganizationController : RWBaseController
    {
        IOrganizationService _organizationService;
        IDictionaryService _dictionaryService;
        public OrganizationController(IOrganizationService organizationService,IDictionaryService dictionaryService)
        {
            this._organizationService = organizationService;
            _dictionaryService = dictionaryService;
        }

        // GET: api/<OrganizationController>
        [HttpGet]
        public IResponseDto GetList([FromQuery] OrganizationSearchDto search)
        {
            //return new string[] { "value1", "value2" };
            var result = _organizationService.GetTreeList(search);
            return ResponseDto.Success(result);
        }

        //// GET api/<OrganizationController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<OrganizationController>
        [HttpPost]
        //[LogFilter(PageName = "组织架构", ActionName = "修改")]
        public IResponseDto Post([FromBody] OrganizationDetailDto input)
        {
            int count = _organizationService.GetList().Where(s => (string.Equals(s.OrganizationName, input.Name)|| string.Equals(s.OrganizationCode, input.Code)) && s.Id != input.Id && s.IsDeleted == false).Count();
            if(count > 0) return ResponseDto.Error(500, "部门名称或部门编码已存在！");
            var result = _organizationService.Insert(input);
            return ResponseDto.Success("添加成功");
        }

        // PUT api/<OrganizationController>/5
        [HttpPut]
        //[LogFilter(PageName = "组织架构", ActionName = "修改", Format = "{account} {ActionName} {PageName} {input.Name}")]
        public IResponseDto Put([FromBody] OrganizationDetailDto input)
        {
            int count = _organizationService.GetList().Where(s => (string.Equals(s.OrganizationName, input.Name) || string.Equals(s.OrganizationCode, input.Code)) && s.Id != input.Id && s.IsDeleted == false).Count();
            if (count > 0) return ResponseDto.Error(500, "名称或部门编码已存在！");
            var result = _organizationService.Update(input.Id, input);
            return ResponseDto.Success("修改成功");
        }

        // DELETE api/<OrganizationController>/5
        [HttpDelete()]
        public IResponseDto Delete([FromBody] DeleteRequesDto input)
        {
            var ids = input.GetIds();
            foreach (var item in ids)
            {
                var childrenId = _organizationService.GetByParentIdList(item);
                if (childrenId.Any())
                {
                    DeleteChiledren(childrenId);
                }
                    
            }
            var result = _organizationService.Delete(ids);
            return ResponseDto.Success("删除成功");
        }

        /// <summary>
        /// 删除父项目下的所有子项目
        /// </summary>
        /// <param name="Id"></param>
        [HttpGet("DeleteChiledren")]
        public void DeleteChiledren(List<OrganizationListDto> list)
        {
            foreach (var CItem in list)
            {
                if (CItem.Children != null)
                {
                    DeleteChiledren(CItem.Children);
                }
                _organizationService.Delete(CItem.Id);
            }
        }

        [HttpGet("tree")]
        public IResponseDto GetTreeList()
        {
            var orgs = _organizationService.GetList();
            var tree = this.GetTrees(orgs.ToList(), 0);
            return ResponseDto.Success(tree);
        }

        List<TreeDto> GetTrees(List<OrganizationEntity> entities, int parent)
        {
            var lst = entities
                .Where(x => x.ParentId == parent)
                .Select(x => new TreeDto
                {
                    ID = x.Id,
                    Label = x.OrganizationName,
                    Children = GetTrees(entities, x.Id)
                })
                .ToList();
            return lst;
        }
        /// <summary>
        /// 获取字典组织类别
        /// </summary>
        /// <returns></returns>
        [HttpGet("OrganizationType")]
        public IResponseDto GetOrganizationType()
        {
            var list = _dictionaryService.GetSubItemList("OrganizationType");
            List<BaseCommonOutput> result = new List<BaseCommonOutput>();
            for (int i = 0; i < list.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = list[i].DictionaryText,
                    value = list[i].Id
                };
                result.Add(baseCommonOutput);
            }
            return ResponseDto.Success(result);
        }
    }

 
}
