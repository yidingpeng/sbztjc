using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Domain.Entities.Project;

namespace RW.PMS.Application.Contracts.Project
{
    public interface IClientCompanyService : ICrudApplicationService<Client_CompanyEntity, int>
    {
        //分页
        PagedResult<Client_CompanyDto> GetPagedList(Client_CompanySearchDto input);
        //防止重复编号
        int Disf(string CompanyCode,int id);
        //客户公司名称列表
        PagedResult<Client_CompanyOutput> CompanyList(Client_CompanySearchDto input);
        //业主名称列表

        Client_CompanyOutput GetModel(int? Id);
        /// <summary>
        /// 根据ID查询数据集
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<Client_CompanyDto> GetByIdList(int Id);

    }
}
