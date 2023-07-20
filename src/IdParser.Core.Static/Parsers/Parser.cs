using IdParser.Core.Static.Constants;
using IdParser.Core.Static.Parsers.Id;

namespace IdParser.Core.Static.Parsers;

internal static class Parser
{
    internal static void ParseAndSet(string elementId, string data, Country country, IdentificationCard idCard)
    {
        ArgumentNullException.ThrowIfNull(elementId);
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(idCard);

        switch (elementId)
        {
            case SubfileElementIds.DBG:
                idCard.Name.AliasFirst = AliasFirstNameParser.Parse(input: data, idCard.AAMVAVersionNumber);
                break;

            case SubfileElementIds.DBN:
                idCard.Name.AliasLast = AliasLastNameParser.Parse(input: data);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(elementId), elementId, $"Unsupported elementId '{elementId}'.");
        }
    }
}
