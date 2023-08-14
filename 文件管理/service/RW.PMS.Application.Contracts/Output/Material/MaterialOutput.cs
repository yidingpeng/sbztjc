using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Output.Material
{
    public class MaterialOutput
    {
        public int cid { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string alias { get; set; }
        public string model { get; set; }
        public string specification { get; set; }
        public string material { get; set; }
        public string basicUnit { get; set; }
        public string source { get; set; }
        public string important { get; set; }
        public string codingStatus { get; set; }
        public decimal weight { get; set; }
        public string weightUnit { get; set; }
        public string categoryPath { get; set; }
        public string categoryMaster { get; set; }
        public string categoryLast { get; set; }
        public string remark { get; set; }
        public string createTime { get; set; }
        public decimal refPrice { get; set; }
        public string priceUnit { get; set; }
    }
}
