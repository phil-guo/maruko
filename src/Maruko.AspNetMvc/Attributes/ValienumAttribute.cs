using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Maruko.AspNetMvc.Attributes
{
    public class ValienumAttribute: ValidationAttribute
    {
        public ValienumAttribute()
        {
        }

        public Type AllowEnum { get; set; }

        public bool AllowMinus { get; set; } = false;
        public bool AllowZero { get; set; } = false;

        public override bool IsValid(object value)
        {
            if (AllowMinus&&Convert.ToInt32(value) <0)
                return true;
            else if (Convert.ToInt32(value) == 0)
            {
                if (AllowZero)
                    return true;
                else
                {
                    ErrorMessage = "不允许有0的枚举值";
                    return false;
                }
            }
            else
            {
                if (AllowEnum == null)
                {
                    ErrorMessage = "请先设置验证枚举对象";
                    return false;
                }
                return AllowEnum.IsEnumDefined(value);
            }
        }
    }
}
