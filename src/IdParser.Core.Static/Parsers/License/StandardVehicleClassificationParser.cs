﻿namespace IdParser.Core.Static.Parsers.License;

internal static class StandardVehicleClassificationParser
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