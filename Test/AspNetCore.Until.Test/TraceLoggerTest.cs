using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.Extensions.DependencyInjection;
using AspNetCore.Until.Logger.Extension;
using Microsoft.Extensions.Logging;
using Xunit;

namespace AspNetCore.Until.Test
{
    public class TraceLoggerTest
    {
        [Fact]
        public void Test()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();

            serviceCollection.AddTraceLogger();
            serviceCollection.AddScoped<LoggerFactoryTest>();
            serviceCollection.AddScoped<LoggerObjectTest>();

            using (var services = serviceCollection.BuildServiceProvider())
            {
                //var app = services.GetRequiredService<IApplicationBuilder>();
                //app.ApplicationServices = services;
                var app = new ApplicationBuilder(services);
                app.UseTraceLogger();

                //var LoggerFactory = services.GetRequiredService<LoggerFactoryTest>();
                //LoggerFactory.Write("hello LoggerFactory");

                var LoggerObject = services.GetRequiredService<LoggerObjectTest>();
                LoggerObject.Write("hello Logger");
            }
        }
    }

    public class Param
    {
        public string Name { get; set; }
    }

    public class LoggerBase<T>
    {
        protected ILogger<T> logger = null;

        public void Write(string message)
        {
            logger?.LogError(message);
        }
    }

    public class LoggerFactoryTest: LoggerBase<Param>
    {
        public LoggerFactoryTest(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<Param>();
        }
    }

    public class LoggerObjectTest : LoggerBase<Param>
    {
        public LoggerObjectTest(ILogger<Param> logger)
        {
            this.logger = logger;
        }
    }
}
