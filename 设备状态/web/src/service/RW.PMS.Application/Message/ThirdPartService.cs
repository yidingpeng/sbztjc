using RW.PMS.Application.Contracts.Message;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RW.PMS.Application.Message
{
    public class ThirdPartService: IThirdPartService
    {
        public ThirdPartService()
        {
            
        }
        /// <summary>
        /// 调用API并返回结果字符串（http方式）
        /// </summary>
        /// <param name="url">访问接口全路径，包含参数</param>
        /// <returns>返回JSON字符串</returns>
        public async Task<string> GetRequest(string url)
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    //需要设置SSL访问方式才需要写下面一行代码，如无要求，可以不写
                    //httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    //HTTPClient
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        //client.BaseAddress = new Uri(url);
                        //设置请求体中的内容，并以GET的方式请求
                        var response = await client.GetAsync(url);
                        //获取请求到数据，并转化为字符串
                        var result = response.Content.ReadAsStringAsync().Result;
                        return result;
                    }
                }
            }
            catch (ArithmeticException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 调用API并返回结果字符串（http方式）
        /// </summary>
        /// <param name="url">服务器地址</param>
        /// <param name="param">参数字符串</param>
        /// <returns>返回JSON字符串</returns>
        public async Task<string> PostRequestAsync(string url, object param)
        {
            try
            {
                HttpClient client = new HttpClient();
                
                //client.DefaultRequestHeaders.Add("Method", "Post");
                //client.DefaultRequestHeaders.Add("Authentication", "Basic"+ Convert.ToBase64String(Encoding.UTF8.GetBytes("admin:admin")));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes("admin:admin")));
                JsonSerializerOptions options = new(JsonSerializerDefaults.Web);
                var jsonContent = JsonSerializer.Serialize(param, param.GetType(), options);
                var content = new StringContent(jsonContent);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(url, content);
                var result = response.Content.ReadAsStringAsync().Result;
                return result;
                //using (var httpClientHandler = new HttpClientHandler())
                //{
                //    //需要设置SSL访问方式才需要写下面一行代码，如无要求，可以不写
                //    //httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                //    //HTTPClient
                //    using (var client = new HttpClient(httpClientHandler))
                //    {
                //        string jsonContent = JsonConvert.SerializeObject(param);
                //        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                //        client.DefaultRequestHeaders.Add("Method", "Post");
                //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes("admin:admin")));
                //        //client.BaseAddress = new Uri(url);
                //        //设置请求体中的内容，并以post的方式请求
                //        var response = await client.PostAsync(url, content);
                //        response.EnsureSuccessStatusCode();
                //        //获取请求到数据，并转化为字符串
                //        var result = response.Content.ReadAsStringAsync().Result;
                //        return result;
                //    }
                //}
            }
            catch (ArithmeticException e)
            {
                throw e;
            }
        }
    }
}
