using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace DatabaseDVLD
{
    public class EventLogs : ILogger
    {
        private readonly string sourceName;

        public EventLogs()
        {
            sourceName = "DVDLApplication";


            // Create the event source if it does not exist
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");
              
            }
        }

        public void Info(string message)
        {
            Write (EventLogEntryType.Information, message);
        }

        public void Error(string message, Exception ex)
        {
         Write(EventLogEntryType.Error, $"{message} - Exception: {ex.Message}");
        }

        private void Write(EventLogEntryType eventLogEntryType, string message)
        {
            try
            {
                EventLog.WriteEntry(sourceName, message , eventLogEntryType);
            }
            catch
            {
  
            }
        }
    }
}
