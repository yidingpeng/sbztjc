using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Model.FileRequst
{
    /// <summary>
    /// 响应实体类
    /// </summary>
    public class FileResponse
    {
        /// <summary>
        /// 响应结果代码:(Y)
        /// OK:成果
        /// NG:失败
        /// </summary>
        public string CODE { get; set; }
        /// <summary>
        /// 结果详细描述(Y)
        /// </summary>
        public string MESSAGE { get; set; }
    }
}
