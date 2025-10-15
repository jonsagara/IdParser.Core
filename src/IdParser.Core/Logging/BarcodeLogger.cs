using Microsoft.Extensions.Logging;

namespace IdParser.Core.Logging;

/// <summary>
/// High-performance logging for ASP.NET Core. See: https://learn.microsoft.com/en-us/dotnet/core/extensions/logger-message-generator
/// </summary>
internal static partial class BarcodeLogger
{
    [LoggerMessage(EventId = 200, Level = LogLevel.Error, Message = "Unhandled exception in {MethodName} while trying to parse element Id {ElementId}.")]
    internal static partial void Error_PopulateIdCardUnhandledException(this ILogger logger, Exception ex, string methodName, string elementId);

    [LoggerMessage(EventId = 201, Level = LogLevel.Warning, Message = "One or more ElementIds were not handled by the ID or Driver's License parsers: {UnhandledElementIds}.")]
    internal static partial void Warning_UnhandledElementIds(this ILogger logger, string unhandledElementIds);
}
