using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloWorldUtilities.Models;
using HelloWorldUtilities.Wrappers;
using HelloWorldUtilities.Mappers;
using HelloWorldUtilities.Resources;
using System.Configuration;

namespace HelloWorldUtilities.Services
{
    ///     Data service for manipulating Hello World data
    public class HelloWorldDataService : IDataService
    {
        
        ///     The application settings service
        private readonly IAppSettings appSettings;
        ///     The DateTime wrapper
        private readonly IDateTime dateTimeWrapper;
        ///     The File IO service
        private readonly IFileIOService fileIOService;
        ///     The Hello World Mapper
        private readonly IHelloWorldMapper helloWorldMapper;
        ///     Initializes a new instance of the <see cref="HelloWorldDataService" /> class.
        public HelloWorldDataService(
            IAppSettings appSettings,
            IDateTime dateTimeWrapper,
            IFileIOService fileIOService,
            IHelloWorldMapper helloWorldMapper)
        {
            this.appSettings = appSettings;
            this.dateTimeWrapper = dateTimeWrapper;
            this.fileIOService = fileIOService;
            this.helloWorldMapper = helloWorldMapper;
        }

        /// <summary>
        ///     Gets today's data
        /// </summary>
        /// <returns>A TodaysData model containing today's data</returns>
        public TodaysData GetTodaysData()
        {
            // Get the file path
            var filePath = this.appSettings.Get(AppSettingsKey.TodayDataFileKey);

            if (string.IsNullOrEmpty(filePath))
            {
                // No file path was found, throw exception
                throw new SettingsPropertyNotFoundException(
                    ErrorCodes.TodaysDataFileSettingsKeyError,
                    new SettingsPropertyNotFoundException("The TodayDataFile settings key was not found or had no value."));
            }

            // Get the data from the file
            var rawData = this.fileIOService.ReadFile(filePath);

            // Add the timestamp
            rawData += " as of " + this.dateTimeWrapper.Now().ToString("F");

            // Map to the return type
            var todaysData = this.helloWorldMapper.StringToTodaysData(rawData);

            return todaysData;
        }
    }
}
