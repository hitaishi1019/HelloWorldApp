using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldUtilities.Wrappers
{
    public class SystemConsole : IConsole
    {
        public void ErrorWrite(string message)
        {
            Console.Error.Write(message); ///     Writes to the Console.Error (standard error)
        }

        public void ErrorWriteLine(string message)
        {
            Console.Error.WriteLine(message);///     Writes a line to the Console.Error (standard error)
        }

        public void Write(string message)
        {
            Console.Write(message); ///     Writes to the Console
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);  ///     Writes a line to the Console
        }
    }
}
