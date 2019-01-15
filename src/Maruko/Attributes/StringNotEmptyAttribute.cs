using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Maruko.Attributes
{
    public class StringNotEmptyAttribute : RequiredAttribute {
        public override bool IsValid(object value)
        {
            return !string.IsNullOrWhiteSpace(value?.ToString()?.Trim());
        }
    }
}
