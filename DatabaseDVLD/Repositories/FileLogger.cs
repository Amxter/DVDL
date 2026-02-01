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
            // نحدد إذا إحنا في Design Time
            _isDesignTime = LicenseManager.UsageMode == LicenseUsageMode.Designtime;

            if (_isDesignTime)
                return; // ❗ مهم جدًا: لا نعمل أي I/O وقت الـ Designer

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            string logDir = Path.Combine(baseDir, "logs");

            // إنشاء المجلد إذا مش موجود
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
                // ❗ لا نرمي Exception من Logger
                // Logger لازم يكون "fail-safe"
            }
        }
    }
}
