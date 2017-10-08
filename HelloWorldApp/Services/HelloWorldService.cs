using HelloWorldUtilities.Models;
using HelloWorldUtilities.Resources;
using HelloWorldUtilities.Services;
using HelloWorldUtilities.Wrappers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldApp.Services
{
    ///     Service class for communicating with the Hello World Web API
    public class HelloWorldService:IHelloWorldService
    {
        ///     The application settings service
        private readonly IAppSettings appSettings;
        ///     The logger
        private readonly ILogger logger;
        ///     The Rest client
        private readonly IRestClient restClient;
        ///     The Rest request
        private readonly IRestRequest restRequest;
        ///     The wrapped Uri service
        private readonly IUri uriService;
        ///     Initializes a new instance of the <see cref="HelloWorldWebService" /> class.
        
        public HelloWorldService(
            IRestClient restClient,
            IRestRequest restRequest,
            IAppSettings appSettings,
            IUri uriService,
            ILogger logger)
        {
            this.restClient = restClient;
            this.restRequest = restRequest;
            this.appSettings = appSettings;
            this.uriService = uriService;
            this.logger = logger;
        }
        ///     Gets today's data from the web API
        /// Returns A TodaysData model containing today's data
        public TodaysData GetTodaysData()
        {
            TodaysData todaysData = null;

            // Set the URL for the request
            this.restClient.BaseUrl = this.uriService.GetUri(this.appSettings.Get(AppSettingsKey.HelloWorldApiUrlKey));

            // Setup the request
            this.restRequest.Resource = "todaysdata";
            this.restRequest.Method = Method.GET;

            // Clear the request parameters
            this.restRequest.Parameters.Clear();

            // Execute the call and get the response
            var todaysDataResponse = this.restClient.Execute<TodaysData>(this.restRequest);

            // Check for data in the response
            if (todaysDataResponse != null)
            {
                // Check if any actual data was returned
                if (todaysDataResponse.Data != null)
                {
                    todaysData = todaysDataResponse.Data;
                }
                else
                {
                    var errorMessage = "Error in RestSharp, most likely in endpoint URL." + " Error message: "
                                       + todaysDataResponse.ErrorMessage + " HTTP Status Code: "
                                       + todaysDataResponse.StatusCode + " HTTP Status Description: "
                                       + todaysDataResponse.StatusDescription;

                    // Check for existing exception
                    if (todaysDataResponse.ErrorMessage != null && todaysDataResponse.ErrorException != null)
                    {
                        // Log an informative exception including the RestSharp exception
                        this.logger.Error(errorMessage, null, todaysDataResponse.ErrorException);
                    }
                    else
                    {
                        // Log an informative exception including the RestSharp content
                        this.logger.Error(errorMessage, null, new Exception(todaysDataResponse.Content));
                    }
                }
            }
            else
            {
                // Log the exception
                const string ErrorMessage =
                    "Did not get any response from the Hello World Web Api for the Method: GET /todaysdata";

                this.logger.Error(ErrorMessage, null, new Exception(ErrorMessage));
            }

            return todaysData;
        }
    }
}

