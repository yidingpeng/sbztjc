using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Post;
using RW.PMS.Domain.Entities.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Service
{
    public interface IPostService : ICrudApplicationService<PostEntity, int>
    {
        PagedResult<PostListDto> GetPagedList(PostQueryDto input);

    }
}
