using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldUtilities.Wrappers
{
    public class SystemUri : IUri
    {
        ///     Creates a Uri based on the specified Uri string
        public Uri GetUri(string uriString)
        {
            /// Returns A DateTime object containing the date and time of Now
            return new Uri(uriString);
        }
    }
}
