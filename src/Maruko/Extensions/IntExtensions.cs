using System;

namespace Maruko.Extensions
{
    public static class IntExtensions
    {
        /// <summary>
        /// 生成随机的字母与数字组合的验证码
        /// </summary>
        /// <param name="length">验证码的长度</param>
        /// <returns></returns>
        public static string BulidValidateCode(this int length)
        {
            return BulidStr(length, true);
        }

        private static string BulidStr(int length, bool sleep)
        {
            if (sleep)
                System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }
    }
}
