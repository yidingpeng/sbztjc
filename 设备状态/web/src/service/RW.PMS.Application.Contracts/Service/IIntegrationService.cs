
using Microsoft.AspNetCore.Http;
using RW.PMS.Application.Contracts.DTO.File;
using RW.PMS.Application.Contracts.DTO.Integration;
using RW.PMS.Domain.Entities.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Service
{
    public interface IIntegrationService : ICrudApplicationService<IntegrationRecordEntity, int>
    {
        int ImportOrganization(ImportOrganizationDto dto);

        ExportOrganizationDto ExportOrganization();
    }
}
