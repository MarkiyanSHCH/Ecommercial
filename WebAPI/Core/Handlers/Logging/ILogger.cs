using System;

using Core.Handlers.Logging.Models;

namespace Core.Handlers.Logging
{
    public interface ILogger
    {
        void Error(string message, ApplicationScope scope, object payload = null, Exception exception = null);
    }
}