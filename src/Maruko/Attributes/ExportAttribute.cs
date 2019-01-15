using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Maruko.Attributes
{
    public class ExportAttribute: DescriptionAttribute
    {
        public ExportAttribute(string name)
        {
            DescriptionValue = name;
        }
    }
}
