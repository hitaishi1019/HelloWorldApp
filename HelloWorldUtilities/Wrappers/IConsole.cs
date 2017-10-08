using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldUtilities.Wrappers
{
    public interface IConsole
    {
        void Write(string message);

        ///     Writes a line to the Console
       
        void WriteLine(string message);

        
        ///     Writes to the Console.Error (standard error)
        
        void ErrorWrite(string message);

        
        ///     Writes a line to the Console.Error (standard error)
        
        void ErrorWriteLine(string message);
    }
}
