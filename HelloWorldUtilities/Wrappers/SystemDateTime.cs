using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldUtilities.Wrappers
{
    public class SystemDateTime : IDateTime
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
