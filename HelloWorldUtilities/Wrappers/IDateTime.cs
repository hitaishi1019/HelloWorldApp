using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldUtilities.Wrappers
{
    public interface IDateTime
    {
        ///     Gets the DateTime as of Now
        
        /// Returns A DateTime object containing the date and time of Now
        DateTime Now();
    }
}
