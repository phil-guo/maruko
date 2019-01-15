using System;
using System.Security.Cryptography;
using System.Text;

namespace Maruko.Runtime.Security
{
    /// <summary>
    /// 安全加密
    /// </summary>
    public static class SecurityEncrypt
    {
        /// <summary>
        /// md5加密
        /// </summary>
        public static string Md5Encrypt(this string str)
        {
            //using (var md5 = MD5.Create())
            //{
            //    var result = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            //    var strResult = BitConverter.ToString(result);
            //    return strResult.Replace("-", "");
            //}
            return str.Get32MD5One();
        }

        /// <summary>
        /// 此代码示例通过创建哈希字符串适用于任何 MD5 哈希函数 （在任何平台） 上创建 32 个字符的十六进制格式哈希字符串
        /// 官网案例改编
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Get32MD5One(this string source)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(source));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                string hash = sBuilder.ToString();
                return hash.ToUpper();
            }
        }
    }
}
