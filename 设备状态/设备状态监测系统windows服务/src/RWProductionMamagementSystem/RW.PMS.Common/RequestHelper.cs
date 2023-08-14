using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace RW.PMS.Common
{
    public static class RequestHelper
    {
        public static string GetUsername()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public static string GetIP()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }

        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        public static string GetUserAgent()
        {
            return HttpContext.Current.Request.UserAgent;
        }

        public static string GetResponse()
        {
            var resp = HttpContext.Current.Response;
            System.IO.TextWriter w = resp.Output;

            Stream stream = resp.OutputStream;

            stream.ReadByte();


            System.IO.StreamReader reader = new System.IO.StreamReader(stream);
            string text = reader.ReadToEnd();


            reader.Close();
            return text;
        }

        #region 发起请求
        /// <summary>
        /// 发起请求(Object)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content">请求内容</param>
        /// <param name="contentType">application/json</param>
        /// <param name="TimeOut">请求超时时间(秒)</param>
        /// <param name="TokenName">basic用户名</param>
        /// <param name="Token">basic密码</param>
        /// <returns></returns>
        public static string HttpServerRequst(string url, object obj, string contentType, int TimeOut, string TokenName = "", string Token = "")
        {
            try
            {
                var client = new RestClient(url);//请求路劲
                var request = new RestRequest(Method.POST);//请求方式
                request.Timeout = TimeOut * 1000;//超时时间
                client.Encoding = Encoding.UTF8;//请求编码
                if (Token != "")
                {
                    client.Authenticator = new HttpBasicAuthenticator(TokenName, Token);
                }
                //client.ThrowOnAnyError = true;//强制抛出错误
                //request.AddParameter(contentType, content, ParameterType.RequestBody);
                request.AddJsonBody(obj);
                IRestResponse restResponse = client.Execute(request);
                if (restResponse.StatusCode == 0)
                {
                    throw new Exception(restResponse.ErrorMessage);
                }
                return restResponse.Content;
            }
            catch (Exception ex)
            {
                throw new Exception("请求接口失败：" + ex + "！");
            }
        }

        /// <summary>
        /// 发起请求(JSON)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content">请求内容</param>
        /// <param name="contentType">application/json</param>
        /// <param name="TimeOut">请求超时时间(秒)</param>
        /// <returns></returns>
        public static string HttpServerRequstJson(string url, string content, string contentType, int TimeOut, string TokenName = "", string Token = "")
        {
            try
            {
                var client = new RestClient(url);//请求路劲
                var request = new RestRequest(Method.POST);//请求方式
                request.Timeout = TimeOut * 1000;//超时时间
                client.Encoding = Encoding.UTF8;//请求编码
                if (Token != "")
                {
                    client.Authenticator = new HttpBasicAuthenticator(TokenName, Token);
                }
                //client.ThrowOnAnyError = true;//强制抛出错误
                request.AddParameter(contentType, content, ParameterType.RequestBody);
                //request.AddJsonBody(obj);
                IRestResponse restResponse = client.Execute(request);
                if (restResponse.StatusCode == 0)
                {
                    throw new Exception(restResponse.ErrorMessage);
                }
                //var res = "true";
                //var res = "{\"res\": \"true\",\"Message\": \"处理成功！\",\"Result\": \"true\"}";
                return restResponse.Content;
                //return res;
            }
            catch (Exception ex)
            {
                throw new Exception("请求接口失败：" + ex + "！");
            }
        }

        public static string HttpServerRequst(string url, string content, string contentType, int TimeOut)
        {
            try
            {
                var client = new RestClient(url);//请求路劲
                var request = new RestRequest(Method.POST);//请求方式
                request.Timeout = TimeOut * 1000;//超时时间
                client.Encoding = Encoding.UTF8;//请求编码
                //client.ThrowOnAnyError = true;//强制抛出错误
                request.AddParameter(contentType, content, ParameterType.RequestBody);
                IRestResponse restResponse = client.Execute(request);
                return restResponse.Content;
            }
            catch (Exception ex)
            {
                throw new Exception("请求接口失败：" + ex + "！");
            }
        }

        /// <summary>
        /// 发起请求(XML)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <param name="TimeOut"></param>
        /// <returns></returns>
        public static string HttpServerRequstXML(string url, string content, int TimeOut, string TokenName = "", string Token = "")
        {
            try
            {
                var client = new RestClient(url);//请求路劲
                var request = new RestRequest(Method.POST);//请求方式
                request.RequestFormat = DataFormat.Xml;
                request.Timeout = TimeOut * 1000;//超时时间
                client.Encoding = Encoding.UTF8;//请求编码
                if (Token != "")
                {
                    client.Authenticator = new HttpBasicAuthenticator(TokenName, Token);
                }
                //request.AddBody(content);
                //client.ThrowOnAnyError = true;//强制抛出错误
                request.AddParameter("text/xml", content, ParameterType.RequestBody);
                IRestResponse restResponse = client.Execute(request);
                return restResponse.Content;
            }
            catch (Exception ex)
            {
                throw new Exception("请求接口失败：" + ex + "！");
            }
        }

        /// <summary>
        /// Json转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string jsonText)
        {
            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonText));
            T obj = (T)s.ReadObject(ms);
            ms.Dispose();
            return obj;
        }

        /// <summary>
        /// 对象转换成JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJSON<T>(T obj)
        {


            var jsonSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var result = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, jsonSetting);

            return result;
            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            //string result = string.Empty;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    serializer.WriteObject(ms, obj);
            //    ms.Position = 0;

            //    using (StreamReader read = new StreamReader(ms))
            //    {
            //        result = read.ReadToEnd();
            //    }
            //}
            //return result;
        }
        #endregion

    }
}
