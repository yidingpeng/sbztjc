using Microsoft.AspNetCore.Http;

namespace RW.PMS.Application.Contracts.DTO.System
{
    public class FileDto
    {
        public IFormFile File { get; set; }

        public string RootPath { get; set; }
    }
}
