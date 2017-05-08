using System;

namespace Maruko.Extensions
{
    public static class DoubleExtensions
    {
        /// <summary>
        /// double 类型的数据转化只保留两位小数，但不进行四舍五入
        /// </summary>
        /// <param name="parame"></param>
        /// <returns></returns>
        public static double ConvertToTwoDecimalPlaces(this double parame)
        {
            return Math.Truncate(parame * 100) / 100;
        }
    }
}
