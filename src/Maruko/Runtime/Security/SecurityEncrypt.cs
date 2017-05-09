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
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(str));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }
    }
}
