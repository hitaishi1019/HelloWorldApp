using HelloWorldUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldApp.Services
{
    public interface IHelloWorldService //service for communicating with web api
    {
        ///     Gets today's data from the web API
        ///     returns A TodaysData model containing today's data
        TodaysData GetTodaysData();
    }
}
