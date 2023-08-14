using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.System;

namespace RW.PMS.API.Controllers
{
    public class ProjectDeviceDetailsController : RWBaseController
    {
        IProjectDeviceDetailsService _ProjectDeviceDetailsService;
        IProjectBasicsService _projectBasicsService;
        IDictionaryService _dictionaryService;
        IClientCompanyService _clientCompanyService;
        public ProjectDeviceDetailsController(IProjectDeviceDetailsService ProjectDeviceDetailsService,IProjectBasicsService projectBasicsService, IDictionaryService dictionaryService, IClientCompanyService clientCompanyService)
        {
            _projectBasicsService = projectBasicsService;
            _dictionaryService = dictionaryService;
            _clientCompanyService = clientCompanyService;
            _ProjectDeviceDetailsService = ProjectDeviceDetailsService;
        }

        /// <summary>
        /// 项目基础信息分页
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("PageList")]
        public IResponseDto GetList([FromQuery] ProjectDeviceDetailsSearchDto search)
        {
            var result = _ProjectDeviceDetailsService.PagedList(search);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 根据项目ID查询设备信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("DeviceList")]
        public IResponseDto GetByProIDList(int Id)
        {
            var result = _ProjectDeviceDetailsService.GetByProIDList(Id);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public IResponseDto DoAdd([FromBody] ProjectDeviceDetailsView input)
        {
            bool counts = _ProjectDeviceDetailsService.ProOnly(input.DeviceNo, input.Id);//验重
            if (counts) return ResponseDto.Error(-1, "存在重复的设备编号！");
            _ProjectDeviceDetailsService.Insert(input);
            return ResponseDto.Success("添加成功！");
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public IResponseDto DoEdit([FromBody] ProjectDeviceDetailsView input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            bool counts = _ProjectDeviceDetailsService.ProOnly(input.DeviceNo, input.Id);//验重
            if (counts) return ResponseDto.Error(-1, "存在重复的设备编号！");
            _ProjectDeviceDetailsService.Update(input);
            return ResponseDto.Success("修改成功！");
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _ProjectDeviceDetailsService.Delete(ids);
            return ResponseDto.Success($"成功删除{count}条数据。");
        }

        /// <summary>
        /// 获取字典设备产品类型
        /// </summary>
        /// <returns></returns>
        [HttpGet("ProductLine")]
        public IResponseDto ProjectProductLine()
        {
            var list = _dictionaryService.GetSubItemList("ProductLine");
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

        /// <summary>
        /// 项目设备所有数据
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetAllList")]
        public IResponseDto GetAllList()
        {
            var result = _ProjectDeviceDetailsService.GetList();
            return ResponseDto.Success(result);
        }
    }
}
