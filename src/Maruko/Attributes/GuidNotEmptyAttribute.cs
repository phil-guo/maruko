using System;
using System.ComponentModel.DataAnnotations;

namespace Maruko.Core.Attributes
{
    public class GuidNotEmptyAttribute:RequiredAttribute
    {
        public override bool IsValid(object value) {
            bool rc = false;
            
            if (Guid.TryParse((value ?? "").ToString(), out Guid iValue))
                rc = iValue !=Guid.Empty;

            return rc;
        }
    }
}
