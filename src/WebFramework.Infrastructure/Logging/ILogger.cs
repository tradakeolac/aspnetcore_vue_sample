namespace WebFramework.Infrastructure.Logging
{
    using System;

    public interface ILogger
    {
        void LogError(string message);
        void LogError(Exception error);
    }
}
