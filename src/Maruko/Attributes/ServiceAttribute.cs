using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.Attributes
{
    public class ServiceAttribute : Attribute
    {
        public string Name { get; set; }
        public string DateTime { get; set; }
        public string Description { get; set; }

        public ServiceAttribute(string name, string description, string dateTime)
        {
            Name = name;
            Description = description;
            DateTime = dateTime;
        }
    }
}
