using System;
using System.Collections.Generic;
using System.Text;
using Hangfire.Dashboard;

namespace Maruko.Hangfire
{
    public class CustomAuthorizeFilter:IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}
