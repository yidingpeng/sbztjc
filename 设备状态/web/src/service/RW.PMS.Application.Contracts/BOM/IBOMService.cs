using RW.PMS.Application.Contracts.BOM.DTO;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Domain.Entities.BOM;
using RW.PMS.Domain.Entities.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.BOM
{
    public interface IBOMService : ICrudApplicationService<BomTableEntity, int>
    {
        PagedResult<BOMTableDto> GetBOMList(BOMTableSearchDto search);

        List<BOMItemDto> GetItems(GetBOMItemDto search);

        List<CommentResult> GetCommentList(CommentSearchDto search);
        bool Approve(CommentResult comment);
        BOMDataDto GetBOM(int id);
        bool UndoBOM(int id);
        PagedResult<BOMTableDto> GetMyBOM(MyBomTableSearchDto search);
    }
}
