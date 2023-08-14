using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.File;
using RW.PMS.Application.Contracts.DTO.Workflow;
using RW.PMS.Domain.Entities.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Service
{
    public interface IUserWorkflowService : ICrudApplicationService<UserFlowEntity, int>
    {
        PagedResult<UserFlowListDto> GetUserFlowPagedList(UserFlowQueryDto input);

        /// <summary>
        /// 生成流程编号
        /// </summary>
        string GenerateNo();
        UserFlowModelDto GetUserFlow(int id);
        bool AddMine(AddUserFlowDto input);
        bool ModifyMine(int id, EditUserFlowDto input);

        bool RemoveMine(int id);
        /// <summary>
        /// 撤销我的流程
        /// </summary>
        bool CancelMine(int id);
        bool AuditFlow(AuditSubmitDto dto);
        /// <summary>
        /// 获取我的附件
        /// </summary>
        List<FileListDto> GetMyFiles(FileQueryDto query);
        /// <summary>
        /// 流程催办
        /// </summary>
        bool Urging(int id);
    }
}
