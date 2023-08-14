using AutoMapper;
using RW.PMS.Application.Contracts.Basics;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Basics;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Basics;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Basics
{
    public class DevicestatusService : CrudApplicationService<DevicestatusEntity, int>, IDevicestatusService
    {
        public DevicestatusService(IDataValidatorProvider dataValidator,
                           IRepository<DevicestatusEntity, int> productionRepository,
                           IMapper mapper,
                           Lazy<ICurrentUser> currentUser) :
          base(dataValidator, productionRepository, mapper, currentUser)
        {
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<DevicestatusDto> GetPagedList(DevicestatusSearchDto input)
        {
            var list = Repository.Select
                //.WhereIf(input.DeviceName.NotNullOrWhiteSpace(), x => x.DeviceName.Contains(input.DeviceName))
                //.WhereIf(input.RoomNumber.NotNullOrWhiteSpace(), x => x.RoomNumber.Contains(input.RoomNumber))
                //.WhereIf(input.IP.NotNullOrWhiteSpace(), x => x.IP.Contains(input.IP))
                .Count(out var total).Page(input.PageNo, input.PageSize).ToList();
            return new PagedResult<DevicestatusDto>(Mapper.Map<List<DevicestatusDto>>(list), total);
        }

        /// <summary>
        /// 查询监听路径是否重复
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Repeatjudgment(DevicestatusSearchDto input)
        {
            return Repository.Select.WhereIf(input.Id.HasValue, r => r.Id != input.Id).Where(r => r.Path == input.Path).Count() > 0;
        }

    }
}
