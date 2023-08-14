using AutoMapper.Configuration.Annotations;
using RW.PMS.Application.Contracts.DTO.File;
using RW.PMS.Domain.Entities.Workflow;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Workflow
{
    public class AuditSubmitDto
    {
        public int FlowId { get; set; }
        public string Comments { get; set; }
        public bool Result { get; set; }

        /// <summary>
        /// 重置节点数量，0表示发起人，1表示上一级，n表示上n级。
        /// 只计算可审核的节点数量；
        /// </summary>
        public int Back { get; set; }

        public FileOutputDto[] Files { get; set; }
    }
}
