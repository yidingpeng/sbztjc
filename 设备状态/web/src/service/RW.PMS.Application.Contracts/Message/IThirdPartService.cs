using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Message
{
    public interface IThirdPartService
    {
        /// <summary>
        /// 调用API并返回结果字符串（http方式）
        /// </summary>
        /// <param name="url">访问接口全路径，包含参数</param>
        /// <returns>返回JSON字符串</returns>
        Task<string> GetRequest(string url);

        /// <summary>
        /// 调用API并返回结果字符串（http方式）
        /// </summary>
        /// <param name="url">服务器地址</param>
        /// <param name="param">参数字符串</param>
        /// <returns>返回JSON字符串</returns>
        Task<string> PostRequestAsync(string url, object param);
    }
}
