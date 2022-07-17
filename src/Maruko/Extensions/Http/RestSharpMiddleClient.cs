using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Maruko.Core.Extensions.Http
{
    /// <summary>
    /// 网络请求基础访问类
    /// </summary>
    public class RestSharpMiddleClient : IMiddleClient
    {

        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private WebUtilsHttpConnectionPool _webClient { get; set; }
        Regex validipregex = new Regex(@"(?<![\w@]+)((http|https)://)+([A-Za-z0-9\.-])");
        public RestSharpMiddleClient(ILogger<RestSharpMiddleClient> logger,
            IConfiguration configuration)
        {

            _logger = logger;
            _configuration = configuration;
            _webClient = new WebUtilsHttpConnectionPool("http://127.0.0.1/", 20);
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<T> ExecuteAsync<T>(IMiddleRequest<T> request) where T : MiddleResponse
        {
            T rsp = null;
            string InterfaceUrl = string.Empty;

            if (validipregex.IsMatch(request.UseRoutName))
            {
                InterfaceUrl = request.UseRoutName;
            }
            _logger.LogInformation($"ServiceDomain为空的地址：{InterfaceUrl}");

            switch (request.ServiceTypeEnum)
            {
                case ServiceMethodRequest.Post:

                    using (var client = _webClient.GetClient())
                    {
                        string responseContent = string.Empty;
                        var sendData = JsonConvert.SerializeObject(request.GetDictionary());
                        var content = new StringContent(sendData, Encoding.UTF8, "application/json");
                        content.Headers.ContentType = new MediaTypeHeaderValue($"application/json");
                        try
                        {
                            if (request.AttachToken)
                            {
                                //client.DefaultRequestHeaders.Authorization =
                                //    new AuthenticationHeaderValue("Bearer", IdentityHelper.GetToken());
                            }
                            else { 
                               if(!string.IsNullOrEmpty(request.OverrideToken))
                                    client.DefaultRequestHeaders.Authorization =
                                            new AuthenticationHeaderValue("Bearer", request.OverrideToken);
                            }


                            if(request.DefalutEQualityService)
                                client.DefaultRequestHeaders.Add("x-from", "EQualityService");

                            var resultHttpPost = await client.PostAsync(InterfaceUrl, content);
                            responseContent = await resultHttpPost.Content.ReadAsStringAsync();
                            _logger.LogInformation($"{InterfaceUrl}:接口返回结果：{resultHttpPost.StatusCode}" + responseContent);
                            rsp = JsonConvert.DeserializeObject<T>(responseContent);
                            if (rsp != null)
                                rsp.Body = responseContent;
                        }
                        catch (ArgumentNullException anEx)
                        {
                            var anExMessage = $"路径异常:{InterfaceUrl}:{anEx}";
                            _logger.LogError(anEx, anExMessage);
                            throw new Exception(anExMessage, anEx);
                        }
                        catch (HttpRequestException httpEx)
                        {
                            var httpExMessage = $"路径异常:{InterfaceUrl}:{httpEx}";
                            _logger.LogError(httpEx, httpExMessage);
                            throw new Exception(httpExMessage, httpEx);
                        }
                        catch (Exception exe)
                        {
                            _logger.LogError($"接口返回结果：{InterfaceUrl}:{exe.Message} 堆栈{exe.StackTrace}");
                            if (rsp == null)
                                rsp = Activator.CreateInstance<T>();

                            rsp.Body = responseContent;
                            rsp.ErrorMessage = exe.Message;
                        }
                    }

                    break;
                case ServiceMethodRequest.Get:
                    using (var client = _webClient.GetClient())
                    {
                        var parameters = request.GetDictionary();
                        if (parameters != null && parameters.Count > 0)
                            if (InterfaceUrl.Contains("?"))
                                InterfaceUrl = InterfaceUrl + "&" + WebUtil.BuildQuery(parameters, "utf-8");
                            else
                                InterfaceUrl = InterfaceUrl + "?" + WebUtil.BuildQuery(parameters, "utf-8");

                        string responseContent = string.Empty;
                        try
                        {
                            if (request.AttachToken)
                            {
                                //client.DefaultRequestHeaders.Authorization =
                                //    new AuthenticationHeaderValue("Bearer", IdentityHelper.GetToken());
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(request.OverrideToken))
                                    client.DefaultRequestHeaders.Authorization =
                                            new AuthenticationHeaderValue("Bearer", request.OverrideToken);

                            }

                            if (request.DefalutEQualityService)
                                client.DefaultRequestHeaders.Add("x-from", "EQualityService");

                            var resultHttpPost = await client.GetAsync(InterfaceUrl);
                            responseContent = await resultHttpPost.Content.ReadAsStringAsync();
                            _logger.LogInformation($"{InterfaceUrl}:接口返回结果：{resultHttpPost.StatusCode}" + responseContent);
                            if (resultHttpPost.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                               
                                rsp = JsonConvert.DeserializeObject<T>(responseContent);
                                rsp.Body = responseContent;
                            }
                        }
                        catch (ArgumentNullException anEx)
                        {
                            _logger.LogError(anEx, $"路径异常:{InterfaceUrl}:{anEx}");
                            throw anEx;
                        }
                        catch (HttpRequestException httpEx)
                        {
                            _logger.LogError(httpEx, $"网络异常:{InterfaceUrl}:{httpEx}");
                            throw httpEx;
                        }
                        catch (Exception exe)
                        {
                            _logger.LogError($"接口返回结果：{InterfaceUrl}:{exe.Message}  堆栈{exe.StackTrace}");
                            if (rsp == null)
                                rsp = Activator.CreateInstance<T>();

                            rsp.Body = responseContent;
                            rsp.ErrorMessage = exe.Message;
                        }
                    }

                    break;
                default:
                    break;
            }

            if (rsp == null)
                rsp = Activator.CreateInstance<T>();

            return rsp;
        }
    }
}