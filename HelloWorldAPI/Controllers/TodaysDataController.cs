using HelloWorldUtilities.Attributes;
using HelloWorldUtilities.Models;
using HelloWorldUtilities.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HelloWorldAPI.Controllers
{
    [APIExceptionFilter]
    public class TodaysDataController : ApiController
    {
        private readonly IDataService dataService;
        ///     Initializes a new instance of the <see cref="TodaysDataController" /> class.
        public TodaysDataController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        ///     Gets today's value
        /// Returns A TodaysData model containing today's value

        [APIExceptionFilter(Type = typeof(IOException), Status = HttpStatusCode.ServiceUnavailable, Severity = SeverityCode.Error)]

        [APIExceptionFilter(Type = typeof(SettingsPropertyNotFoundException), Status = HttpStatusCode.ServiceUnavailable, Severity = SeverityCode.Error)]

        public TodaysData Get()
        {
            return this.dataService.GetTodaysData();
        }
    }
}
