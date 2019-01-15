using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Maruko.Attributes
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
