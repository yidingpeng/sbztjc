using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Basics;
using RW.PMS.Application.Contracts.Basics;
using RW.PMS.Application.Contracts.System;
using RW.PMS.Application.System;
using RW.PMS.Application.Contracts.DTO.Configuration;
using RW.PMS.Application.Basics;
using RW.PMS.Application.Contracts.DTO.Project;
using System.ComponentModel.Design;
using IDictionaryService = RW.PMS.Application.Contracts.System.IDictionaryService;
using Microsoft.AspNetCore.Authorization;

namespace RW.PMS.API.Controllers
{
    public class MaterialController : RWBaseController
    {
        private readonly IMaterialService _materialService;
        private readonly IDictionaryService _dictionaryService;
        private readonly IMaterialDetailService _materialDetailService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _evn;
        public MaterialController(IMaterialService roleService, IDictionaryService dictionaryService, IMaterialDetailService materialDetailService, IConfiguration configuration, IWebHostEnvironment evn)
        {
            _materialService = roleService;
            _dictionaryService = dictionaryService;
            _materialDetailService = materialDetailService;
            _configuration = configuration;
            _evn = evn;
        }

        [HttpGet]
        public IResponseDto GetList([FromQuery] MaterialSearchDto input)
        {
            var result = _materialService.GetPagedList(input);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 数据是否重复
        /// </summary>
        [HttpGet("GetRepeat")]
        public IResponseDto GetRepeat([FromQuery] MaterialSearchDto input)
        {
            bool model = _materialService.Repeatjudgment(input);

            return ResponseDto.Success(model);
        }

        /// <summary>
        /// 根据id获取物料图片信息id
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [HttpGet("GetImageByPtid")]
        public IActionResult GetImageByPtid(int pid)
        {
            var result = _materialDetailService.GetImageByPtid(pid);
            return new JsonResult(new { ids = result });
        }

        /// <summary>
        /// 将上传的图片修改对应父id
        /// </summary>
        /// <param name="imgFilesId"></param>
        /// <param name="pId"></param>
        [HttpGet("UpdateImgPid")]
        public void UpdateImgPid(string imgFilesId, int? pId)
        {
            _materialDetailService.UpdateImgPid(imgFilesId, pId);
        }

        /// <summary>
        /// 把该所有父id修改成0
        /// </summary>
        /// <param name="Pid"></param>
        /// <returns></returns>
        [HttpGet("UpdateImgPidtoZero")]
        public IResponseDto UpdateImgPidtoZero(int Pid)
        {
            var result = _materialDetailService.UpdateImgPidtoZero(Pid);
            return result ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, "删除失败");
        }

        /// <summary>
        /// 获取上传文件地址
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetInsertFilePath")]
        public string GetInsertFilePath()
        {
            var MyUrl = _configuration["FileUrl:MyUrl"];
            string url = MyUrl + "/Material/InsertFile";
            return url;
        }

        [HttpGet("GetInsertFilePath2")]
        public IActionResult GetInsertFilePath2()
        {
            var MyUrl = _configuration["FileUrl:MyUrl"];
            string url = MyUrl + "/Material/InsertFile";
            return new JsonResult(new { Imgurl = url }); ;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="UploadFile"></param>
        /// <returns></returns>
        [HttpPost("InsertFile")]
        public IActionResult InsertFile(IFormFileCollection UploadFile)
        {
            FileDto Uploadfile = new FileDto();
            Uploadfile.File = UploadFile[0];
            Uploadfile.RootPath = _evn.ContentRootPath;
            var result = _materialDetailService.Upload(Uploadfile);
            return new JsonResult(new { id = result.Id, Path = result.Path });
        }

        /// <summary>
        /// 根据父id获取工具图片文件
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetFilesByPid")]
        public IResponseDto GetFilesByPid(int Id)
        {
            var result = _materialDetailService.GetFilesByPid(Id);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 获取图片下载路径
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFilePreviewPath")]
        public string GetFilePreviewPath()
        {
            var MyUrl = _configuration["FileUrl:MyUrl"];
            string url = MyUrl + "/Material/GetPictureData";
            return url;
        }


        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get")]
        [AllowAnonymous]
        public ActionResult Get(int id)
        {
            var item = _materialDetailService.GetFile(id);
            return new JsonResult(new { url = "data:image/png;base64," + Convert.ToBase64String(item) });

        }

        /// <summary>
        /// 获取字典物料分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("MaterialClass")]
        public IResponseDto GetMaterialClassList()
        {
            var result = _dictionaryService.GetSubItemList("MaterialClass");
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
        /// 获取字典物料类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("MaterialType")]
        public IResponseDto GetMaterialTypeList()
        {
            var result = _dictionaryService.GetSubItemList("MaterialType");
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
        /// 获取字典基本单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("MaterialUnit")]
        public IResponseDto GetMaterialUnitList()
        {
            var result = _dictionaryService.GetSubItemList("MaterialUnit");
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
        /// 获取字典重要度
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("Measuringimportance")]
        public IResponseDto GetMeasuringimportanceList()
        {
            var result = _dictionaryService.GetSubItemList("Measuringimportance");
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
        /// 获取字典材质
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("MeasuringTexture")]
        public IResponseDto GetMeasuringTextureList()
        {
            var result = _dictionaryService.GetSubItemList("MeasuringTexture");
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

        [HttpPost]
        public IResponseDto DoAdd([FromBody] MaterialDto input)
        {
            var result = _materialService.Insert(input);

            if (result.Id > 0)
            {
                UpdateImgPid(input.imgFilesId, result.Id);
            }
            return ResponseDto.Success("添加成功！");
        }

        [HttpPut]
        public IResponseDto DoEdit([FromBody] MaterialDto input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            UpdateImgPid(input.imgFilesId, input.Id);
            _materialService.Update(input.Id, input);
            return ResponseDto.Success("修改成功！");
        }

        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _materialService.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }
    }
}
