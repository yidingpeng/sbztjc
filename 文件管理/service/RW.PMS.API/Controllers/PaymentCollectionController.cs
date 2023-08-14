using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.System;

namespace RW.PMS.API.Controllers
{
    public class PaymentCollectionController : RWBaseController
    {
        IPayment_CollectionService _payment_CollectionService;
        IDictionaryService _dictionaryService;
        IProjectBasicsService _projectBasicsService;
        IContract_InfoService _contract_InfoService;
        IProjectDeliveryService _projectDeliveryService;


        public PaymentCollectionController(IPayment_CollectionService payment_CollectionService, IDictionaryService dictionaryService,
            IProjectBasicsService projectBasicsService, IContract_InfoService contract_InfoService, IProjectDeliveryService projectDeliveryService)
        {
            this._payment_CollectionService = payment_CollectionService;
            this._dictionaryService = dictionaryService;
            this._projectBasicsService = projectBasicsService;
            this._contract_InfoService = contract_InfoService;
            this._projectDeliveryService = projectDeliveryService;
        }
        #region 开票回款
        /// <summary>
        /// 开票回款分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public IResponseDto Payment_CollectionPagedList([FromQuery] Payment_CollectionSearchDto input)
        {
            var pagedList = _payment_CollectionService.GetPagedList(input);
            return ResponseDto.Success(pagedList);
        }

        /// <summary>
        /// 根据ID查询回款信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetByIdList")]
        public IResponseDto GetByIdList(int Id)
        {
            var pagedList = _payment_CollectionService.GetByIdList(Id);
            return ResponseDto.Success(pagedList);
        }
        /// <summary>
        /// vue api回款项目信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("Pro")]
        public IResponseDto GetProjectPayment()
        {
            var pagedList = _projectBasicsService.GetList();
            List<BaseCommonOutput> result = new List<BaseCommonOutput>();
            for (int i = 0; i < pagedList.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    name = pagedList[i].ProjectName,
                    code = pagedList[i].Id
                };
                result.Add(baseCommonOutput);
            }
            return ResponseDto.Success(result);
        }
        /// <summary>
        /// vue api回款合同信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("Contra")]
        public IResponseDto GetContractPayment()
        {
            var list = _contract_InfoService.GetList();
            List<BaseCommonOutput> result = new List<BaseCommonOutput>();
            for (int i = 0; i < list.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    name = list[i].ct_code,
                    code = list[i].Id
                };
                result.Add(baseCommonOutput);
            }
            return ResponseDto.Success(result);
        }
        /// <summary>
        /// vue api回款项目信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("Deliv")]
        public IResponseDto GetDeliveryPayment()
            {
            var pagedList = _projectDeliveryService.GetList();
            List<BaseCommonOutput> result = new List<BaseCommonOutput>();
            for (int i = 0; i < pagedList.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    name = pagedList[i].DeliveryCode,
                    code = pagedList[i].Id
                };
                result.Add(baseCommonOutput);
            }
            return ResponseDto.Success(result);
        }
        /// <summary>
        /// 开票回款添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public IResponseDto AddPayment_Collection([FromBody] Payment_CollectionDto input)
        {
            var result = _payment_CollectionService.Insert(input);
            return ResponseDto.Success("添加成功！");
        }
        /// <summary>
        /// 开票回款编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public IResponseDto EditPayment_Collection([FromBody] Payment_CollectionDto input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            _payment_CollectionService.Update(input);
            return ResponseDto.Success("修改成功！");
        }
        /// <summary>
        /// 开票回款删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        [HttpDelete]
        public IResponseDto DeleteClient_Company([FromBody] DeleteCli_PayKeys model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _payment_CollectionService.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }

        /// <summary>
        /// 获取字典回款类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("ReturnType")]
        public IResponseDto GetReturnTypeList()
        {
            var result = _dictionaryService.GetSubItemList("ReturnType");
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
        /// 获取字典回款方式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("ReturnWay")]
        public IResponseDto GetReturnWayList()
        {
            var result = _dictionaryService.GetSubItemList("ReturnWay");
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
        /// 根据客户名称获取回款数据
        /// </summary>
        /// <param name="clietId"></param>
        /// <returns></returns>
        [HttpGet("GetListByClientId")]
        public IResponseDto GetListByClientId(int clietId)
        {
            var reslult = _payment_CollectionService.GetListByClientId(clietId);
            return ResponseDto.Success(reslult);
        }
        #endregion
    }
}
