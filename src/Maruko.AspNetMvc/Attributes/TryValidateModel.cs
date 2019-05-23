using System.ComponentModel.DataAnnotations;

namespace Maruko.AspNetMvc.Attributes
{
    public class TryValidateNullOrEmptyModel : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                throw new MarukoException(ErrorMessage);
            return true;
        }
    }
}
