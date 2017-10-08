using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldUtilities.Services
{
    public interface ILogger
    {
        ///     Write an INFO message to the log
        void Info(string message, Dictionary<string, object> otherProperties);
        ///     Write an DEBUG message to the log
        void Debug(string message, Dictionary<string, object> otherProperties);
        ///     Write an ERROR message to the log
        void Error(string message, Dictionary<string, object> otherProperties, Exception exception);
    }
}
