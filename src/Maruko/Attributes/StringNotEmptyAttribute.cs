using System.ComponentModel.DataAnnotations;

namespace Maruko.Core.Attributes
{
    public class StringNotEmptyAttribute : RequiredAttribute {
        public override bool IsValid(object value)
        {
            return !string.IsNullOrWhiteSpace(value?.ToString()?.Trim());
        }
    }
}
