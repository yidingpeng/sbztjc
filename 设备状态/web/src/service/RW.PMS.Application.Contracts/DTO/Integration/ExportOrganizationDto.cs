using RW.PMS.Application.Contracts.DTO.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Integration
{
    public class ExportOrganizationDto
    {
        public byte[] Data { get; set; }
        public string Filename { get; set; }
    }
}
