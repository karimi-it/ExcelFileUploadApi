using ExcelFileUploadApi.Application.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelFileUploadApi.Infrastructure.Logging
{
    public class LoggingService : ILoggingService
    {
        public void LogError(string message, Exception ex)
        {
            Log.Error(ex, message);
            
        }

        public void LogInformation(string message)
        {
            Log.Information(message);
        }
    }
}
