using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldUtilities.Services
{
   public class TestLogger:ILogger
    {
        ///     The list of log messages set by calling classes
        private readonly List<string> logMessageList;
        ///     The list of exceptions set by calling classes
        private readonly List<Exception> exceptionList;
        ///     The list of other properties set by calling classes
        private readonly List<object> otherPropertiesList;
        ///     Initializes a new instance of the <see cref="TestLogger"/> class. 
        public TestLogger(ref List<string> logMessageList, ref List<Exception> exceptionList, ref List<object> otherPropertiesList)
        {
            this.logMessageList = logMessageList;
            this.exceptionList = exceptionList;
            this.otherPropertiesList = otherPropertiesList;
        }
        ///     Write an INFO message to the log
        public void Info(string message, Dictionary<string, object> otherProperties)
        {
            this.logMessageList.Add(message);
            this.otherPropertiesList.Add(otherProperties);
        }
        ///     Write an DEBUG message to the log
        public void Debug(string message, Dictionary<string, object> otherProperties)
        {
            this.logMessageList.Add(message);
            this.otherPropertiesList.Add(otherProperties);
        }
        ///     Write an ERROR message to the log
        public void Error(string message, Dictionary<string, object> otherProperties, Exception exception)
        {
            this.logMessageList.Add(message);
            this.otherPropertiesList.Add(otherProperties);
            this.exceptionList.Add(exception);
        }
    }
}

