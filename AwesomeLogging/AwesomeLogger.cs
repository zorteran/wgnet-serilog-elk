using Serilog;
using Serilog.Events;
using System;
using System.Data.SqlClient;

namespace AwesomeLogging
{
    public class AwesomeLogger : IAwesomeLogger
    {
        private readonly ILogger _perfLogger;
        private readonly ILogger _usageLogger;
        private readonly ILogger _errorLogger;
        private readonly ILogger _warningLogger;
        private readonly ILogger _diagnosticLogger;
        private readonly bool _writeDiagnostics;

        public AwesomeLogger(AwesomeLoggerConfig config)
        {
            _writeDiagnostics = config.WriteDiagnostics;
            _perfLogger = new LoggerConfiguration()
                .ConfigureLogFile(config.FilePathFormat, "perf")
                .ConfigureElasticSearch(config.ElasticSearchUris, config.ElasticSearchUsername, config.ElasticSearchPassword, "perf")
                .ConfigureConsole(config.WriteToConsole)
                .CreateLogger();

            _usageLogger = new LoggerConfiguration()
                .ConfigureLogFile(config.FilePathFormat, "usage")
                .ConfigureElasticSearch(config.ElasticSearchUris, config.ElasticSearchUsername, config.ElasticSearchPassword, "usage")
                .ConfigureConsole(config.WriteToConsole)
                .CreateLogger();

            _errorLogger = new LoggerConfiguration()
                .ConfigureLogFile(config.FilePathFormat, "error")
                .ConfigureElasticSearch(config.ElasticSearchUris, config.ElasticSearchUsername, config.ElasticSearchPassword, "error")
                .ConfigureConsole(config.WriteToConsole)
                .CreateLogger();

            _warningLogger = new LoggerConfiguration()
                .ConfigureLogFile(config.FilePathFormat, "warning")
                .ConfigureElasticSearch(config.ElasticSearchUris, config.ElasticSearchUsername, config.ElasticSearchPassword, "warning")
                .ConfigureConsole(config.WriteToConsole)
                .CreateLogger();

            _diagnosticLogger = new LoggerConfiguration()
                .ConfigureLogFile(config.FilePathFormat, "diagnostic")
                .ConfigureElasticSearch(config.ElasticSearchUris, config.ElasticSearchUsername, config.ElasticSearchPassword, "diagnostic")
                .ConfigureConsole(config.WriteToConsole)
                .CreateLogger();

            

            Serilog.Debugging.SelfLog.Enable(msg => System.Diagnostics.Debug.WriteLine(msg));
        }

        public void WritePerf(AwesomeLog infoToLog)
        {
            _perfLogger.Write(LogEventLevel.Information, "{@FlogDetail}", infoToLog);
        }
        public void WriteUsage(AwesomeLog infoToLog)
        {
            _usageLogger.Write(LogEventLevel.Information, "{@FlogDetail}", infoToLog);
        }
        public void WriteError(AwesomeLog infoToLog)
        {
            if (infoToLog.Exception != null)
            {
                var procName = FindProcName(infoToLog.Exception);
                infoToLog.Location = string.IsNullOrEmpty(procName) ? infoToLog.Location : procName;
                infoToLog.Message = GetMessageFromException(infoToLog.Exception);
            }
            _errorLogger.Write(LogEventLevel.Error, "{@FlogDetail}", infoToLog);
        }
        public void WriteDiagnostic(AwesomeLog infoToLog)
        {
            if (!_writeDiagnostics)
                return;

            _diagnosticLogger.Write(LogEventLevel.Information, "{@FlogDetail}", infoToLog);
        }
        public void WriteWarning(AwesomeLog infoToLog)
        {
            _warningLogger.Write(LogEventLevel.Warning, "{@FlogDetail}", infoToLog);
        }

        private string GetMessageFromException(Exception ex)
        {
            if (ex.InnerException != null)
                return GetMessageFromException(ex.InnerException);

            return ex.Message;
        }

        private string FindProcName(Exception ex)
        {
            if (ex is SqlException sqlEx)
            {
                var procName = sqlEx.Procedure;
                if (!string.IsNullOrEmpty(procName))
                    return procName;
            }

            if (!string.IsNullOrEmpty((string)ex.Data["Procedure"]))
            {
                return (string)ex.Data["Procedure"];
            }

            if (ex.InnerException != null)
                return FindProcName(ex.InnerException);

            return null;
        }

    }
}
