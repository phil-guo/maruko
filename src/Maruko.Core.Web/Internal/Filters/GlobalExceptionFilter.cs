using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Core.Application;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Maruko.Core.Web.Internal.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var result = new AjaxResponse<object>();

        }
    }
}
