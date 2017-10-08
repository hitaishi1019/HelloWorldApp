using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;


namespace HelloWorldUtilities.Services
{
   public class ServerHostingEnvironment:IHostingEnvironmentService
    {

        ///     Map's the specified path to the hosting environment's path
        //Returns The hosting environment's path
        public string MapPath(string path)
        {
            return HostingEnvironment.MapPath("~/" + path);
        }
    }
}
