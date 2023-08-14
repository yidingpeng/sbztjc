using AutoMapper;
using RW.PMS.Application.Contracts.DTO.Purchase;
using RW.PMS.Domain.Entities.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Mapper
{
    public class PurchaseProfile : Profile
    {
        public PurchaseProfile()
        {
            CreateMap<Mat_OperateEntity, MaterialOperateDto>();
            CreateMap<MaterialOperateDto, Mat_OperateEntity>();

            CreateMap<Mat_SupplierEntity, SupplierDto>();
            CreateMap<SupplierDto, Mat_SupplierEntity>();

            CreateMap<Mat_InventoryEntity, InventoryDto>();
            CreateMap<InventoryDto, Mat_InventoryEntity>();

            CreateMap<Mat_FifoEntity, FifoDto>();
            CreateMap<FifoDto, Mat_FifoEntity>();

            CreateMap<Qr_CodeEntity, QrCodeDto>();
            CreateMap<QrCodeDto, Qr_CodeEntity>();

            CreateMap<IndentPrimaryEntity, IndentPrimaryDto>();
            CreateMap<IndentPrimaryDto, IndentPrimaryEntity>();

            CreateMap<Qr_codePicEntity, Qr_codePicDto>();
            CreateMap<Qr_codePicDto, Qr_codePicEntity>();

            CreateMap<Mat_PurchaseDetailEntity, Mat_PurchaseDetailDto>();
            CreateMap<Mat_PurchaseDetailDto, Mat_PurchaseDetailEntity>();

            CreateMap<Mat_PurchaseOrderEntity, Mat_PurchaseOrderDto>();
            CreateMap<Mat_PurchaseOrderDto, Mat_PurchaseOrderEntity>();

            CreateMap<MatCDEntity, CDDto>();
            CreateMap<CDDto, MatCDEntity>();

            CreateMap<MatQCEntity, QCDto>();
            CreateMap<QCDto, MatQCEntity>();

            CreateMap<Mat_ProductIssuEntity, ProductIssuDto>();
            CreateMap<ProductIssuDto, Mat_ProductIssuEntity>();
        }
    }
}
