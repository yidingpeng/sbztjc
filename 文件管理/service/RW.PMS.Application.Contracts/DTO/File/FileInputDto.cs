using Microsoft.AspNetCore.Http;
using System.IO;

namespace RW.PMS.Application.Contracts.DTO.File
{
    public class FileInputDto
    {
        //public IFormFile File { get; set; }

        public string Filename { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }

        public string RootPath { get; set; }
    }
}