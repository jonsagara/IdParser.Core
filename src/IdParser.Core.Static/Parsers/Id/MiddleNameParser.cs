﻿namespace IdParser.Core.Static.Parsers.Id;

internal static class MiddleNameParser
{
    internal static string? Parse(string input)
    {
        if (ParserHelper.StringHasNoValue(input))
        {
            return null;
        }

        return input;
    }
}