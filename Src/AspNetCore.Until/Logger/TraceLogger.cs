using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AspNetCore.Until.Logger
{
    public class TraceLogger : ILogger
    {
        private string Name { get; }

        public TraceLogger(string name)
        {
            Name = name;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            StackTrace trace = new StackTrace();
            List<string> informatList = new List<string>();
            for (int i = 4; i > 0; i--)
            {
                var frame = trace.GetFrame(i);
                string name = frame.ToString();
                informatList.Add(name);
            }

            string message = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}\r\nclass:{Name}\r\n{string.Join("\n", informatList)}\r\n{logLevel}:{eventId} - {formatter(state, exception)}";
            var type = typeof(TState);
            FileProvider.Write(type, message);
        }
    }

    internal static class FileProvider
    {
        private static ConcurrentDictionary<Type,object> dictionary = new ConcurrentDictionary<Type, object>();

        public static void Write(Type fileType, string message)
        {
            var dir = $"{DateTime.Now:yyyy/MM/dd}";

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var name = $"{dir}/{fileType.Name}.log";

            Object locked;
            if (dictionary.ContainsKey(fileType))
            {
                locked = dictionary[fileType];
            }
            else
            {
                locked = new object();
                dictionary.TryAdd(fileType, locked);
            }

            lock (locked)
            {
                Stream stream;
                if (File.Exists(name))
                    stream = new FileStream(name, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, 512, FileOptions.WriteThrough);
                else
                    stream = new FileStream(name, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 512, FileOptions.WriteThrough);

                var bytes = Encoding.UTF8.GetBytes(message);
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
            }
        }
    }
}
