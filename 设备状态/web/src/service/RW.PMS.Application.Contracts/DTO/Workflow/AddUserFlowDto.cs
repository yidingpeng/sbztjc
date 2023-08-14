using RW.PMS.Application.Contracts.DTO.File;
using RW.PMS.Domain.Entities.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Workflow
{
    public class AddUserFlowDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 流程标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 流程类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 应用的流程ID
        /// </summary>
        public int WorkflowId { get; set; }

        /// <summary>
        /// 流水号，根据类型+日期 自动生成
        /// </summary>
        //public string SN { get; set; }

        /// <summary>
        /// 是否立即发起
        /// </summary>
        public bool AutoPublish { get; set; }

        /// <summary>
        /// 工作流详情
        /// </summary>
        public JsonNode Detail { get; set; }

        public IdNameDto[] SendUsers { get; set; }

        public void SetUsers(int[] ids, string[] names)
        {
            var users = new IdNameDto[ids.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                users[i] = new IdNameDto();
                users[i].Id = ids[i];
                users[i].Name = names[i];
            }
            this.SendUsers = users;
        }

        /// <summary>
        /// 流程附件
        /// </summary>
        public FileOutputDto[] Files { get; set; } = new FileOutputDto[0];
    }
}
