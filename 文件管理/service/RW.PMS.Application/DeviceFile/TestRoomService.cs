using AutoMapper;
using RW.PMS.Application.Contracts.DeviceFile;
using RW.PMS.Application.Contracts.DTO.DeviceFile;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Common;
using RW.PMS.Domain.Entities.DeviceFile;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.DeviceFile
{
    public class TestRoomService : CrudApplicationService<TestRoomEntity, int>, ITestRoomService
    {
        public TestRoomService(IDataValidatorProvider dataValidator,
                            IRepository<TestRoomEntity, int> productionRepository,
                            IMapper mapper,
                            Lazy<ICurrentUser> currentUser) :
           base(dataValidator, productionRepository, mapper, currentUser)
        {
        }
        public PagedResult<TestRoomDto> getroomAlllist()
        {
            var list = Repository.Orm.Select<TestRoomEntity>().ToList().Select(t => Mapper.Map<TestRoomDto>(t)).ToList();
            return new PagedResult<TestRoomDto>(list, list.Count);
        }
    }
}
