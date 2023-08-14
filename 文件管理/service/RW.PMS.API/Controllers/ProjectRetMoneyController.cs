using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.System;

namespace RW.PMS.API.Controllers
{
    public class ProjectRetMoneyController : RWBaseController
    {
        IProjectRetMoneyService _projectRetMoneyService;
        IDictionaryService _dictionaryService;
        IProjectBasicsService _projectBasicsService;
        IProjectDeliveryService _projectDeliveryService;

        public ProjectRetMoneyController(IProjectRetMoneyService projectRetMoneyService, IDictionaryService dictionaryService, IProjectBasicsService projectBasicsService, IProjectDeliveryService projectDeliveryService)
        {
            this._projectRetMoneyService = projectRetMoneyService;
            this._dictionaryService = dictionaryService;
            this._projectBasicsService = projectBasicsService;
            this._projectDeliveryService = projectDeliveryService;
        }
        #region 质保金
        System.DateTime dealTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        /// <summary>
        /// 质保金分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public IResponseDto GetRetMoney([FromQuery] ProjectRetMoneySearchDto input)
        {
            var list = _projectRetMoneyService.GetPagedList(input);
            return ResponseDto.Success(list);
        }
        /// <summary>
        /// vue api回款项目信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetByIdList")]
        public IResponseDto GetByIdList(int Id)
        {
            var pagedList = _projectRetMoneyService.GetByIdList(Id);
            return ResponseDto.Success(pagedList);
        }
        /// <summary>
        /// vue api回款项目信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("Pro")]
        public IResponseDto GetProjectPayment(string name)
        {
            var pagedList = _projectBasicsService.GetList().Where(a => a.ProjectName.Contains(name) || a.ProjectCode.Contains(name)).ToList();
            List<BaseCommonOutput> result = new List<BaseCommonOutput>();
            for (int i = 0; i < pagedList.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = pagedList[i].ProjectName,
                    value = pagedList[i].Id
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
        /// 质保金添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public IResponseDto AddRetMoney([FromBody] ProjectRetMoneyDto input)
        {
            var IsExist = _projectRetMoneyService.IsExist(input);
            if (IsExist)
            {
                return ResponseDto.Error(500, "该项目质保金已存在");
            }
            string[] Warr = input.WarrantyPeriodObject.ToString().Split('~').Select(x => x).ToArray();
            var timeone = dealTime.AddSeconds(double.Parse(Warr[0])).ToString("s");
            var starttime = timeone.Split("T");
            var timetwo = dealTime.AddSeconds(double.Parse(Warr[1])).ToString("s");
            var endtime = timetwo.Split("T");
            input.WarrantyPeriod = starttime[0] + "~" + endtime[0];
            var entity = _projectRetMoneyService.Insert(input);
            return entity.Id > 0 ? ResponseDto.Success("添加成功！") : ResponseDto.Success("未能添加成功！");
        }
        /// <summary>
        /// 质保金编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public IResponseDto EditRetMoney([FromBody] ProjectRetMoneyDto input)
        {
            var IsExist = _projectRetMoneyService.IsExist(input);
            if (IsExist)
            {
                return ResponseDto.Error(500, "该项目质保金已存在");
            }
            string[] Warr = input.WarrantyPeriodObject.ToString().Split('~').Select(x => x).ToArray();
            var timeone = dealTime.AddSeconds(double.Parse(Warr[0])).ToString("s");
            var starttime = timeone.Split("T");
            var timetwo = dealTime.AddSeconds(double.Parse(Warr[1])).ToString("s");
            var endtime = timetwo.Split("T");
            input.WarrantyPeriod = starttime[0] + "~"+ endtime[0];
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            var result = _projectRetMoneyService.Update(input.Id.Value, input);
            return result > 0 ? ResponseDto.Success("修改成功！") : ResponseDto.Success("未能修改成功！");
        }
        /// <summary>
        /// 质保金删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        [HttpDelete]
        public IResponseDto DeleteRetMoney([FromBody] DeleteCli_PayKeys model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _projectRetMoneyService.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }
        #endregion
    }
}
