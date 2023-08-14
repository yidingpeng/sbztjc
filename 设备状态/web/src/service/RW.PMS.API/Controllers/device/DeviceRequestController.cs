using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RW.PMS.Application.Contracts.System;
using System.Net;
using System.Text;
using System.Xml;

namespace RW.PMS.API.Controllers.device
{
    [AllowAnonymous]
    public class DeviceRequestController : RWBaseController
    {
        IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public readonly ILogService _log;
        public DeviceRequestController(IHttpClientFactory httpClientFactory, IConfiguration configuration,ILogService logService)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _log = logService;
        }

        /// <summary>
        /// 封装使用HttpClient调用WebService
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <param name="content">参数</param>
        /// <returns></returns>
        private async Task<string> PostHelper(string url, HttpContent content)
        {
            var result = string.Empty;
            try
            {
                using (var client = _httpClientFactory.CreateClient())
                using (var response = await client.PostAsync(url, content))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        result = await response.Content.ReadAsStringAsync();

                        //XmlDocument doc = new XmlDocument();
                        //doc.LoadXml(result);
                        //result = doc.InnerText;
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }


        [HttpPost("RequestMESDevice")]
        public async Task<string> RequestMESDevice()
        {
            string strResult = "";
            string TokenName = "";
            string Token = "";
            int TimeOut = 3;
            try
            {
                Request.EnableBuffering();

                string body = "";
                var stream = Request.Body;
                if (stream != null)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true))
                    {
                        body = await reader.ReadToEndAsync();
                    }
                    stream.Seek(0, SeekOrigin.Begin);
                }
              
                //System.IO.Stream sm = Request.ReadFromJsonAsync;// .Content.ReadAsStreamAsync().Result;
                //int len = (int)sm.Length;
                //byte[] inputByts = new byte[len];
                //sm.Read(inputByts, 0, len);
                //sm.Close();
                //string request = Encoding.GetEncoding("utf-8").GetString(inputByts);
                // url地址格式：WebService地址+方法名称     
                // WebService地址：http://www.webxml.com.cn/WebServices/WeatherWebService.asmx
                // 方法名称： getSupportCity
                string url = _configuration["apiUrl:csapiUrl"];
                _log.AddOperationLog(true, "地址",url);
                // 参数
                //Dictionary<string, string> dicParam = new Dictionary<string, string>();
                ////参数名
                //dicParam.Add("JSONROOT", body);
                //// 将参数转化为HttpContent
                //HttpContent content = new FormUrlEncodedContent(dicParam);
                //strResult = await PostHelper(url, content);
                var client = new RestClient(url);//请求路劲
                var request = new RestRequest("",Method.Post);//请求方式
                request.RequestFormat = DataFormat.Xml;
                request.Timeout = TimeOut * 1000;//超时时间
                request.AddHeader("Content-Type", "application/json; charset=utf-8");//请求编码
                //request.AddHeader("Content-Type", "text/xml; charset=utf-8");
                if (Token != "")
                {
                    client.AddDefaultHeader(TokenName, Token);
                }
                //request.AddBody(content);
                //client.ThrowOnAnyError = true;//强制抛出错误
                request.AddParameter("text/xml", "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tem=\"http://tempuri.org/\">\r\n\r\n   <soapenv:Header/>\r\n\r\n   <soapenv:Body>\r\n\r\n      <tem:OPC_Send_Data_To_Mes>\r\n\r\n         <!--Optional:-->\r\n\r\n         <tem:Request>\r\n\r\n            " +body+ "  </tem:Request>\r\n\r\n      </tem:OPC_Send_Data_To_Mes>\r\n\r\n   </soapenv:Body>\r\n\r\n</soapenv:Envelope>\r\n", ParameterType.RequestBody);
                RestResponse restResponse = client.Execute(request);
                _log.AddOperationLog(true, "接口返回数据", restResponse.StatusCode.ToString());
                return restResponse.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return "1111";
        }
    }
}
