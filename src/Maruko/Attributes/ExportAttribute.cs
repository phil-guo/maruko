using System.ComponentModel;

namespace Maruko.Core.Attributes
{
    public class ExportAttribute: DescriptionAttribute
    {
        public ExportAttribute(string name)
        {
            DescriptionValue = name;
        }
    }
}
