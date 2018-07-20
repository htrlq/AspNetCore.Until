using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCore.Until
{
    public class ValidBaseController: Controller
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                TokenCheck.Checked(context);

                await next.Invoke();
            }
            catch (Exception ex)
            {
                var statusCode = context.HttpContext.Response.StatusCode;
                if (ex is ArgumentException)
                {
                    statusCode = 200;
                }
                await HandleExceptionAsync(context.HttpContext, ex.Message);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, string result)
        {
            return context.Response.WriteAsync(result);
        }
    }
}
