using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.Until.Middleware
{
    public abstract class BaseMiddleware
    {
        protected RequestDelegate next { get; }

        public BaseMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public abstract Task Invoke(HttpContext context);
    }
}
