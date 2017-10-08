using HelloWorldUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldUtilities.Services
{
    public interface IDataService
    {
        ///     Gets today's data
        TodaysData GetTodaysData();
        //returns A TodaysData model containing today's data
    }
}
