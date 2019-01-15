using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Maruko.Attributes
{
    /// <summary>
    /// 限定最小值
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class MinAttribute : ValidationAttribute{
      

        public MinAttribute(double minimum) {
            Minimum = minimum;
        }
        public MinAttribute(int minimum) {
            Minimum = minimum;
        }

        public object Minimum { get; }
      
        public override bool IsValid(object value)
        {
            bool rc = false;

            if (value != null && double.TryParse(Minimum.ToString(), out double dMinimum))
            {
                double dValue = (int)value;
                if (value.GetType() == typeof(double))
                    dValue = (double)value;
                rc = dValue >= dMinimum;
            }
            
            return rc;
        }
    }
}
