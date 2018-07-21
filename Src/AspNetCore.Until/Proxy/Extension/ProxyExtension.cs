using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AspNetCore.Until.Proxy.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Until.Proxy.Extension
{
    public static class ProxyExtension
    {
        public static IServiceCollection AddeProxyUrl(this IServiceCollection service, Action<ProxyBuilder> action)
        {
            ProxyBuilder builder = new ProxyBuilder();
            action.Invoke(builder);
            service.AddSingleton(builder);

            return service;
        }

        public static IApplicationBuilder UseProxyUrl(this IApplicationBuilder app)
        {
            app.UseMiddleware<ProxyUrlMiddleware>();

            return app;
        }
    }
}
