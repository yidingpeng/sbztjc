using FreeSql.DataAnnotations;
using RW.PMS.CrossCutting.Exceptions;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.System;

/// <summary>
/// 新闻公告
/// </summary>
[Table(Name = "sys_notice")]
public class SystemNoticeEntity : FullEntity
{
    /// <summary>
    /// 通知类型，0 普通通知，1 紧急通知 2 更新通知 3 公告
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// 通知标题
    /// </summary>
    [MaxLength(200)]
    public string Title { get; set; }
    /// <summary>
    /// 通知内容，HTML格式的内容
    /// 由富文本编辑器创建
    /// </summary>
    [MaxLength(-1)]
    public string Content { get; set; }

    /// <summary>
    /// 阅读人数
    /// </summary>
    public int ReadCount { get; set; }

    /// <summary>
    /// 通知状态，0待发布，1已发布，-1已撤销
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 发送的用户名，以逗号隔开
    /// </summary>
    [MaxLength(500)]
    public string Users { get; set; }

    /// <summary>
    /// 发布
    /// </summary>
    public void Publish()
    {
        Status = 1;
    }

    /// <summary>
    /// 取消
    /// </summary>
    public void Cancel()
    {
        if (Status == 1)
            throw new LogicException(ExceptionCode.Failed, "已发布的通知无法取消");
        Status = -1;
    }

    /// <summary>
    /// 阅读人数+1
    /// </summary>
    public void Read()
    {
        ReadCount++;
    }
}
