using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Post;
using RW.PMS.Application.Contracts.Service;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Services
{
    public class PostService : CrudApplicationService<PostEntity, int>, IPostService
    {
        public PostService(
            IDataValidatorProvider dataValidator, IRepository<PostEntity, int> repository, IMapper mapper,
            Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
        }

        public PagedResult<PostListDto> GetPagedList(PostQueryDto input)
        {
            var lst = Repository.Select
                 .From<OrganizationEntity>((join, org) => join.LeftJoin(post => post.OrgId == org.Id))
                 .WhereIf(!string.IsNullOrEmpty(input.Key), x => x.t1.PostName.Contains(input.Key) || x.t2.OrganizationName.Contains(input.Key))
                 .Count(out long total)
                 .Page(input.PageNo, input.PageSize)
                 .ToList(x => new { x.t1, x.t2.OrganizationName })
                 .Select(x =>
                 {
                     var dto = Mapper.Map<PostListDto>(x.t1);
                     dto.OrgName = x.OrganizationName;
                     return dto;
                 })
                 .ToList()
                 ;

            return new PagedResult<PostListDto>(lst, total);

        }
    }
}
