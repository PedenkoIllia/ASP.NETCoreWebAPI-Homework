using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP.NETCoreWebAPI_Homework.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            string actionName = context.ActionDescriptor.DisplayName;

            string exceptionMessage = context.Exception.Message;

            context.Result = new ContentResult()
            {
                Content = $"Method {actionName} throw an exception : {exceptionMessage}",
                StatusCode = 400
            };
            context.ExceptionHandled = true;
        }
    }
}
