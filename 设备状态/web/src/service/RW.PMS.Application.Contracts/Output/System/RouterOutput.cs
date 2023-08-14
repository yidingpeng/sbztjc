using System.Collections.Generic;
using RW.PMS.Application.Contracts.DTO;

namespace RW.PMS.Application.Contracts.Output.System;

/// <summary>
///     路由输出对象
/// </summary>
public class RouterOutput : TreeList<RouterOutput>
{
    /// <summary>
    ///     首字母大写，一定要与vue文件的name对应起来，name名不可重复，用于noKeepAlive缓存控制（该项特别重要）
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     注意根路由（第一条数据）是斜线，第一级路由必须带斜线，第二级路由开始不能，path名不可重复
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    ///     后端路由时此项为字符串，前端路由时此项为import的function，第一级路由是为Layout，其余为层级为vue的文件路径
    /// </summary>
    public string Component { get; set; }

    /// <summary>
    ///     重定向到子路由，格式从第一级路由开始拼接（默认可不写）
    /// </summary>
    public string Redirect { get; set; }

    public Meta Meta { get; set; } = new Meta();
    /// <summary>
    /// 是否显示
    /// </summary>
    public int IsShow { get; set; }


}

/// <summary>
///     路由元信息
/// </summary>
public class Meta
{
    /// <summary>
    ///     是否显示在菜单中显示隐藏路由
    /// </summary>
    public bool Hidden { get; set; }

    /// <summary>
    ///     是否显示在菜单中显示隐藏一级路由
    /// </summary>
    public bool LevelHidden { get; set; }

    /// <summary>
    ///     菜单、面包屑、多标签页显示的名称
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     图表
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    ///     是否是自定义svg图标（默认值：false，如果设置true，那么需要把你的svg拷贝到icon下，然后icon字段配置上你的图标名）
    /// </summary>
    public bool IsCustomSvg { get; set; }

    /// <summary>
    ///     当前路由是否不缓存（默认值：false）
    /// </summary>
    public bool NoKeepAlive { get; set; }

    /// <summary>
    ///     当前路由是否可关闭多标签页
    /// </summary>
    public bool NoClosable { get; set; }

    /// <summary>
    ///     是否隐藏分栏，仅在分栏布局中二级路由生效
    /// </summary>
    public bool NoColumn { get; set; }

    /// <summary>
    ///     badge小标签（只支持子级，String例行，支持自定义）
    /// </summary>
    public string Badge { get; set; }

    /// <summary>
    ///     当前路由是否不显示多标签页（默认值：false，不推荐使用）
    /// </summary>
    public bool TabHidden { get; set; }

    /// <summary>
    ///     是否浏览新标签页打开（不适用于分栏布局左侧tab部分）
    /// </summary>
    public string Target { get; set; }

    /// <summary>
    ///     高亮指定菜单，要从跟路由的path开始拼接
    /// </summary>
    public string ActiveMenu { get; set; }

    /// <summary>
    ///     小圆点（默认值：false）
    /// </summary>
    public bool Dot { get; set; }

    /// <summary>
    ///     动态传参路由是否新开标签页（默认值：false）
    /// </summary>
    public bool DynamicNewTab { get; set; }

    /// <summary>
    ///     面包屑是否显示（默认值：false）
    /// </summary>
    public bool BreadcrumbHidden { get; set; }
}