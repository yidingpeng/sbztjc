using RW.PMS.Application.Contracts.DTO.System;
using RW.PMS.Domain.Entities.System;

namespace RW.PMS.Application.Contracts.System
{
    public interface IAttachmentUploadService : ICrudApplicationService<AttachmentUploadEntity, int>
    {
        FileReDto Upload(FileDto input);

        void Remove(int id);
    }
}