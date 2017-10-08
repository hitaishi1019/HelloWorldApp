using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;
using System.IO;
using System.Collections;
using Newtonsoft.Json;

namespace HelloWorldUtilities.Layouts
{
   public class JSONLayout:LayoutSkeleton
    {
        /// <summary>
        ///     Overrides the Activate Options method for log4net's layout
        /// </summary>
        public override void ActivateOptions()
        {
        }

        /// <summary>
        ///     Overrides the Format method for log4net's layout
        /// </summary>
        /// <param name="writer">The text writer</param>
        /// <param name="loggingEvent">The logging event</param>
        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            var dictionary = new Dictionary<string, object>();

            // Add the main properties
            dictionary.Add("timestamp", loggingEvent.TimeStamp);
            dictionary.Add("level", loggingEvent.Level != null ? loggingEvent.Level.DisplayName : "null");
            dictionary.Add("message", loggingEvent.RenderedMessage);
            dictionary.Add("logger", loggingEvent.LoggerName);

            // Loop through all other properties
            foreach (DictionaryEntry dictionaryEntry in loggingEvent.GetProperties())
            {
                var key = dictionaryEntry.Key.ToString();

                // Check if the key exists
                if (!dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, dictionaryEntry.Value);
                }
            }

            // Convert the log string into a JSON string
            var logString = JsonConvert.SerializeObject(dictionary);

            writer.WriteLine(logString);
        }
    }
}
