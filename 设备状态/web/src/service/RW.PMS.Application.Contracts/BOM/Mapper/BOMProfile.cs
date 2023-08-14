using AutoMapper;
using RW.PMS.Application.Contracts.BOM.DTO;
using RW.PMS.Domain.Entities.BOM;

namespace RW.PMS.Application.Contracts.BOM.Mapper
{
    public class BOMProfile : Profile
    {
        public BOMProfile()
        {
            CreateMap<BomTableEntity, BOMTableDto>();
            CreateMap<BOMTableDto, BomTableEntity>();

            CreateMap<BomItemEntity, BOMItemDto>();
            CreateMap<BOMItemDto, BomItemEntity>();

            CreateMap<BOMApprovalLogEntity, CommentListDto>()
                .ForMember(x => x.CommentTime, x => x.MapFrom(a => a.ApprovedTime))
                .ForMember(x => x.Comment, x => x.MapFrom(a => a.Desc))
                .ForMember(x => x.Node, x => x.MapFrom(a => BOMStatusDesc.GetDesc(a.Node).Text))
                ;

        }

    }
}
