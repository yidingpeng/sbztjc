using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.File
{
    public class FileListDto
    {
        public int Id { get; set; }

        public string Filename { get; set; }

        public string ContentType { get; set; }

        public int FileSize { get; set; }

        public string Uploader { get; set; }

        public DateTime UploadTime { get; set; }
    }
}
