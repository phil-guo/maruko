using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Maruko.AspNetMvc.Service
{
    public static class Utils
    {
        public static string GetTimeSpan()
        {
            var startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, 0), TimeZoneInfo.Local);
            long time = (DateTime.Now.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位
            return time.ToString().Substring(0, 13);
        }

        /// <summary>
        /// 列表返回数据转换成字典
        /// </summary>
        /// <typeparam name="T">数据对象</typeparam>
        /// <param name="datas">数据</param>
        /// <param name="total">总条数</param>
        /// <returns></returns>
        public static Dictionary<string, object> DataToDictionary<T>(this T datas, int total)
        {
            return new Dictionary<string, object>()
            {
                { "datas",datas},
                { "total",total}
            };
        }

        /// <summary>
        /// 返回对象列表数据转换成字典
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="data">数据对象</param>
        /// <param name="total">总条数</param>
        /// <param name="names">排除或包含参数列表</param>
        /// <param name="ispc">是否排除(true排除false包含)</param>
        /// <returns></returns>
        public static Dictionary<string, object> DataToDictionary<T>(this List<T> datas, int total, List<string> names = null, bool ispc = true)
        {
            List<Dictionary<string, object>> zhlist = new List<Dictionary<string, object>>();
            foreach (var item in datas)
                zhlist.Add(item.DataToDictionary(names, ispc));
            return new Dictionary<string, object>()
            {
                { "Datas",zhlist},
                { "Total",total}
            };
        }

        /// <summary>
        /// 返回对象数据转换成字典
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="data">数据对象</param>
        /// <param name="names">排除或包含参数列表</param>
        /// <param name="ispc">是否排除(true排除false包含)</param>
        /// <returns></returns>
        public static Dictionary<string, object> DataToDictionary<T>(this T data, List<string> names = null, bool ispc = true)
        {
            Dictionary<string, object> te = new Dictionary<string, object>();
            var pt = data.GetType();
            foreach (var item in pt.GetProperties())
            {
                if (names != null && names.Count > 0)
                {
                    if (ispc)
                    {
                        if (Array.IndexOf(names.ToArray(), item.Name) < 0)
                            data.SetProperty(item, ref te);
                    }
                    else
                    {
                        if (Array.IndexOf(names.ToArray(), item.Name) >= 0)
                            data.SetProperty(item, ref te);
                    }
                }
                else
                    data.SetProperty(item, ref te);
            }
            return te;
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        private static void SetProperty<T>(this T data, System.Reflection.PropertyInfo info, ref Dictionary<string, object> ret)
        {
            object value;
            if (info.PropertyType.FullName == "System.DateTime")
                value = DateTime.Parse(info.GetValue(data).ToString()).ToString("yyyy-MM-dd hh:mm:ss");
            else
                value = info.GetValue(data);
            ret.Add(info.Name, value);
        }

        /// <summary>
        /// 字典对象转换成指定实体对象
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="t">实体对象</param>
        /// <param name="obj">字典对象</param>
        public static T DictionaryToT<T>(this T t, Dictionary<string, object> obj)
        {
            var pt = t.GetType();
            foreach (var item in pt.GetProperties())
            {
                if (obj.ContainsKey(item.Name))
                {
                    var node = obj[item.Name];
                    item.SetValue(t, Convert.ChangeType(node, item.PropertyType));
                }
            }
            return t;
        }

        /// <summary>
        /// 保留N位小数，第N+1位四舍五入
        /// </summary>
        /// <param name="value">待处理值</param>
        /// <param name="length">保留位数(默认三位)</param>
        /// <returns></returns>
        public static decimal ConvertToDecimal_RoundingOff(this decimal value, int length = 3)
        {
            return Math.Round(value, length, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 保留N位小数，第N+1位无条件向上进位
        /// </summary>
        /// <param name="value">待处理值</param>
        /// <param name="length">保留位数(默认三位)</param>
        /// <returns></returns>
        public static decimal ConvertToDecimal(this decimal value, int length = 3)
        {
            string str = "1";
            for (int i = 0; i < Math.Abs(length); i++)
            {
                str += "0";
            }
            int lengvalue = int.Parse(str);
            return Math.Ceiling(value * lengvalue) / lengvalue;
        }

        /// <summary>
        /// 生成用户邀请码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="source_string">//自定义乱序字符串,‘0-9 A-Z’,剔除易混淆字符‘I，O’，剔除高位补‘0’  </param>
        /// <param name="len">code偶数位</param>
        /// <returns></returns>
        public static string BuildCode(this int uid, int len = 6, string source_string = "2YU9IP6ASDFG8QWERTHJ7KLZX4CV5B3ONM1")
        {
            string code = "";
            int mod = 0;
            StringBuilder sb = new StringBuilder();
            while (uid > 0)
            {
                mod = uid % 35;
                uid = (uid - mod) / 35;
                code = source_string.ToCharArray()[mod] + code;
            }
            return code.PadRight(len, '0');//不足六位补0  
        }

        /// <summary>
        /// 邀请码解析分用户id
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static int Decode(this string code, string source_string = "2YU9IP6ASDFG8QWERTHJ7KLZX4CV5B3ONM1")
        {
            code = new string((from s in code where s != '0' select s).ToArray());
            int num = 0;
            for (int i = 0; i < code.ToCharArray().Length; i++)
            {
                for (int j = 0; j < source_string.ToCharArray().Length; j++)
                {
                    if (code.ToCharArray()[i] == source_string.ToCharArray()[j])
                    {
                        num += j * Convert.ToInt32(Math.Pow(35, code.ToCharArray().Length - i - 1));
                    }
                }
            }
            return num;
        }

        /// <summary>
        /// 将集合key以ascii码从小到大排序
        /// </summary>
        /// <param name="sArray">待排序字典</param>
        /// <returns></returns>
        public static Dictionary<string, string> AsciiDictionary(this Dictionary<string, string> sArray)
        {
            Dictionary<string, string> asciiDic = new Dictionary<string, string>();
            string[] arrKeys = sArray.Keys.ToArray();
            Array.Sort(arrKeys, string.CompareOrdinal);
            foreach (var key in arrKeys)
            {
                string value = sArray[key];
                asciiDic.Add(key, value);
            }
            return asciiDic;
        }

        /// <summary>
        /// 将集合key以ascii码从小到大排序
        /// </summary>
        /// <param name="sArray">待排序字典</param>
        /// <returns></returns>
        public static string AsciiString(this Dictionary<string, string> sArray)
        {
            string[] arrKeys = sArray.Keys.ToArray();
            Array.Sort(arrKeys, string.CompareOrdinal);
            StringBuilder asciistr = new StringBuilder();
            foreach (var key in arrKeys)
            {
                asciistr.Append($"{key}={sArray[key]}&");
            }
            return asciistr.ToString().Substring(0, asciistr.Length - 1);
        }

        #region 枚举帮助方法
        /// <summary>
        /// 获取枚举的显示特性
        /// </summary>
        /// <param name="enum">枚举参数</param>
        /// <returns></returns>
        public static DisplayAttribute GetDisplay(this Enum @enum)
        {
            return @enum.GetType().GetMember(@enum.ToString()).First().GetCustomAttribute<DisplayAttribute>();
        }
        public static DisplayAttribute GetDisplay(this Object @enum)
        {
            return @enum.GetType().GetMember(@enum.ToString()).First().GetCustomAttribute<DisplayAttribute>();
        }

        /// <summary>
        /// 查询枚举是否存在
        /// </summary>
        /// <param name="@enum">枚举类</param>
        /// <param name="ostr">待判断串</param>
        /// <param name="nstr">检查后的有效串(','分割)</param>
        /// <param name="separator">原串分隔符</param>
        /// <param name="varr">枚举值数组</param>
        /// <param name="nseparator">检查后的有效串分隔符(默认',')</param>
        /// <returns>是否存在有效值</returns>
        public static bool IsEnumValue(this Type @enum, string ostr, out string nstr, string[] separator = null, int[] varr = null, string nseparator = ",")
        {
            bool flag = false;
            string[] arr;
            List<string> narr = new List<string>();
            nstr = "";
            if (separator != null && separator.Length > 0)
                arr = ostr.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            else
                arr = new string[] { ostr };
            foreach (var item in arr)
            {
                if (int.TryParse(item, out int tint))
                {
                    if (@enum.IsEnumDefined(tint))
                    {
                        if (varr != null && varr.Length > 0)
                        {
                            if (Array.IndexOf(varr, tint) >= 0)
                            {
                                flag = true;
                                narr.Add(item);
                            }
                        }
                        else
                        {
                            flag = true;
                            narr.Add(item);
                        }
                    }
                }
            }
            if (flag)
            {
                if (separator != null && separator.Length > 0)
                    nstr = string.Join(nseparator, narr.ToArray());
                else
                    nstr = narr[0];
            }
            return flag;
        }
        #endregion
    }
}
