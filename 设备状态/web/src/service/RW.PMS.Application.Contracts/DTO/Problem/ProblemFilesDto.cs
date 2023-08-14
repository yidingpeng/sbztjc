using Microsoft.AspNetCore.Http;

namespace RW.PMS.Application.Contracts.DTO.Problem
{
    public class ProblemFilesDto
    {
        public string FileName { get; set; }

        public string RootPath { get; set; }

        public string ContentType { get; set; }
    }
}
