using HelloWorldApp.Services;
using HelloWorldUtilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldApp.Application
{
    ///     Hello World Console Application
   public class HelloWorldConsole : IHelloWorldConsole
    {
        
        ///     The Hello World Web Service
        private readonly IHelloWorldService helloWorldWebService;

        ///     The logger
       
        private readonly ILogger logger;

        ///     Initializes a new instance of the <see cref="HelloWorldConsole" /> class.
       
        public HelloWorldConsole(IHelloWorldService helloWorldService, ILogger logger)
        {
            this.helloWorldWebService = helloWorldService;
            this.logger = logger;
        }

        ///     Runs the main Hello World Console Application
        
        public void Display(string[] arguments)
        {
            // Get Today's data
            var todaysData = this.helloWorldWebService.GetTodaysData();

            // Write Today's data to the screen
            this.logger.Info(todaysData != null ? todaysData.Data : "No data was found!", null);
        }
    }
}
