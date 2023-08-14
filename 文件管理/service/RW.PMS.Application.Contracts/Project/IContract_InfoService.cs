using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Input.BaseInfo;
using RW.PMS.Application.Contracts.Output.Project;
using RW.PMS.Domain.Entities.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Project
{
    public interface IContract_InfoService : ICrudApplicationService<Contract_InfoEntity, int>
    {
        /// <summary>
        /// 获取合同信息数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<Contract_InfoDto> GetContractPagedList(Contract_InfoSearchDto input);


        /// <summary>
        /// 判断合同编号是否存在
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //bool IsExist(Contract_InfoDto input);

        /// <summary>
        /// 根据ID获取合同信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<Contract_InfoDto> GetByIdList(int Id);

        /// <summary>
        /// 判断合同编号是否存在
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool IsExist(Contract_InfoDto input);

        /// <summary>
        /// 根据客户名称查询数据
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        List<Contract_InfoDto> GetAllList();

        /// <summary>
        /// 获取合同信息中最新添加的内部合同编号
        /// </summary>
        /// <returns></returns>
        string GetTheLastCtCode();

        /// <summary>
        /// 新增合同项目的时候需要把总金额增加到合同总金额里面
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool EditHTPrice(int id);
    }
}
