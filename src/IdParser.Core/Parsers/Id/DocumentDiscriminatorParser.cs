﻿namespace IdParser.Core.Parsers.Id;

internal static class DocumentDiscriminatorParser
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
