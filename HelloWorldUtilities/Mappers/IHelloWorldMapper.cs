using HelloWorldUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldUtilities.Mappers
{
  public interface IHelloWorldMapper
    {
        TodaysData StringToTodaysData(string input);
    }
}
