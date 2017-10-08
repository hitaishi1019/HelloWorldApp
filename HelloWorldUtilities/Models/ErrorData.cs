using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldUtilities.Models
{
    class ErrorData
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public string ExceptionType { get; set; }
        public string FullException { get; set; }
        public string Severity { get; set; }
    }
}
