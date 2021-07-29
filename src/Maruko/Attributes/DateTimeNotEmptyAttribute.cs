using System;
using System.ComponentModel.DataAnnotations;

namespace Maruko.Core.Attributes
{
    /// <summary>
    /// 时间不能为空
    /// </summary>
    public class DateTimeNotEmptyAttribute : RequiredAttribute {
        public override bool IsValid(object value) {
            return DateTime.TryParse(value?.ToString()??"",out DateTime date) && date!=DateTime.MinValue && date!=DateTime.MaxValue;
        }
    }

    /// <summary>
    /// 时间必须大于或等于当前时间
    /// </summary>
    public class GreaterOrEquealThanNowTimeAttribute : RequiredAttribute {
        public override bool IsValid(object value) {
            return DateTime.TryParse(value?.ToString() ?? "", out DateTime date) && date != DateTime.MinValue && date != DateTime.MaxValue && date>=DateTime.Now;
        }
    }
}
