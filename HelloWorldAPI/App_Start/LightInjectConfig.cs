using HelloWorldUtilities.Mappers;
using HelloWorldUtilities.Services;
using HelloWorldUtilities.Wrappers;
using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HelloWorldAPI.App_Start
{
    /// Configures dependency injection via LightInject
    public static class LightInjectConfig
    {
        /// Registers main components
        public static void Register(HttpConfiguration config)
        {
            var container = new ServiceContainer();
        //    container.RegisterApiControllers();
        //    container.EnablePerWebRequestScope();
        //    container.EnableWebApi(GlobalConfiguration.Configuration);
        //    container.EnableMvc();
            // Register Services
            RegisterServices(container);
        }

        /// Registers the dependency services to be injected
        private static void RegisterServices(IServiceRegistry serviceRegistry)
        {
            // Register default Application Settings Service
            serviceRegistry.Register<IAppSettings, ConfigAppSetting>();

            // Register default Logger Service
            ////serviceRegistry.Register<ILogger, JsonL4NLogger>();
            serviceRegistry.RegisterInstance(typeof(ILogger), new JsonL4NLogger());

            // Register default Hosting Environment Service
            serviceRegistry.Register<IHostingEnvironmentService, ServerHostingEnvironment>();

            // Register default File IO Service
            serviceRegistry.Register<IFileIOService, TextFileIOService>();

            // Register default Data Service
            serviceRegistry.Register<IDataService, HelloWorldDataService>();

            // Register default DateTime wrapper
            serviceRegistry.Register<IDateTime, SystemDateTime>();

            // Register default Hello World mapper
            serviceRegistry.Register<IHelloWorldMapper, HelloWorldMapper>();
        }
    }
}