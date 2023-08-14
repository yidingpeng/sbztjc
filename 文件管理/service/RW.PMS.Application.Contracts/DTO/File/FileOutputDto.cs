using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.File
{
    public class FileOutputDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }
    }

    public static class FileOutputDtoExtender
    {
        /// <summary>
        /// 将文件数组保存至附件仓库中
        /// </summary>
        /// <param name="files">文件列表</param>
        /// <param name="id">数据ID</param>
        /// <param name="type">数据类型</param>
        /// <param name="attachmentRepo">附件仓库</param>
        public static void SaveTo(this FileOutputDto[] files, int id, string type, IRepository<UserAttachmentEntity, int> attachmentRepo)
        {
            attachmentRepo.Delete(x => x.DataId == id && x.Type == type);
            if (files != null && files.Length > 0)
                attachmentRepo.Insert(files.Select(x => x.ToAttachment(id, type)));
        }

        /// <summary>
        /// 将文件Dto转换成附件实体
        /// </summary>
        public static UserAttachmentEntity ToAttachment(this FileOutputDto dto, int id, string type)
        {
            return new UserAttachmentEntity { DataId = id, FileId = dto.Id, Name = dto.Name, Type = type };
        }
    }
}
