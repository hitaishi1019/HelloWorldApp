using HelloWorldUtilities.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldUtilities.Services
{
    ///     Service for text file IO
    public class TextFileIOService : IFileIOService
    {
        ///     The hosting environment service
        private readonly IHostingEnvironmentService hostingEnvironmentService;

        ///     Initializes a new instance of the <see cref="TextFileIOService" /> class.
        
        public TextFileIOService(IHostingEnvironmentService hostingEnvironmentService)
        {
            this.hostingEnvironmentService = hostingEnvironmentService;
        }

        ///     Reads the specified file
        
        public string ReadFile(string filePath)
        {
            string fileContents;

            // Map path to server path
            var serverPath = this.hostingEnvironmentService.MapPath(filePath);

            // Read the contents of the file
            try
            {
                using (var reader = new StreamReader(serverPath))
                {
                    fileContents = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                // Throw an IO exception
                throw new IOException(ErrorCodes.TodayDataFileError, new IOException("There was a problem reading the data file!", ex));
            }
            return fileContents;
        }
    }
}
