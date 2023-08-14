using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Notice;
using RW.PMS.Domain.Entities.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.System
{
    public interface INoticeService : ICrudApplicationService<SystemNoticeEntity, int>
    {
        PagedResult<NoticeOutputDto> GetPagedList(NoticeQueryInputDto input);

        bool Publish(int id);

        bool ReadInform(int id);
    }
}
