using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCore.Until
{
    public class ValidAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                TokenCheck.Checked(context);
            }
            catch (Exception e)
            {
                context.Result = new ContentResult()
                {
                    Content = e.Message,
                    ContentType = "text/html",
                    StatusCode = 404
                };
            }
        }
    }
}