using System.Linq;
using Maruko.Application;
using Maruko.AspNetMvc.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Maruko.AspNetMvc.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                var ajaxReponse = new ApiReponse<object>
                {
                    Status = ServiceEnum.Failure
                };

                var itemMsg = actionContext.ModelState.Values.SingleOrDefault(item => item.ValidationState == ModelValidationState.Invalid);

                ajaxReponse.Msg = itemMsg?.Errors.Where(item => !string.IsNullOrEmpty(item.ErrorMessage)).Take(1)
                                      .SingleOrDefault()?.ErrorMessage ??
                                  itemMsg?.Errors.LastOrDefault()?.Exception.Message;

                actionContext.Result = new JsonResult(ajaxReponse);
            }
            base.OnActionExecuting(actionContext);
        }
    }
}
