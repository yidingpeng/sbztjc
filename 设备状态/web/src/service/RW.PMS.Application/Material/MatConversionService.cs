using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Material;
using RW.PMS.Application.Contracts.Material;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Material;
using RW.PMS.Domain.Repositories;
using System;
using System.Linq;

namespace RW.PMS.Application.Material
{
    public class MatConversionService : CrudApplicationService<MaterialConversionEntity, int>, IMatConversionService
    {
        public MatConversionService(IDataValidatorProvider dataValidator,
           IRepository<MaterialConversionEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
           dataValidator, repository, mapper, currentUser)
        {
        }
        /// <summary>
        /// 物料换算基础信息查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<MaterialConversionDto> GetHsPageList(MaterialConversionSearchDto input)
        {
            var list = Repository
                .WhereIf(input.Code.NotNullOrWhiteSpace(), o => o.Code.Contains(input.Code))
                .WhereIf(input.Name.NotNullOrWhiteSpace(), o => o.Name.Contains(input.Name))
                .Count(out var total)
                .OrderByDescending(t => t.Id)
                .Page(input.PageNo, input.PageSize).ToList()
                .Select(t => Mapper.Map<MaterialConversionDto>(t)).ToList();
            return new PagedResult<MaterialConversionDto>(list, total);
        }
        /// <summary>
        /// 验重
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsExist(MaterialConversionDto input)
        {
            var exist = Repository.Where(t => t.Code == input.Code).ToOne();
            if (exist == null) return false;
            if (input.Id.HasValue)
            {
                return input.Id.Value != exist.Id;
            }
            return true;
        }
        /// <summary>
        /// 编码查信息
        /// </summary>
        /// <returns></returns>
        public MaterialConversionDto GetByCode(string code)
        {
            var result = Repository.Where(o => o.Code == code).ToOne();
            return Mapper.Map<MaterialConversionDto>(result);
        }
    }
}
