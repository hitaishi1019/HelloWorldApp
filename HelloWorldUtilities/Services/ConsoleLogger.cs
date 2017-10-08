using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloWorldUtilities.Wrappers;

namespace HelloWorldUtilities.Services
{
    public class ConsoleLogger : ILogger
    {
        ///     The Console abstraction for writing to the console.
        
        private readonly IConsole console;
        ///     Initializes a new instance of the <see cref="ConsoleLogger" /> class.
        /// </summary>
        /// <param name="console">The injected console</param>
        public ConsoleLogger(IConsole console)
        {
            this.console = console;
        }

        ///     Write an INFO message to the log
        public void Info(string message, Dictionary<string, object> otherProperties)
        {
            this.WriteLog("INFO", message, otherProperties, null);
        }

        ///     Write an DEBUG message to the log
        public void Debug(string message, Dictionary<string, object> otherProperties)
        {
            this.WriteLog("DEBUG", message, otherProperties, null);
        }

        ///     Write an ERROR message to the log
        public void Error(string message, Dictionary<string, object> otherProperties, Exception exception)
        {
            this.WriteLog("ERROR", message, otherProperties, exception);
        }

        ///     Writes the log level to the Console
        
        private void WriteLog(string logLevel, string message, Dictionary<string, object> otherProperties, Exception exception)
        {
            // Create a string builder with the log level and message
            var builder = new StringBuilder(logLevel);
            builder.Append(": ");
            builder.Append(message);

            // Check for other properties
            if (otherProperties != null)
            {
                foreach (var property in otherProperties)
                {
                    if (property.Key != null && property.Value != null)
                    {
                        builder.Append(" [");
                        builder.Append(property.Key);
                        builder.Append("=");
                        builder.Append(property.Value);
                        builder.Append("]");
                    }
                }
            }

            // Check for an exception
            if (exception != null)
            {
                builder.Append(" [Exception: ");
                builder.Append(exception);
                builder.Append("]");
            }

            // Write the log to the Console
            this.console.WriteLine(builder.ToString());
        }
    }
}
