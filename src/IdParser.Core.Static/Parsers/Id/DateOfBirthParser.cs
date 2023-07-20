namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DBB")]
internal static class DateOfBirthParser
{
    internal static string? Parse(string input)
    {
        if (DateHasNoValue(input))
        {
            return;
        }

        IdCard.DateOfBirth = ParseDate(input);
    }
}
