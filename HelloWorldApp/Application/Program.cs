
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloWorldApp.Services;
using HelloWorldUtilities.Services;
using HelloWorldUtilities.Wrappers;
using LightInject;
using RestSharp;

namespace HelloWorldApp.Application
{
    ///     Main class that drives the application
    public class Program
    {
     public static void Main(string[] args)
        {
            // Setup dependency injection and run the application

            using (var container = new ServiceContainer())

            {
                // Configure depenency injection

                container.Register<IHelloWorldConsole, HelloWorldConsole>();

                container.Register<IAppSettings, ConfigAppSetting>();

                container.Register<IConsole, SystemConsole>();

                container.Register<ILogger, ConsoleLogger>();

                container.Register<IUri, SystemUri>();

                container.Register<IHelloWorldService, HelloWorldService>();

                container.RegisterInstance(typeof(IRestClient), new RestClient());

                container.RegisterInstance(typeof(IRestRequest), new RestRequest());

                // Run the main program

                container.GetInstance<IHelloWorldConsole>().Display(args);

            }
        }
    }
}
