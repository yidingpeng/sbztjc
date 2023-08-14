using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Material
{
    /// <summary>
    /// 物料分类
    /// </summary>
    [Table(Name = "v_pdm_materialsort", DisableSyncStructure = true)]
    public class MaterialSortEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int Crid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Fcrid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Uid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PramID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Dname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Dcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Fid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Start { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Suid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Cdatatime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Adatatime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Exp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Tuhao { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Caizhi { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Zzsx { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Lsm { get; set; }

        /// <summary>
        /// 父级
        /// </summary>
        [Navigate(nameof(Fid))]
        public MaterialSortEntity Parent { get; set; }
        /// <summary>
        /// 子集
        /// </summary>
        [Navigate(nameof(Fid))]
        public List<MaterialSortEntity> Childs { get; set; }

    }
}
