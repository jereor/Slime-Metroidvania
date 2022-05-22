namespace Utility
{
    public interface ILoggerAdapter
    {
        void LogVerbose(string message);
        void Log(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
}
