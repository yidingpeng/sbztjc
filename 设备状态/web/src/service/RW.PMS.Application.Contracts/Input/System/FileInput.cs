using Microsoft.AspNetCore.Http;

namespace RW.PMS.Application.Contracts.Input.System
{
    public class FileInput
    {
        public IFormFile File { get; set; }

        public string RootPath { get; set; }
    }
}