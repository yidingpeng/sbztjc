using System.Collections.Generic;
using FreeSql;
using RW.PMS.Domain.Entities.System;

namespace RW.PMS.Domain.Repositories.System;

/// <summary>
///     功能操作仓储基类
/// </summary>
public interface IOperationRepository : IBaseRepository<OperationEntity>
{
    /// <summary>
    ///     获取全部操作列表
    /// </summary>
    /// <returns></returns>
    List<OperationEntity> GetList();

    /// <summary>
    ///     获取操作列表
    /// </summary>
    /// <param name="moduleId">模块Id</param>
    /// <returns></returns>
    List<OperationEntity> GetList(int moduleId);

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="listData"></param>
    /// <returns></returns>
    bool Edit(List<OperationEntity> listData);

    /// <summary>
    /// 修改一个数据
    /// </summary>
    bool EditOne(OperationEntity entity);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    bool Add(OperationEntity input);
}