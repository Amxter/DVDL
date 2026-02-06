using System;
using System.ComponentModel;
using System.IO;

namespace DatabaseDVLD
{
    public class FileLogger : ILogger
    {
        private readonly string _logFilePath;
        private readonly bool _isDesignTime;

        public FileLogger()
        {
            
            _isDesignTime = LicenseManager.UsageMode == LicenseUsageMode.Designtime;

            if (_isDesignTime)
                return;  

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            string logDir = Path.Combine(baseDir, "logs");

        
            Directory.CreateDirectory(logDir);

            _logFilePath = Path.Combine(
                logDir,
                $"log_{DateTime.Now:yyyy_MM_dd}.txt"
            );
        }

        public void Info(string message)
        {
            if (_isDesignTime) return;
            Write("INFO", message);
        }

        public void Error(string message, Exception ex)
        {
            if (_isDesignTime) return;
            Write("ERROR", $"{message}{Environment.NewLine}{ex}");
        }

        private void Write(string level, string message)
        {
            try
            {
                var log = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";
                File.AppendAllText(_logFilePath, log + Environment.NewLine);
            }
            catch
            {
  
            }
        }
    }
}
