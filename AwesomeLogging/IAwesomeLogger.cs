namespace AwesomeLogging
{
    public interface IAwesomeLogger
    {
        void WritePerf(AwesomeLog infoToLog);
        void WriteUsage(AwesomeLog infoToLog);
        void WriteError(AwesomeLog infoToLog);
        void WriteDiagnostic(AwesomeLog infoToLog);
        void WriteWarning(AwesomeLog infoToLog);
    }
}