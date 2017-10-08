using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldUtilities.Services
{
    public interface IHostingEnvironmentService
    {
        ///     Map's the specified path to the hosting environment's path
        string MapPath(string path); // returns The hosting environment's path
    }
}
