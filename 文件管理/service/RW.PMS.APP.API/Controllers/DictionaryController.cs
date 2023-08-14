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
            _dictionaryService = dictionaryService;
        }
        /// <summary>
        /// 查询所有字典数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [AllowAnonymous]

        public IResponseDto GetAllDictionary()
        {
            var lst = _dictionaryService.GetAll();

            return ResponseDto.Success(lst);
        }
    }
}
