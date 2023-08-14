
using AutoMapper;
using RW.PMS.Application.Contracts.DeviceFile;
using RW.PMS.Application.Contracts.DTO.DeviceFile;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Basics;
using RW.PMS.Domain.Entities.Common;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.DeviceFile
{
    public class DeviceService : CrudApplicationService<DeviceEntity, int>, IDeviceService
    {
        public DeviceService(IDataValidatorProvider dataValidator,
                            IRepository<DeviceEntity, int> productionRepository,
                            IMapper mapper,
                            Lazy<ICurrentUser> currentUser) :
           base(dataValidator, productionRepository, mapper, currentUser)
        {
        }

        public PagedResult<DeviceDto>  getdeviceAlllist()
        {
            var list = Repository.Orm.Select<DeviceEntity>().ToList().Select(t => Mapper.Map<DeviceDto>(t)).ToList();
            return new PagedResult<DeviceDto>( list,list.Count);
        }
    }
}
