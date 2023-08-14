﻿using Microsoft.AspNetCore.Http;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class ContractInfoFilesDto
    {
        public int Pid { get; set; }

        public IFormFile File { get; set; }

        public string RootPath { get; set; }
    }
}
