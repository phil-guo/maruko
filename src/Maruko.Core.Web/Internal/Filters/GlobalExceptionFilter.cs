using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Core.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Maruko.Core.Web
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = new JsonResult(new AjaxResponse<object>(context.Exception.Message, 500));
        }
    }
}
