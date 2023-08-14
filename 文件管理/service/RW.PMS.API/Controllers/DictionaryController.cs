using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.Input.System;
using RW.PMS.Application.Contracts.System;
using RW.PMS.Domain.Entities.System;

namespace RW.PMS.API.Controllers
{
    public class DictionaryController : RWBaseController
    {
        IDictionaryService _dictionaryService;

        public DictionaryController(IDictionaryService dictionaryService)
        {
            this._dictionaryService = dictionaryService;
        }

        [HttpPost]
        public IResponseDto DoAdd([FromBody] DictionaryInput input)
        {
            bool isExistText = _dictionaryService.DictionaryTextExist(input, "DictionaryText");
            bool isExistValue = _dictionaryService.DictionaryTextExist(input, "DictionaryValue");
            if (isExistText|| isExistValue)
            {
                return ResponseDto.Error(500, "字典名称或字典值已存在");
            }
            Domain.Entities.System.DictionaryEntity dictionaryEntity = _dictionaryService.Insert(input);
            return ResponseDto.Success("添加成功！");
        }

        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _dictionaryService.DeleteAndChildren(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }

        [HttpPut]
        public IResponseDto DoEdit([FromBody] DictionaryInput input)
        {
            if (!input.Id.HasValue) return ResponseDto.Error(500, "ID不能为空！");

            bool isExistText = _dictionaryService.DictionaryTextExist(input, "DictionaryText");
            bool isExistValue = _dictionaryService.DictionaryTextExist(input, "DictionaryValue");
            if (isExistText || isExistValue)
            {
                return ResponseDto.Error(500, $"\"{input.DictionaryText}\"已存在");
            }

            _dictionaryService.Update(input.Id.Value, input);
            return ResponseDto.Success("修改成功！");
        }

        [HttpGet]
        public IResponseDto GetList([FromQuery] DictionarySearchInput search)
        {
            PagedResult<Application.Contracts.Output.System.DictionaryOutput> pagedResult = _dictionaryService.GetPagedList(search);
            return ResponseDto.Success(pagedResult);
        }

        [HttpGet("GetParentList")]
        public IResponseDto GetParentList()
        {
            DictionarySearchInput dictionarySearchInput = new DictionarySearchInput() { ParentId = 0 };
            IList<Application.Contracts.Output.System.DictionaryOutput> dictionaryOutputs = _dictionaryService.GetList(dictionarySearchInput);
            return ResponseDto.Success(dictionaryOutputs);
        }
        [HttpGet("KeyValue")]
        public IResponseDto GetKeyValue(string type)
        {
            var lst = _dictionaryService.GetKeyValue(type);
            return ResponseDto.Success(lst);
        }

        [HttpGet("all")]
        [AllowAnonymous]

        public IResponseDto GetAllDictionary()
        {
            var lst = _dictionaryService.GetAll();

            return ResponseDto.Success(lst);
        }
    }
}
