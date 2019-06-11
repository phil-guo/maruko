using Maruko.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Maruko.AspNetMvc.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = (context.Exception as MarukoException) ?? new MarukoException(context.Exception.Message, ServiceEnum.Failure);

            var response = new ApiReponse<object>
            {
                Msg = exception.Msg,
                Status = exception.Status
            };
            context.Result = new JsonResult(response);
        }
    }
}
