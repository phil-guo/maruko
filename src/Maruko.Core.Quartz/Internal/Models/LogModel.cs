using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.Core.Quartz.Internal.Models
{
    internal class LogModel
    {
        public string StartTime { get; set; } = DateTime.Now.ToString("G");
        public string EndTime { get; set; }
        public double UseTime { get; set; }
        public string RequestUrl { get; set; }
        public string RequestType { get; set; }
        public string Result { get; set; }
        public string ErrorMsg { get; set; }
        public bool IsError { get; set; } = false;
        public string Body { get; set; }
    }
}
