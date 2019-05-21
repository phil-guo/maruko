using Maruko.AspNetMvc.Models;
using Maruko.Configuration;
using Microsoft.Extensions.Configuration;
using NodaTime;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using QRCoder;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ZXing;


namespace Maruko.AspNetMvc
{
    public static class Tools
    {
        private static object _lock = new object();
        #region 系统信息

        /// <summary>
        /// 判断当前是什么系统(Windows Linux OSX)
        /// </summary>
        /// <param name="oS"></param>
        /// <returns></returns>
        public static bool IsOS(OSPlatform oS)
        {
            return RuntimeInformation.IsOSPlatform(oS);
        }

        /// <summary>
        /// 获取系统信息
        /// </summary>
        /// <returns></returns>
        public static string GetOSInfo()
        {
            return $"系统架构：{RuntimeInformation.OSArchitecture};系统名称：{RuntimeInformation.OSDescription};进程架构：{RuntimeInformation.ProcessArchitecture};是否64位操作系统：{Environment.Is64BitOperatingSystem}";
        }

        #endregion

        #region 时间扩展
        /// <summary>
        /// 统一使用IANA时区
        /// </summary>
        /// dotnet core在Windows和Linux上使用的时区不同，在Windows上使用的是Windows time zone IDs，但是在*nix系统上使用的是IANA时区。
        /// <returns></returns>
        public static DateTime GetCstDateTime()
        {
            Instant now = SystemClock.Instance.GetCurrentInstant();
            var shanghaiZone = DateTimeZoneProviders.Tzdb["Asia/Shanghai"];
            return now.InZone(shanghaiZone).ToDateTimeUnspecified();
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime ToCstTime(this DateTime time)
        {
            return GetCstDateTime();
        }

        public static TimeSpan DateDiffTimeSpan(DateTime dateStart, DateTime dateEnd)
        {
            //DateTime start = Convert.ToDateTime(dateStart.ToLongDateString());
            //DateTime end = Convert.ToDateTime(dateEnd.ToLongDateString());

            TimeSpan sp = dateEnd.Subtract(dateStart);

            return sp;
        }

        public static int DateDiff(DateTime dateStart, DateTime dateEnd)
        {
            DateTime start = Convert.ToDateTime(dateStart.ToShortDateString());
            DateTime end = Convert.ToDateTime(dateEnd.ToShortDateString());

            TimeSpan sp = end.Subtract(start);

            return sp.Days;
        }

        #endregion

        /// <summary>
        /// 创建一个(头短字符串+时间串+数字随机数)随机串
        /// </summary>
        /// <param name="headstr">订单头短字符串</param>
        /// <param name="formatdt">时间串格式(请严格按照时间格式化字符串规则)</param>
        /// <param name="length">数字随机数位数</param>
        /// <returns></returns>
        public static string BuidOrderNumber(this string headstr, string formatdt = "yyMMddHHmmss", int length = 4)
        {
            lock (_lock)
                return $"{headstr}{DateTime.Now.ToString(formatdt)}{GenerateRandomCode(length)}";
        }

        /// <summary>
        /// 创建一个随机数(数字)
        /// </summary>
        /// <param name="length">数字随机数位数</param>
        /// <returns></returns>
        public static string GenerateRandomCode(this int length)
        {
            var result = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());
                result.Append(r.Next(0, 10));
            }
            return result.ToString();
        }

        /// <summary>
        /// 从API文档中读取控制器描述
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="moduleList">模块信息</param>
        /// <param name="methodNumDict">Controller中方法数量集合</param>
        /// <returns>所有控制器描述</returns>
        public static ConcurrentDictionary<string, string> GetControllerDesc(this string filepath, HashSet<string> moduleList)
        {
            var controllerDescDict = new ConcurrentDictionary<string, string>();
            //判断文件是否存在
            if (File.Exists(filepath))
            {
                //加载文件
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(filepath);
                string type = String.Empty, path = String.Empty, controllerName = String.Empty;
                string[] arrPath;
                int length = -1, cCount = "Controller".Length;
                XmlNode summaryNode = null;
                //获取member节点列表并循环
                foreach (XmlNode node in xmldoc.SelectNodes("//member"))
                {
                    //获取节点名称值
                    type = node.Attributes["name"].Value;
                    //判断是否类节点(T类、M方法、F参数)
                    if (type.StartsWith("T:"))
                    {
                        //分割字符串
                        arrPath = type.Split('.');
                        length = arrPath.Length;
                        //得到类的名称
                        controllerName = arrPath[length - 1];
                        //判断是否controller
                        if (controllerName.EndsWith("Controller"))
                        {
                            //模块信息
                            var moduleName = arrPath[length - 2];
                            //获取模块名称
                            moduleList.Add(moduleName);

                            //获取控制器注释
                            summaryNode = node.SelectSingleNode("summary");
                            //删除控制器名称中的Controller串得到key
                            string key = controllerName.Remove(controllerName.Length - cCount, cCount);
                            //判断注释不为空，并且集合中没有,就添加到集合中
                            if (summaryNode != null && !String.IsNullOrEmpty(summaryNode.InnerText) && !controllerDescDict.ContainsKey(key))
                                controllerDescDict.TryAdd(key, summaryNode.InnerText.Trim());
                        }
                    }
                }
            }
            return controllerDescDict;
        }

        //银行名称、简称、BIN、卡类型
        static List<BankCard> bankcardlist = new List<BankCard>();

        //卡类型
        static Dictionary<string, string> _cardTypeMap = new Dictionary<string, string>
        {
            {"DC","储蓄卡"},
            { "CC", "信用卡" },
            { "SCC", "准贷记卡" },
            { "PC", "预付费卡" }
        };

        /// <summary>
        /// 验证银行卡是否合法
        /// </summary>
        /// <param name="bcid">卡号</param>
        /// <returns></returns>
        public static bool VerifyBankCard(this string bcid)
        {
            if (string.IsNullOrEmpty(bcid) || !new Regex("^[0-9]+$").Match(bcid).Success || bcid.Length < 16 || bcid.Length > 19)
                //如果传的数据不合法返回N    
                return false;
            char[] chs = bcid.Substring(0, bcid.Length - 1).ToCharArray();
            int luhmSum = 0;
            // 执行luh算法  
            for (int i = chs.Length - 1, j = 0; i >= 0; i--, j++)
            {
                int k = chs[i] - '0';
                if (j % 2 == 0)
                {  //偶数位处理  
                    k *= 2;
                    k = k / 10 + k % 10;
                }
                luhmSum += k;
            }
            return bcid.Substring(bcid.Length - 1).Equals(((luhmSum % 10 == 0) ? 0 : (10 - luhmSum % 10)).ToString());
        }

        /// <summary>
        /// 本地数据判断银行卡BIN信息
        /// </summary>
        /// <param name="bcId">卡号</param>
        /// <returns></returns>
        public static ConcurrentDictionary<string, string> GetLoaclBankCardInfo(this string bcId)
        {
            var dic = new ConcurrentDictionary<string, string>();
            if (!string.IsNullOrEmpty(bcId) && bcId.VerifyBankCard())
            {
                if (bankcardlist.Count == 0)
                    ConfigurationSetting.DefaultConfiguration.GetSection("BankCardList").Bind(bankcardlist);
                foreach (var item in bankcardlist)
                {
                    foreach (var pattern in item.patterns)
                    {
                        if (new Regex(pattern.reg).IsMatch(bcId))
                        {
                            dic.TryAdd("validated", true.ToString());
                            dic.TryAdd("bankCode", item.bankCode);
                            dic.TryAdd("bankName", item.bankName);
                            dic.TryAdd("cardType", pattern.cardType);
                            dic.TryAdd("cardTypeName", _cardTypeMap[pattern.cardType]);
                            break;
                        }
                    }
                    if (dic.ContainsKey("validated"))
                        break;
                }

                if (!dic.ContainsKey("validated"))
                    dic.TryAdd("validated", true.ToString());
            }
            else
                dic.TryAdd("validated", false.ToString());
            return dic;
        }

        /// <summary>
        /// 同步请求get或者post(流请求形式)
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求数据</param>
        /// <param name="encoding">数据字符集设置</param>
        /// <param name="cookie">保留的cookie(没有请传null)</param>
        /// <param name="type">请求类型(1、json；2、xml；其它为网页请求)</param>
        /// <param name="method">请求方式(1、GET；2、POST)</param>
        /// <param name="method">超时时间(默认15000)</param>
        /// <returns></returns>
        public static string GetAndPost(this string url, string data, Encoding encoding, ref CookieContainer cookie, int type = 0,
            int method = 1, int timeout = 15000)
        {
            try
            {
                HttpWebRequest req = WebRequest.CreateHttp(new Uri(url));
                if (type == 1)
                    req.ContentType = "application/json;charset=utf-8";
                else if (type == 2)
                    req.ContentType = "application/xml;charset=utf-8";
                else
                    req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

                if (cookie != null)
                    req.CookieContainer = cookie;
                req.Method = method == 1 ? "GET" : "POST";
                req.Accept = "application/json, text/javascript, */*; q=0.01";
                req.UserAgent =
                    "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.89 Safari/537.36";
                req.ContinueTimeout = timeout;

                if (!string.IsNullOrEmpty(data))
                {
                    Stream reqStream = req.GetRequestStreamAsync().Result;
                    byte[] postData = encoding.GetBytes(data);
                    reqStream.Write(postData, 0, postData.Length);
                    reqStream.Dispose();
                }

                var rsp = (HttpWebResponse)req.GetResponseAsync().Result;
                cookie.Add(rsp.Cookies);
                var result = GetResponseAsString(rsp, encoding);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            Stream stream = null;
            StreamReader reader = null;

            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                reader = new StreamReader(stream, encoding);
                return reader.ReadToEnd();
            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Dispose();
                if (stream != null) stream.Dispose();
                if (rsp != null) rsp.Dispose();
            }
        }


        /// <summary>
        /// 创建一个随机串(英文+数字)
        /// </summary>
        /// <param name="vcodeNum">位数</param>
        /// <returns>英文和数字的随机数字符串</returns>
        public static string CreateRandom(this int vcodeNum)
        {
            //验证码可以显示的字符集合  
            var vChar = @"0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,P,Q,R,S,T,U,V,W,X,Y,Z";

            string[] vcArray = vChar.Split(new Char[] { ',' });//拆分成数组   
            string code = "";//产生的随机数  
            int temp = -1;//记录上次随机数值，尽量避避免生产几个一样的随机数  
            var rand = new Random();
            //采用一个简单的算法以保证生成随机数的不同  
            for (int i = 1; i < vcodeNum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));//初始化随机类  
                }
                int t = rand.Next(61);//获取随机数  
                if (temp != -1 && temp == t)
                {
                    return CreateRandom(vcodeNum);//如果获取的随机数重复，则递归调用  
                }
                temp = t;//把本次产生的随机数记录起来  
                code += vcArray[t];//随机数的位数加一  
            }
            return code;
        }

        /// <summary>
        /// 创建一个图形验证码
        /// </summary>
        /// <param name="code">随机数</param>
        /// <param name="numbers">生成位数</param>
        /// <returns>验证码文件流</returns>
        public static MemoryStream CreateImageValidateCode(out string code, int numbers = 4)
        {
            code = CreateRandom(numbers);

            Bitmap img = null;
            Graphics g = null;
            MemoryStream ms = null;
            Random random = new Random();
            //验证码颜色集合  
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };

            //验证码字体集合
            string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };


            //定义图像的大小，生成图像的实例  
            img = new Bitmap((int)code.Length * 30, 40);

            g = Graphics.FromImage(img);//从Img对象生成新的Graphics对象    

            g.Clear(Color.White);//背景设为白色  

            //在随机位置画背景点  
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(img.Width);
                int y = random.Next(img.Height);
                g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
            }

            //验证码绘制在g中  
            for (int i = 0; i < code.Length; i++)
            {
                int cindex = random.Next(7);//随机颜色索引值  
                int findex = random.Next(5);//随机字体索引值  
                Font f = new Font(fonts[findex], 20, FontStyle.Bold);//字体  
                Brush b = new SolidBrush(c[cindex]);//颜色  
                int ii = 4;
                if ((i + 1) % 2 == 0)//控制验证码不在同一高度  
                {
                    ii = 2;
                }
                g.DrawString(code.Substring(i, 1), f, b, 3 + (i * 28), ii);//绘制一个验证字符  
            }
            ms = new MemoryStream();//生成内存流对象  
            img.Save(ms, ImageFormat.Jpeg);//将此图像以Png图像文件的格式保存到流中  

            //回收资源  
            g.Dispose();
            img.Dispose();
            return ms;
        }

        /// <summary>
        /// 将二维码图像转化为编码串
        /// </summary>
        public static string QrImageToCode(string strSavePath)
        {
            BarcodeReader reader = new BarcodeReader();
            reader.Options.CharacterSet = "UTF-8";
            Bitmap map = new Bitmap(strSavePath);
            Result result = reader.Decode(BitmapByte(map));
            return result == null ? "" : result.Text;

            //QRCodeDecoder decoder = new QRCodeDecoder();
            ////String decodedString = decoder.decode(new QRCodeBitmapImage(pic));
            //var result = decoder.decode(new ThoughtWorks.QRCode.Codec.Data.QRCodeBitmapImage(new System.Drawing.Bitmap(System.Drawing.Image.FromFile(strSavePath))));
            //return result;
        }

        public static byte[] BitmapByte(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }
    }
}
