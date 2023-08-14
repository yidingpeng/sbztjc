using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.System;

namespace RW.PMS.API.Controllers
{
    public class ClientCompanyController : RWBaseController
    {
        IClientCompanyService _clientCompanyService;
        IDictionaryService _dictionaryService;
        public ClientCompanyController(IClientCompanyService clientCompanyService, IDictionaryService dictionaryService)
        {
            this._clientCompanyService = clientCompanyService;
            this._dictionaryService = dictionaryService;
        }
        #region 客户公司信息
        /// <summary>
        /// 客户公司信息分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public IResponseDto GetClientCompany([FromQuery] Client_CompanySearchDto input)
        {
            var list = _clientCompanyService.GetPagedList(input);
            return ResponseDto.Success(list);
        }
        /// <summary>
        /// 根据ID查询数据集
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetByIdList")]
        public IResponseDto GetByIdList(int Id)
        {
            var list = _clientCompanyService.GetByIdList(Id);
            return ResponseDto.Success(list);
        }
        /// <summary>
        /// 获取字典客户级别
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("DictionaryClient")]
        public IResponseDto GetDicClient()
        {
            var list = _dictionaryService.GetSubItemList("ClientRank");
            List<BaseCommonOutput> result = new List<BaseCommonOutput>();
            for (int i = 0; i < list.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    name = list[i].DictionaryText,
                    code = list[i].Id
                };
                result.Add(baseCommonOutput);
            }
            return ResponseDto.Success(result);
        }
        /// <summary>
        /// 获取字典业务类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("DictionaryBusiness")]
        public IResponseDto GetDicBusiness()
        {
            var list = _dictionaryService.GetSubItemList("CooperativeBusiness");
            List<BaseCommonOutput> result = new List<BaseCommonOutput>();
            for (int i = 0; i < list.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    name = list[i].DictionaryText,
                    code = list[i].Id
                };
                result.Add(baseCommonOutput);
            }
            return ResponseDto.Success(result);
        }
        /// <summary>
        /// 客户公司信息添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public IResponseDto AddClient_Company([FromBody] Client_CompanyDto input)
        {
            int list = _clientCompanyService.Disf(input.CompanyCode, 0);
            if (list > 0)
            {
                return ResponseDto.Error(-1, "该客户编号已存在");
            }
            var entity = _clientCompanyService.Insert(input);
            return entity.Id > 0 ? ResponseDto.Success("添加成功！") : ResponseDto.Success("未能添加成功！");
        }
        /// <summary>
        /// 客户公司信息编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public IResponseDto EditClient_Company([FromBody] Client_CompanyDto input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            int list = _clientCompanyService.Disf(input.CompanyCode, (int)input.Id);
            if (list > 0)
            {
                return ResponseDto.Error(-1, "该客户编号已存在");
            }
            var result = _clientCompanyService.Update(input.Id.Value, input);
            return result > 0 ? ResponseDto.Success("修改成功！") : ResponseDto.Success("未能修改成功！");
        }
        /// <summary>
        /// 客户公司信息删除vue
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        [HttpDelete]
        public IResponseDto DeleteClient_Company([FromBody] DeleteCli_PayKeys model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _clientCompanyService.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }
        
        /// <summary>
        /// 获取公司全部信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllList")]
        public IResponseDto GetAllList()
        {
            var list = _clientCompanyService.GetList().Where(t => t.IsDeleted == false).ToList();
            return ResponseDto.Success(list);
        }
        #endregion
    }
}
