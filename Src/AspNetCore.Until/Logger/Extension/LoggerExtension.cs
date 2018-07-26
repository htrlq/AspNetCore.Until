using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCore.Until.Logger.Extension
{
    public static class LoggerExtension
    {
        public static IServiceCollection AddTraceLogger(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ILoggerFactory, TraceLoggerFactory>();
            return serviceCollection;
        }

        public static IApplicationBuilder UseTraceLogger(this IApplicationBuilder app)
        {
            var services = app.ApplicationServices;
            using (var loggerFactory = services.GetRequiredService<ILoggerFactory>())
            {
                loggerFactory.AddProvider(new TraceLoggerProvider());
            }

            return app;
        }
    }
}
