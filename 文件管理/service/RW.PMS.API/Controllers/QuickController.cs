using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.UserData;
using RW.PMS.Application.Contracts.Service;

namespace RW.PMS.API.Controllers
{
    public class QuickController : RWBaseController
    {
        IUserQuickService userQuickService;
        public QuickController(IUserQuickService userQuickService)
        {
            this.userQuickService = userQuickService;
        }

        [HttpGet]
        public IResponseDto GetList(int top)
        {
            var lst = userQuickService.GetQuickList(top);
            return ResponseDto.Success(lst);
        }

        [HttpPut("{id}")]
        public IResponseDto DoEdit(int id, QuickDto dto)
        {
            int num = userQuickService.Update(id, dto);
            return ResponseDto.UpdateResult(num > 0);
        }

        [HttpPost]
        public IResponseDto DoAdd(QuickDto dto)
        {
            var num = userQuickService.Insert(dto);
            return ResponseDto.InsertResult(num != null);
        }

        [HttpDelete("{id}")]
        public IResponseDto DoDelete(int id)
        {
            var result = userQuickService.Delete(id);
            return ResponseDto.DeleteResult(result == 1);
        }
    }
}
