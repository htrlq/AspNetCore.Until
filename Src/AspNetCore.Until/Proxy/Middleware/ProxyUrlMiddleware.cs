using System.Threading.Tasks;
using AspNetCore.Until.Extension;
using AspNetCore.Until.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.Extensions;
using System;

namespace AspNetCore.Until.Proxy.Middleware
{
    public class ProxyUrlMiddleware : BaseMiddleware
    {
        public ProxyUrlMiddleware(RequestDelegate next) : base(next)
        {
        }


        public override async Task Invoke(HttpContext context)
        {
            var builder = context.RequestServices.GetRequiredService<ProxyBuilder>();
            var request = context.Request;
            var pathBase = !request.PathBase.HasValue ? request.Path.Value : request.PathBase.Value;
            foreach (var item in builder.Items)
            {
                if (pathBase.ToLowContains(item.SrcPath))
                {
                    var url = item.TargetPath;

                    await context.ProxyUrl(url);
                    return;
                }
            }

            await next(context);
        }
    }
}