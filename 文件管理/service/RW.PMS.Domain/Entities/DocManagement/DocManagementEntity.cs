using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.DocManagement
{
    /// <summary>
    /// 项目文档地址存放表格
    /// </summary>
    [Table(Name = "pro_doc_management", OldName = "DocManagement")]
    public class DocManagementEntity
    {
        /// <summary>
        /// 项目编号_项目名称
        /// </summary>
        [Column(IsPrimary = true)]
        public string ProjectCN { get; set; }

        /// <summary>
        /// 生产员项目文件夹地址
        /// </summary>
        public string FolderAddressSC { get; set; }

        /// <summary>
        /// 管理员项目文件夹地址
        /// </summary>
        public string FolderAddressGL { get; set; }

    }
}
