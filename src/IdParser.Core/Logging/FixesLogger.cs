using Microsoft.Extensions.Logging;

namespace IdParser.Core.Logging;

/// <summary>
/// High-performance logging for ASP.NET Core. See: https://learn.microsoft.com/en-us/dotnet/core/extensions/logger-message-generator
/// </summary>
internal static partial class FixesLogger
{
    [LoggerMessage(EventId = 100, Level = LogLevel.Error, Message = "Input doesn't start with the expected compliance indicator '{ExpectedComplianceIndicator}'. Exiting {MethodName}.")]
    internal static partial void InputDoesntStartWithExpectedComplianceIndicator(ILogger logger, char expectedComplianceIndicator, string methodName);

    [LoggerMessage(EventId = 101, Level = LogLevel.Information, Message = "Header contains '{ExpectedFileType}'. Forcefully ensuring that the header is valid.")]
    internal static partial void ForcefullyEnsuringValidHeader(ILogger logger, string expectedFileType);

    [LoggerMessage(EventId = 102, Level = LogLevel.Information, Message = "Header contains 'AAMVA'. This is from an earlier spec. Replacing the old header with the current valid header text.")]
    internal static partial void ReplacingOldAAMVAHeaderWithCurrentValidHeader(ILogger logger);

    [LoggerMessage(EventId = 103, Level = LogLevel.Warning, Message = "Header is malformed, and starts with '@\\r\\n\\u0030A'. Changing it to '@\\r\\n\\u0030\\r\\nA'. A later method will remove incorrect \\r characters.")]
    internal static partial void FixingMalformedHeader(ILogger logger);

    [LoggerMessage(EventId = 104, Level = LogLevel.Information, Message = "Scanned text contains \\r characters. Removing them.")]
    internal static partial void TextContainsCarriageReturns(ILogger logger);

    [LoggerMessage(EventId = 105, Level = LogLevel.Information, Message = "Adding the single allowed \\r character back to the header.")]
    internal static partial void AddingRequiredCarriageReturnToHeader(ILogger logger);
}
