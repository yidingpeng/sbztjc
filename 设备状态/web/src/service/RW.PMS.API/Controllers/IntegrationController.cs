using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.File;
using RW.PMS.Application.Contracts.DTO.Integration;
using RW.PMS.Application.Contracts.Service;
using RW.PMS.CrossCutting.Exceptions;

namespace RW.PMS.API.Controllers
{
    public class IntegrationController : RWBaseController
    {
        readonly IIntegrationService integrationService;
        public IntegrationController(IIntegrationService integrationService)
        {
            this.integrationService = integrationService;
        }

        [HttpPost("import/organization")]
        public IResponseDto Import([FromBody] ImportOrganizationDto dto)
        {
            int count = integrationService.ImportOrganization(dto);
            return ResponseDto.Success($"导入成功，共导入{count}条数据。");
        }

        [HttpPost("export/organization")]
        public object Export()
        {
            try
            {
                var dto = integrationService.ExportOrganization();

                MemoryStream ms = new MemoryStream(dto.Data);
                return File(ms, "application/vnd.ms-excel", dto.Filename, enableRangeProcessing: true);
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ResponseDto.Error(400, ex.Message);
            }
        }

    }
}
