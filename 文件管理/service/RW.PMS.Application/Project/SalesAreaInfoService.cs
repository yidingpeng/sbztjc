using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Project;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Project
{
    public class SalesAreaInfoService : CrudApplicationService<bd_SalesAreaInfoEntity, int>, ISalesAreaInfoService
    {
        public SalesAreaInfoService(IDataValidatorProvider dataValidator,
           IRepository<bd_SalesAreaInfoEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
           dataValidator, repository, mapper, currentUser)
        {
        }
        
        public PagedResult<bd_SalesAreaInfoDto> GetPagedList(bd_SalesAreaInfoSearchDto input)
        {
            var list = Repository.Select.From< UserEntity, UserEntity, UserEntity>((s, c, c2, c3) => s
                 .LeftJoin(t => t.AreaGM == c.Id)
                 .LeftJoin(t => t.AreaCharger == c2.Id)
                 .LeftJoin(t => t.AreaSalesman == c3.Id))
            .WhereIf(input.AreaCode.NotNullOrWhiteSpace(), t => t.t1.AreaCode.Contains(input.AreaCode))
            .WhereIf(input.AreaName.NotNullOrWhiteSpace(), t => t.t1.AreaName.Contains(input.AreaName))
            .OrderByDescending(t => t.t1.CreatedAt)
            .Count(out var total).Page(input.PageNo, input.PageSize)
            .ToList((s, c, c2, c3) => new
            {
                sales = s,
                areaGM = c.RealName,
                areaCharger = c2.RealName,
                areasalesman = c3.RealName,
            })
            .Select(t =>
            {
                var output = Mapper.Map<bd_SalesAreaInfoDto>(t.sales);
                output.AreaGMText = t.areaGM;
                output.AreaChargerText = t.areaCharger;
                output.AreaSalesmanText = t.areasalesman;
                return output;
            }).ToList();
            return new PagedResult<bd_SalesAreaInfoDto>(Mapper.Map<List<bd_SalesAreaInfoDto>>(list), total);
        }

        public bool IsExist(bd_SalesAreaInfoDto input)
        {
            var exist = Repository.Where(t => t.AreaCode == input.AreaCode || t.AreaName == input.AreaName).ToOne();
            if (exist == null) return false;
            if (input.Id.HasValue)
            {
                return input.Id.Value != exist.Id;
            }
            return true;
        }

        public List<SalesAreaInfoSelectDto> GetSalesAreaSelect()
        {
            var list = Repository.Select.From<ContactsEntity, ContactsEntity, ContactsEntity>((s,c, c2, c3) => s
                 .LeftJoin(t => t.AreaGM == c.Id)
                 .LeftJoin(t => t.AreaCharger == c2.Id)
                 .LeftJoin(t => t.AreaSalesman == c3.Id))
            .OrderByDescending(t => t.t1.CreatedAt)
            .ToList((s, c, c2, c3) => new
            {
                sales = s,
                areaGM = c.ContactsName,
                areaCharger = c2.ContactsName,
                areasalesman = c3.ContactsName,
            })
            .Select(t =>
            {
                var output = Mapper.Map<bd_SalesAreaInfoDto>(t.sales);
                output.AreaGMText = t.areaGM;
                output.AreaChargerText = t.areaCharger;
                output.AreaSalesmanText = t.areasalesman;
                return output;
            }).ToList();
            List<SalesAreaInfoSelectDto> returnList = new List<SalesAreaInfoSelectDto>();
            foreach (var item in list)
            {
                returnList.Add(new SalesAreaInfoSelectDto {Id=item.Id,AreaNameAndPlaceName=item.AreaName });
            }
            return returnList;
        }
    }
}
