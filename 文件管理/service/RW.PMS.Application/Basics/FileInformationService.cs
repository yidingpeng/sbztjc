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
    public class FileInformationService : CrudApplicationService<FileInformationEntity, int>, IFileInformationService
    {
        public FileInformationService(IDataValidatorProvider dataValidator,
                           IRepository<FileInformationEntity, int> productionRepository,
                           IMapper mapper,
                           Lazy<ICurrentUser> currentUser) :
          base(dataValidator, productionRepository, mapper, currentUser)
        {
        }

        public PagedResult<FileInformationDto> GetPagedList(FileInformationSearchDto input)
        {
            var list = Repository.Select
                .WhereIf(input.FileName.NotNullOrWhiteSpace(), x => x.FileName.Contains(input.FileName))
                .WhereIf(input.FilePath.NotNullOrWhiteSpace(), x => x.FilePath.Contains(input.FilePath))
                .WhereIf(input.roomName.NotNullOrWhiteSpace(), x => x.FilePath.Contains(input.roomName))
                .WhereIf(input.FileRelativePath.NotNullOrWhiteSpace(), x => x.FileRelativePath.Contains(input.FilePath))
                .WhereIf(input.startTime.NotNullOrWhiteSpace(), x => x.CreatedAt >= Convert.ToDateTime(input.startTime))
                .WhereIf(input.endTime.NotNullOrWhiteSpace(), x => x.CreatedAt <= Convert.ToDateTime(input.endTime))
                .Count(out var total).Page(input.PageNo, input.PageSize).OrderByDescending(t => t.Id).ToList();
            return new PagedResult<FileInformationDto>(Mapper.Map<List<FileInformationDto>>(list), total);
        }

    }
}
