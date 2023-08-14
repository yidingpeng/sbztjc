using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.DeviceFile;
using RW.PMS.Domain.Entities.DeviceFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DeviceFile
{
    public  interface ITestSheetService : ICrudApplicationService<TestSheetEntity, int>
    {
        int insertTestSheet(TestSheetDto input);
        PagedResult<TestSheetDto> getTestSheetAllList(TestSheetDto input);
        bool Update(TestSheetDto input);
        bool Repeatjudgment(TestSheetDto input);
        List<TestSheetDto> getTestSheetAllListDateTime();
    }
}
