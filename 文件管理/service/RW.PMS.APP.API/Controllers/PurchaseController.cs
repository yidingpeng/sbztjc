using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.DTO.Purchase;
using RW.PMS.Application.Contracts.Input.Message;
using RW.PMS.Application.Contracts.Message;
using RW.PMS.Application.Contracts.Purchase;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.Exceptions;
using RW.PMS.Domain.Repositories.Material;
using System.Collections;

namespace RW.PMS.API.Controllers
{
    public class PurchaseController : RWBaseController
    {
        IQrCodeService _qrcodeService;
        IFifoService _fifoService;
        IinventoryService _inventoryService;
        IConfigService _configService;
        IThirdPartService _thirdPartService;
        IConfiguration _configuration;
        IUserService _userService;
        IPurchaseService _purchaseService;
        IQrCodePicService _qrCodePicService;
        IWebHostEnvironment _evn;
        IMaterialRepository _material;
        IQCService _qc;
        IProductIssuService _issu;
        ICdService _cd;
        public PurchaseController(IQrCodeService qrcodeService, IFifoService fifoService, IinventoryService inventoryService,
            IConfigService configService, IThirdPartService thirdPartService, IQrCodePicService qrCodePicService, IWebHostEnvironment evn,
            IConfiguration configuration, IUserService userService, IPurchaseService purchaseService, IMaterialRepository material,IQCService qc,
            IProductIssuService issu, ICdService cd)
        {
            _qrcodeService = qrcodeService;
            _fifoService = fifoService;
            _inventoryService = inventoryService;
            _configService = configService;
            _thirdPartService = thirdPartService;
            _configuration = configuration;
            _userService = userService;
            _purchaseService = purchaseService;
            _qrCodePicService = qrCodePicService;
            _evn = evn;
            _material = material;
            _qc = qc;
            _issu = issu;
            _cd = cd;
        }

        /// <summary>
        /// 修改物料是否合格
        /// </summary>
        /// <param name="materialCode"></param>
        /// <param name="qualified"></param>
        /// /// <param name="imgFilesId"></param>
        /// <returns></returns>
        [HttpPost("UpdateMQualified")]
        public IResponseDto UpdateMQualified([FromBody] QrCodeDto input)
        {
            var result = _qrcodeService.UpdateMQualified(input.QrCode, input.qualified);
            var materialInfo = _qrcodeService.GetList().Where(t => t.QrCode == input.QrCode).FirstOrDefault();
            if (materialInfo != null && !string.IsNullOrEmpty(input.imgFilesId))
            {
                UpdateImgPid(input.imgFilesId, materialInfo.Id);
            }
            return result == true ? ResponseDto.Success("保存成功") : ResponseDto.Success("保存失败，该物料可能不存在");
        }


        /// <summary>
        /// 出入库信息删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete("FifoDelete")]
        public IResponseDto FifoDelete([FromBody] DeleteRequesDto input)
        {
            var ids = input.GetIds();
            var result = _fifoService.Delete(ids);
            return result > 0 ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
        }

        /// <summary>
        /// 出入库新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("FifoAdd")]
        public IResponseDto FifoAdd([FromBody] FifoDto input)
        {
            try
            {
                input.FifoDateTime = DateTime.Now.ToLocalTime();
                //var IsExist = _fifoService.IsExist(input);
                string fifotext = input.Type == 1 ? "入库" : "出库";
                int InventoryNumber = 0;//出入库后的库存数量
                //if (IsExist)
                //{
                //    return ResponseDto.Error(500, input.MaterialName + "重复" + fifotext + "!");
                //}
                var inventory = _inventoryService.GetInventoryInfo(input.MaterialCode);
                //有库存信息
                if (inventory != null)
                {
                    //入库
                    if (input.Type == 1)
                    {
                        InventoryNumber = inventory.WarehousCount + input.Count;
                    }
                    else //出库
                    {
                        InventoryNumber = inventory.WarehousCount - input.Count;
                        if (InventoryNumber < 0)
                        {
                            return ResponseDto.Success("出库数量超出库存!");
                        }
                    }
                    _inventoryService.InvCountEdit(InventoryNumber, input.MaterialCode);

                }
                else //无库存信息
                {
                    var mat = _material.getDataByCode(input.MaterialCode);
                    InventoryNumber = input.Count;
                    //如果入库没有库存信息时需要增加一条
                    _inventoryService.Insert(new InventoryDto
                    {
                        MaterialCode = input.MaterialCode,
                        MaterialName = input.MaterialName,
                        ProjectCode = input.ProjectCode,
                        ProjectName = input.ProjectName,
                        WarehousCount = InventoryNumber,
                        Model = mat == null ? "" : mat.Model,
                        Unit = mat == null ? "" : mat.BasicUnit
                    });

                }
                var entity = _fifoService.Insert(input);
                if (entity.Id > 0 && entity.Type == 2)
                {
                    _issu.Insert(new ProductIssuDto
                    {
                        QrCode = entity.QrCode,
                        ProjectCode = entity.ProjectCode,
                        ProjectName = entity.ProjectName,
                        Count = entity.Count,
                        MaterialCode = entity.MaterialCode,
                        MaterialName = entity.MaterialName,
                        State = 0,
                        LLMan = entity.FifoPersonnel
                    });
                }
                return entity.Id > 0 ? ResponseDto.Success(fifotext + "成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 出入库修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("FifoEdit")]
        public IResponseDto FifoEdit([FromBody] FifoDto input)
        {

            if (input.Id.HasValue)
            {
                string fifotext = input.Type == 1 ? "入库" : "出库";
                var IsExist = _fifoService.IsExist(input);
                if (IsExist)
                {
                    return ResponseDto.Error(500, input.MaterialName + "重复" + fifotext + "!");
                }
                var result = _fifoService.Update(input.Id.Value, input);
                return result > 0 ? ResponseDto.Success("修改成功") : ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
            }
            return ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
        }
        /// <summary>
        /// 根据二维码号查询最新的出入库信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetByIdQrCodeFifo")]
        public IResponseDto GetByIdQrCodeFifo(string QrCode, int Type)
        {
            var result = _fifoService.GetByQrCodeFifo(QrCode,Type);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 根据二维码号查询二维码信息库信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetByIdQrCode")]
        public IResponseDto GetByIdQrCode(string QrCode)
        {
            try
            {
                var result = _qrcodeService.GetByQrCode(QrCode);
                return ResponseDto.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 根据供应商查询订单子表信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("SupplierGetList")]
        public IResponseDto SupplierGetList([FromQuery] MaterialOperateSearchDto search)
        {
            var result = _purchaseService.GetSupplierPagedList(search);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 供应商修改预计、实际完成日期及预计入库日期
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [HttpPost("OrderDateTimeEdit")]
        public IResponseDto OrderDateTimeEdit(int Id, string type, string time,int BomId)
        {
            var result = _purchaseService.OrderDateTimeEdit(Id, type, time,BomId);
            if (result)
            {
                return ResponseDto.Success("更新成功");
            }
            else
            {
                return ResponseDto.Success("更新异常");
            }
        }
        /// <summary>
        /// 通过参数类型查询系统配置值
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetDeployType")]
        public IResponseDto GetDeployType(string type)
        {
            var configs = _configService.GetConfigs(type);
            return ResponseDto.Success(configs);
        }



        /// <summary>
        /// 通过参数编码查询系统配置值
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetDeployCode")]
        public IResponseDto GetDeployCode(string code)
        {
            var configs = _configService.GetConfigCode(code);
            return ResponseDto.Success(configs);
        }
        /// <summary>
        /// 获取上传质检图片方法地址（二维码表）
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetInsertFilePath")]
        public IActionResult GetInsertFilePath()
        {
            var MyUrl = _configuration["FileUrl:MyUrl"];
            string url = MyUrl + "/Purchase/InsertFile";
            return new JsonResult(new { Imgurl = url });
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
            var result = _qrCodePicService.Upload(Uploadfile);
            return new JsonResult(new { id = result.Id, Path = result.Path });

            //string ids = "", paths = "";
            //foreach (var item in UploadFile)
            //{
            //    FileDto Uploadfile = new FileDto();
            //    Uploadfile.File = UploadFile[0];
            //    Uploadfile.RootPath = _evn.ContentRootPath;
            //    var result = _qrCodePicService.Upload(Uploadfile);
            //    ids = ids + result.Id.ToString() + ',';
            //    paths = paths + result.Path.ToString() + ',';
            //}
            //return new JsonResult(new { id = ids, Path = paths });
        }

        /// <summary>
        /// 将二维码图片修改对应父id
        /// </summary>
        /// <param name="imgFilesId"></param>
        /// <param name="pId"></param>
        [HttpGet("UpdateImgPid")]
        public void UpdateImgPid(string imgFilesId, int? pId)
        {
            _qrCodePicService.UpdateImgPid(imgFilesId, pId);
        }

        /// <summary>
        /// 根据物料编码获取物料信息
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpGet("MatByCodeInfo")]
        public IResponseDto MatByCodeInfo(string Code)
        {
            var result = _material.getDataByCode(Code);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 新增质检信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("DoQCAdd")]
        public IResponseDto DoQCAdd([FromBody] QCDto input)
        {
            try
            {
                if (input.QCount == 0 && input.Qualified == 1)
                {
                    return ResponseDto.Error(500, "合格数量不能为0!");
                }
                var qrcode = _qrcodeService.GetByQrCode(input.QrCode);
                if (qrcode != null)
                {
                    var bo = _qc.QcInfo(input.QrCode);
                    if (bo != null) //存在质检信息做修改
                    {
                        if ((input.QCount + bo.QCount) <= input.Count)
                        {
                            bo.QCount += input.QCount;
                            bo.Qualified = input.Qualified;
                            int yxh = _qc.Update(bo.Id.Value, bo);
                            if (yxh > 0)
                            {
                                if (qrcode.qualified != input.Qualified)
                                {
                                    _qrcodeService.UpdateMQualified(input.QrCode, input.Qualified);
                                }
                                return ResponseDto.Success("更新成功");
                            }
                        }
                        else
                        {
                            return ResponseDto.Error(500, "已超出质检数量!");
                        }
                    }
                    else //否则新增质检信息
                    {
                        var entity = _qc.Insert(input);
                        if (entity.Id > 0)
                        {
                            _qrcodeService.UpdateMQualified(input.QrCode, 1);
                           _qrCodePicService.UpdateImgPid(input.ImgFilesId, entity.Id);
                        }
                        else
                        {
                            ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
                        }
                    }
                    return ResponseDto.Success("添加成功");
                }
                else
                {
                    return ResponseDto.Error(500, "操作异常");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 修改质检信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 
        [HttpPut("DoQCEdit")]
        public IResponseDto DoQCEdit([FromBody] QCDto input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            _qc.Update(input.Id.Value, input);
            return ResponseDto.Success("修改成功");
        }

        /// <summary>
        /// 根据二维码号查询质检信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetByQrCodeQC")]
        public IResponseDto GetByQrCodeQC(string QrCode)
        {
            try
            {
                var result = _qc.QcInfo(QrCode);
                return ResponseDto.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 根据二维码号查询报检信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetByQrCodeCD")]
        public IResponseDto GetByQrCodeCD(string QrCode)
        {
            try
            {
                var result = _cd.CDInfo(QrCode);
                return ResponseDto.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 根据二维码查询已入库数量
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetByQrCodeYRK")]
        public IResponseDto GetByQrCodeYRK(string QrCode,int Type)
        {
            try
            {
                var result = _fifoService.GetCkCount(QrCode,Type);
                return ResponseDto.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 待确认领料信息列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("IssulList")]
        public IResponseDto IssulList(string LLMan)
        {
            var list = _issu.GetList().Where(o => o.LLMan == LLMan && o.State == 0);
            return ResponseDto.Success(list);
        }

        /// <summary>
        /// 批量领料
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpPost("BatchPick")]
        public IResponseDto BatchPick([FromBody] List<ProductIssuDto> inputs)
        {
            int count = 0;
            foreach (var item in inputs)
            {
                if (item.State == 0)
                {
                    item.State = 1;
                    int bo = _issu.Update(item.Id.Value, item);
                    if (bo > 0)
                    {
                        count++;
                    }
                }
            }
            return count > 0 ? ResponseDto.Success("领料成功") : ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
        }

        /// <summary>
        /// 新增报检信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("DoCDAdd")]
        public IResponseDto DoCDAdd([FromBody] CDDto input)
        {
            var info = _cd.CDInfo(input.QrCode);
            if (info == null)
            {
                var entity = _cd.Insert(input);
                return entity.Id > 0 ? ResponseDto.Success("报检成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
            }
            else
            {
                return ResponseDto.Error(500, "报检失败(重复操作!)");
            }
        }
    }
}


