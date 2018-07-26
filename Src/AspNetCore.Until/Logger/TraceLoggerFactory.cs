using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace AspNetCore.Until.Logger
{
    public class TraceLoggerFactory : ILoggerFactory
    {
        private ILoggerProvider provider = null;

        public void AddProvider(ILoggerProvider provider)
        {
            this.provider = provider;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return provider.CreateLogger(categoryName);
        }

        public void Dispose()
        {

        }
    }
}
