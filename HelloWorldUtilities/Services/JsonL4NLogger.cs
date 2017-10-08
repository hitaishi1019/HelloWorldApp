using log4net.Config;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldUtilities.Services
{
   public class JsonL4NLogger:ILogger
    {
       
        ///     The log4net logger
        private readonly log4net.Core.ILogger log4NetLogger;
        ///     The logger name
        private string loggerName;

        ///     Initializes a new instance of the <see cref="JsonL4NLogger" /> class.
        
        public JsonL4NLogger()
        {
            XmlConfigurator.Configure();
            this.log4NetLogger = LoggerManager.GetLogger(this.GetType().Assembly, this.GetType().Name);
            ////this.log4NetLogger = LoggerManager.GetLogger(this.GetType().Assembly, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            this.loggerName = this.GetType().Name;
        }

        ///     Write an INFO message to the log
        public void Info(string message, Dictionary<string, object> otherProperties)
        {
            this.WriteLog(Level.Info, message, otherProperties, null);
        }

        ///     Write an DEBUG message to the log
        public void Debug(string message, Dictionary<string, object> otherProperties)
        {
            this.WriteLog(Level.Debug, message, otherProperties, null);
        }

        ///     Write an ERROR message to the log
        public void Error(string message, Dictionary<string, object> otherProperties, Exception exception)
        {
            this.WriteLog(Level.Error, message, otherProperties, exception);
        }

        ///     Writes the log using log4net
        private void WriteLog(Level logLevel, string message, Dictionary<string, object> otherProperties, Exception exception)
        {
            // Create the logging event data
            var loggingEventData = new LoggingEventData()
            {
                Level = logLevel,
                LoggerName = this.loggerName,
                Domain = AppDomain.CurrentDomain.FriendlyName,
                TimeStamp = DateTime.Now,
                Message = message
            };

            // Create the logging event
            var loggingEvent = new LoggingEvent(loggingEventData);

            // Check for other properties
            if (otherProperties != null)
            {
                foreach (var property in otherProperties)
                {
                    if (property.Key != null && property.Value != null)
                    {
                        loggingEvent.Properties[property.Key] = property.Value;
                    }
                }
            }

            // Check for exception
            if (exception != null)
            {
                loggingEvent.Properties["exception"] = exception.ToString();
            }

            // Log the data
            this.log4NetLogger.Log(loggingEvent);
        }
    }
}
