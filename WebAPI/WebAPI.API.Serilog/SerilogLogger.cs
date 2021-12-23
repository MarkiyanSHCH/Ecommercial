using System;
using System.Text.Json;

using Serilog;

using Core.Handlers.Logging.Models;
using ILogger = Core.Handlers.Logging.ILogger;

namespace WebAPI.API.Serilog
{
    public class SerilogLogger : ILogger
    {
        private const string ApplicationId = "API";

        public void Error(string message, ApplicationScope scope, object payload = null, Exception exception = null)
            => Log.Error(
                    "{Application} {Scope} {Message} {TimeStamp} {Exception} {Payload}",
                    ApplicationId,
                    Enum.GetName(typeof(ApplicationScope), scope),
                    message,
                    DateTime.UtcNow,
                    exception?.ToString(),
                    payload != null ? JsonSerializer.Serialize(payload) : null);

    }
}