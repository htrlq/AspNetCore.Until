using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace AspNetCore.Until.Logger
{
    public class TraceLoggerProvider : ILoggerProvider
    {
        readonly ConcurrentDictionary<string, ILogger> loggers = new ConcurrentDictionary<string, ILogger>();

        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, name => new TraceLogger(categoryName));
        }

        public void Dispose()
        {

        }
    }
}
