using System.Threading.Tasks;
using AspNetCore.Until.Extension;
using AspNetCore.Until.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using Microsoft.Extensions.Options;

namespace AspNetCore.Until.Proxy.Middleware
{
    public class ProxyUrlMiddleware : BaseMiddleware
    {
        private ProxyBuilder _builder { get; }

        public ProxyUrlMiddleware(RequestDelegate next,IOptions<ProxyBuilder> options) : base(next)
        {
            _builder = options.Value;
        }


        public override async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            var pathBase = !request.PathBase.HasValue ? request.Path.Value : request.PathBase.Value;
            foreach (var item in _builder.Items)
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