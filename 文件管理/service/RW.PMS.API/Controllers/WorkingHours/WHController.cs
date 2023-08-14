using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.WorkingHours;
using RW.PMS.Application.Contracts.WorkingHours;
using RW.PMS.CrossCutting.Exceptions;

namespace RW.PMS.API.Controllers.WorkingHours
{
    public class WHController : RWBaseController
    {
        IWSService _wh;
        public WHController(IWSService wh)
        {
            _wh = wh;
        }

        /// <summary>
        /// 根据供应商查询订单子表信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetPageList")]
        public IResponseDto GetPageList([FromQuery] WHDetailSearchDto search)
        {
            try
            {
                var result = _wh.GetPageList(search);
                return ResponseDto.Success(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 新增、修改工时信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("WHAddOrEdit")]
        public IResponseDto WHAddOrEdit([FromBody] WHDetailDto input)
        {
            try
            {
                if (input.Id > 0)
                {
                    var line = _wh.Update(input.Id, input);
                    return line > 0 ? ResponseDto.Success("更新成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
                }
                else
                {
                    var entity = _wh.Insert(input);
                    return entity.Id > 0 ? ResponseDto.Success("新增成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 工时信息删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete("WHDelete")]
        public IResponseDto WHDelete([FromBody] DeleteRequesDto input)
        {
            var ids = input.GetIds();
            var result = _wh.Delete(ids);
            return result > 0 ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
        }
    }
}
