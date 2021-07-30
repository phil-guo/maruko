using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.Core.Extensions
{
    public static class ExceptionExtension
    {
        public static void TryCatch(this object obj, Action action, string customerMsg = "")
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                if (!string.IsNullOrEmpty(customerMsg))
                    throw new Exception($"{customerMsg},{e.Message}");
                else
                {
                    throw new Exception($"{e.Message}");
                }
            }
        }

        public static T TryCatch<T>(this object obj, Func<T> action, string customerMsg = "")
        {
            try
            {
                return action();
            }
            catch (Exception e)
            {
                if (!string.IsNullOrEmpty(customerMsg))
                    throw new Exception($"{customerMsg},{e.Message}");
                else
                {
                    throw new Exception($"{e.Message}");
                }
            }
        }
    }
}
