using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Purchase;
using RW.PMS.Application.Contracts.Purchase;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Purchase;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace RW.PMS.Application.Purchase
{
    public class IndentPrimaryService:CrudApplicationService<IndentPrimaryEntity, int>, IindentPrimaryService
    {
        public IndentPrimaryService(IDataValidatorProvider dataValidator,
           IRepository<IndentPrimaryEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
           dataValidator, repository, mapper, currentUser)
        {
        }

        /// <summary>
        /// BOM订单主表分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<IndentPrimaryDto> PagedList(IndentPrimarySearchDto input)
        {
            var list = Repository.Orm.Select<IndentPrimaryEntity>()
                 .WhereIf(input.ProjectCode.NotNullOrEmpty(), a => a.ProjectCode.Contains(input.ProjectCode) || a.ProjectName.Contains(input.ProjectCode))
                 .WhereIf(input.Applicant.NotNullOrEmpty(),a=>a.Applicant.Contains(input.Applicant))
                 .WhereIf(input.ApplicationDate.NotNullOrEmpty(), a => a.ApplicationDate.Contains(input.ApplicationDate))
                 .OrderByDescending(o=>o.Id)
                 .Count(out var total)
                 .Page(input.PageNo, input.PageSize)
                 .ToList();
            return new PagedResult<IndentPrimaryDto>(Mapper.Map<List<IndentPrimaryDto>>(list), total);
        }
        /// <summary>
        /// 根据BomId查询订单主表信息
        /// </summary>
        /// <param name="BomId"></param>
        /// <returns></returns>
        public IndentPrimaryDto ByBomId(int BomId)
        {
            var entity = Repository.Where(o => o.BomId == BomId && o.IsDeleted == false).ToOne();
            return Mapper.Map<IndentPrimaryDto>(entity);
        }
    }
}
