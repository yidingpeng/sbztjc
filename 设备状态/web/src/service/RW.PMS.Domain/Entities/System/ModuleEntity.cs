using System;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using RW.PMS.Domain.ValueObject;

namespace RW.PMS.Domain.Entities.System;

//     菜单实体
[Table(Name = "sys_menu", OldName = "Module")]
public class ModuleEntity : FullEntity, ITree<int>, IOrderable
{
    /// <summary>
    ///     模块名称
    /// </summary>
    public string ModuleName { get; set; }

    /// <summary>
    ///     标题
    /// </summary>
    [MaxLength(50)]
    public string Title { get; set; }

    /// <summary>
    /// 是否隐藏
    /// </summary>
    public bool Hidden { get; set; }

    /// <summary>
    ///     图标
    /// </summary>
    [MaxLength(50)]
    public string Icon { get; set; }

    /// <summary>
    ///     打开方式
    /// </summary>
    [MaxLength(20)]
    public string Target { get; set; }

    /// <summary>
    ///     路径
    /// </summary>
    [MaxLength(500)]
    public string Path { get; set; }

    /// <summary>
    ///     组件
    /// </summary>
    public string Component { get; set; }

    /// <summary>
    ///     模块类型
    /// </summary>
    [Column(MapType = typeof(int))]
    public ModuleType ModuleType { get; set; }

    /// <summary>
    ///     排序
    /// </summary>
    [Obsolete("请使用OrderIndex")]
    [Column(IsIgnore = true)]
    public int Sort { get; set; }

    /// <summary>
    ///     说明
    /// </summary>
    [MaxLength(255)]
    public string Description { get; set; }

    /// <summary>
    ///     模块编码
    /// </summary>
    [MaxLength(50)]
    public string ModuleCode { get; set; }

    /// <summary>
    ///     父级菜单Id
    /// </summary>
    public int ParentId { get; set; }

    /// <summary>
    ///     必需模块
    /// </summary>
    public bool Required { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderIndex { get; set; }
}