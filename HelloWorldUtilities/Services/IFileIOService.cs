using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldUtilities.Services
{
    public interface IFileIOService
    {
        ///     Reads the specified file
        string ReadFile(string filePath);//returns The contents of the file
    }
}
