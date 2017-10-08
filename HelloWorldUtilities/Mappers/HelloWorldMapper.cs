using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloWorldUtilities.Models;

namespace HelloWorldUtilities.Mappers
{
    public class HelloWorldMapper : IHelloWorldMapper
    {
        public TodaysData StringToTodaysData(string input)
        {
            return new TodaysData { Data = input };
        }
    }
}
