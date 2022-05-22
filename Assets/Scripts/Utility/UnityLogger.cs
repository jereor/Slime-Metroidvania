using UnityEngine;

namespace Utility
{
    public sealed class UnityLogger : ILoggerAdapter
    {
        public void LogVerbose(string message)
        {
            Debug.Log(message);
        }

        public void Log(string message)
        {
            Debug.Log(message);
        }

        public void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }

        public void LogError(string message)
        {
            Debug.LogError(message);
        }
    }
}
