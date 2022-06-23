using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Maruko.Core.Extensions.Http
{
    public class WebUtilsHttpConnectionPool
    {
        private readonly ConcurrentStack<HttpClient> _clients = new ConcurrentStack<HttpClient>();
        private string _url = string.Empty;
        private int _size = 1;
        private readonly object _lockObject = new object();
        private double _timeout = 60000;

        /// <summary>
        /// 初始化 HttpClient 池
        /// </summary>
        /// <param name="url">目标域名，不带路径 比如 http://www.baidu.com </param>
        /// <param name="num">池里Client数量</param>
        public WebUtilsHttpConnectionPool(string url, int num)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException(nameof(url));
            }

            if (num <= 0)
            {
                throw new ArgumentException(nameof(num));
            }
            _clients.Clear();
            _url = url;
            _size = num;

        }

        public HttpClient GetClient()
        {
            if (_clients.TryPop(out var client))
            {
                return client;
            }
            else
            {
                var newClient = new HttpClient()
                { BaseAddress = new Uri(_url), Timeout = TimeSpan.FromMilliseconds(_timeout) };
                //newClient.DefaultRequestHeaders.Add("User-Agent", $"{ConfiguraString.EQuality}");
                newClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
                return newClient;
            }
        }

        public void ReturnClient(HttpClient client)
        {
            lock (_lockObject)
            {
                if (_clients.Count >= _size)
                {
                    client.Dispose();
                }
                else
                {
                    _clients.Push(client);
                }
            }
        }


        /// <summary>
        /// post 请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <param name="url"></param>
        /// <param name="heads"></param>
        /// <returns>Tuple<string,T> string 返回流内容,T返回对象</returns>
        public async Task<Tuple<string, T>> PostAsync<T>(string url = "", object parameters = null, Dictionary<string, string> heads = null) where T : class, new()
        {
            string innerUrl = _url;
            if (!string.IsNullOrEmpty(url))
                innerUrl = url;

            T t = new T();
            string message = string.Empty;
            using (var client = GetClient())
            {
                string content = string.Empty;
                if (parameters != null)
                    content = JsonConvert.SerializeObject(parameters);
                if (heads != null)
                {
                    foreach (var item in heads)
                    {
                        if (!client.DefaultRequestHeaders.Contains(item.Key))
                            client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var resultHttpPost = await client.PostAsync(innerUrl, new StringContent(content, Encoding.UTF8, "application/json"));
                    var interfaceresult = await resultHttpPost.Content.ReadAsStringAsync();
                    if (resultHttpPost.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        t = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(interfaceresult);
                    }
                    else
                    {
                        t = new T();
                    }
                    message = interfaceresult;
                }
                catch (Exception exe)
                {
                    message = exe.Message;
                }
                if (t == null)
                    t = new T();

            }
            Tuple<string, T> tuple = Tuple.Create(message, t);

            return tuple;
        }
        /// <summary>
        /// get 请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <param name="url"></param>
        /// <param name="charset"></param>
        /// <param name="heads"></param>
        /// <returns></returns>
        public async Task<Tuple<string, T>> GetAsync<T>(string url = "", Dictionary<string, string> parameters = null, Dictionary<string, string> heads = null, string charset = "utf-8") where T : class, new()
        {
            string innerUrl = _url;
            if (!string.IsNullOrEmpty(url))
                innerUrl = url;

            T t = new T();

            string message = string.Empty;
            using (var client = GetClient())
            {
                if (heads != null)
                {
                    foreach (var item in heads)
                    {
                        if (!client.DefaultRequestHeaders.Contains(item.Key))
                            client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                if (parameters != null && parameters.Count > 0)
                    if (innerUrl.Contains("?"))
                        innerUrl = innerUrl + "&" + BuildQuery(parameters, charset);
                    else
                        innerUrl = innerUrl + "?" + BuildQuery(parameters, charset);

                var query = new Uri(innerUrl).Query;
                try
                {
                    var resultHttpPost = await client.GetAsync(query);
                    var interfaceresult = await resultHttpPost.Content.ReadAsStringAsync();
                    t = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(interfaceresult);

                    if (resultHttpPost.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        t = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(interfaceresult);
                    }
                    else
                    {
                        t = new T();
                    }
                    message = interfaceresult;
                }
                catch (Exception exe)
                {
                    message = exe.Message;
                }
                if (t == null)
                    t = new T();
            }
            Tuple<string, T> tuple = Tuple.Create(message, t);
            return tuple;
        }
        public static string BuildQuery(IDictionary<string, string> parameters, string charset)
        {
            var postData = new StringBuilder();
            var hasParam = false;

            using (var dem = parameters.GetEnumerator())
            {
                while (dem.MoveNext())
                {
                    var name = dem.Current.Key;
                    var value = dem.Current.Value;
                    // 忽略参数名或参数值为空的参数
                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                    {
                        if (hasParam)
                            postData.Append("&");

                        postData.Append(name);
                        postData.Append("=");

                        var encodedValue = HttpUtility.UrlEncode(value, Encoding.GetEncoding(charset));

                        postData.Append(encodedValue);
                        hasParam = true;
                    }
                }

                return postData.ToString();
            }

        }
    }
}
