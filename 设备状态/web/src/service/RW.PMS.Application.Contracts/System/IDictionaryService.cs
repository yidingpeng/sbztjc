using System.Collections.Generic;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.Input.System;
using RW.PMS.Application.Contracts.Output.System;
using RW.PMS.Domain.Entities.System;

namespace RW.PMS.Application.Contracts.System
{
    /// <summary>
    ///     字典应用服务接口
    /// </summary>
    public interface IDictionaryService : ICrudApplicationService<DictionaryEntity, int>
    {
        DictionaryView GetView(int id);

        /// <summary>
        ///     获取顶级字典分页数据
        /// </summary>
        /// <param name="input"><see cref="DictionarySearchInput" />搜索输入项</param>
        /// <returns></returns>
        PagedResult<DictionaryOutput> GetPagedList(DictionarySearchInput input);

        /// <summary>
        ///     根据查询条件获取数据
        /// </summary>
        /// <param name="input">搜索输入项</param>
        /// <returns></returns>
        IList<DictionaryOutput> GetList(DictionarySearchInput input);


        /// <summary>
        ///     根据查询条件获取子项数据
        /// </summary>
        /// <param name="value">父级字典值</param>
        /// <returns></returns>
        IList<DictionaryOutput> GetSubItemList(string value);

        /// <summary>
        ///     根据查询条件获取字典分页数据
        /// </summary>
        /// <param name="value">父级字典值</param>
        /// <returns></returns>
        PagedResult<DictionaryOutput> GetPageList(DictionarySearchInput input);

        /// <summary>
        ///     根据查询条件获取字典数据
        /// </summary>
        /// <param name="dictionaryValue">父级字典值</param>
        /// <returns></returns>
        List<DictionaryAppOutput> GetDictionaryList(string dictionaryValue);

        /// <summary>
        /// 数据字典名称是否重复
        /// </summary>
        /// <param name="input"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        bool DictionaryTextExist(DictionaryInput input, string type);

        int GetMaxCode();

        /// <summary>
        /// 根据id删除数据及数据子级
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        int DeleteAndChildren(int[] ids);

        /// <summary>
        /// 获取键值对列表
        /// </summary>
        List<KeyValueDto> GetKeyValue(string type);
        /// <summary>
        /// 获取所有的字典值
        /// 转成json为：{类型:[{key:key1,value:value1},{key:key2,value:value2}]}
        /// </summary>
        Dictionary<string, List<KeyValueDto>> GetAll();
        string GetCacheValue(string type, string key, string defaultValue = null);
    }
}