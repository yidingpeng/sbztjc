using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.File
{
    public class FileModelDto : FileListDto
    {
        public byte[] Data { get; set; }
    }
}
