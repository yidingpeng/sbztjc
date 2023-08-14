using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.File;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.Exceptions;
using System.Drawing;
using System.Net.Http.Headers;
using System.Text;

namespace RW.PMS.API.Controllers
{
    public class UploadController : RWBaseController
    {
        private IFileService _fileService;
        private IUserService _userService;
        public UploadController(IFileService fileService, IUserService userService)
        {
            _fileService = fileService;
            _userService = userService;
        }

        [HttpGet]
        public IResponseDto GetList([FromQuery] FileQueryDto query)
        {
            var lst = _fileService.GetPagedList(query);
            return ResponseDto.Success(lst);
        }

        [HttpGet("base64/{id}")]
        [AllowAnonymous]
        public string Get(int id)
        {
            try
            {
                var item = _fileService.GetFile(id);
                return "data:image/png;base64," + Convert.ToBase64String(item.Data);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ResponseDto.Error(404, ex.Message));
            }
        }

        [HttpGet("image/{id}")]
        [AllowAnonymous]
        public void GetImage(int id)
        {
            try
            {
                var item = _fileService.GetFile(id);
                Response.Body.WriteAsync(item.Data, 0, item.Data.Length);
            }
            catch (Exception ex)
            {
                var bts = Encoding.UTF8.GetBytes(ex.Message);
                Response.Body.Write(bts, 0, bts.Length);
            }
        }

        [HttpGet("download/{id}")]
        public object Download(int id)
        {
            try
            {
                var item = _fileService.GetFile(id);
                if (item == null)
                    throw new LogicException(ExceptionCode.Failed, "文件未找到");
                MemoryStream ms = new MemoryStream(item.Data);
                return File(ms, item.ContentType, item.Filename, enableRangeProcessing: true);
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ResponseDto.Error(400, ex.Message);
            }
            //return File(item.Data, item.ContentType);
        }

        [HttpDelete("{id}")]
        public IResponseDto DeleteFile(int id)
        {
            _fileService.Remove(id);
            return ResponseDto.Success("删除成功");
        }

        [HttpPost("{type}")]
        public IResponseDto UploadFile(IFormFile file, string? type)
        {
            if (file == null) return ResponseDto.Error(402, "请上传文件");
            //if (Request.Form.Files.Count == 0)
            //    return ResponseDto.Error(-1, "请上传文件.");
            //file.File = Request.Form.Files[0];
            //读取文件流，将文件保存
            var stream = file.OpenReadStream();
            byte[] buffer = new byte[stream.Length];

            var result = _fileService.Upload(new FileInputDto
            {
                Data = buffer,
                ContentType = file.ContentType,
                Filename = file.FileName,
                RootPath = type
            });

            return ResponseDto.Success(result);
        }

        [HttpPost("avatar")]
        public async Task<IResponseDto> UPloadAvatar()
        {
            var stream = Request.Body;
            StreamReader reader = new StreamReader(stream);
            var data = reader.ReadToEnd();
            var output = _userService.UploadAvatar(data);

            return ResponseDto.Success(output ? "上传成功" : "上传失败");
        }
    }
}
