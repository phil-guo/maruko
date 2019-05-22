using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Maruko.Utils
{
    public static class ContainerManager
    {
        public static IContainer Current { get; set; }
    }
}
