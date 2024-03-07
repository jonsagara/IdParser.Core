using IdParser.Core.Constants;

namespace IdParser.Core;

/// <summary>
/// A field from an AAMVA-compliant ID string.
/// </summary>
/// <typeparam name="T">The CLR type of the value.</typeparam>
/// <param name="ElementId">The three-character element identifier.</param>
/// <param name="Value">The parsed value of the element.</param>
/// <param name="RawValue">The raw string value as read from the scanned text.</param>
/// <param name="Error">If unable to parse the raw value, a message describing the error.</param>
/// <param name="Present">True if the ElementId was present in the scanned text; false otherwise.</param>
public readonly record struct Field<T>(
    string? ElementId,
    T? Value,
    string? RawValue,
    string? Error,
    bool Present
    )
{
    /// <summary>
    /// Returns true if the field has a non-null, non-white space <see cref="Error"/> message; false otherwise.
    /// </summary>
    public bool HasError
        => !string.IsNullOrWhiteSpace(Error);
}

internal static class FieldHelpers
{
    private static Field<TValue> Uninitialized<TValue>(string? elementId)
        => new Field<TValue>(ElementId: elementId, Value: default, RawValue: null, Error: null, Present: false);

    internal static Field<TValue> ParsedField<TValue>(string? elementId, TValue value, string? rawValue)
        => new Field<TValue>(ElementId: elementId, Value: value, RawValue: rawValue, Error: null, Present: true);

    internal static Field<TValue> UnparsedField<TValue>(string? elementId, string? rawValue, string error)
        => new Field<TValue>(ElementId: elementId, Value: default, RawValue: rawValue, Error: error, Present: true);


    internal static readonly Field<IssuerIdentificationNumber> UninitializedIssuerIdentificationNumber = Uninitialized<IssuerIdentificationNumber>(elementId: null);
    internal static readonly Field<AAMVAVersion> UninitializedAAMVAVersion = Uninitialized<AAMVAVersion>(elementId: null);
    internal static readonly Field<ComplianceType?> UninitializedNullableComplianceType = Uninitialized<ComplianceType?>(elementId: SubfileElementIds.ComplianceType);
    internal static readonly Field<Country> UninitializedCountry = Uninitialized<Country>(elementId: SubfileElementIds.Country);
    internal static readonly Field<Sex?> UninitializedNullableSex = Uninitialized<Sex?>(elementId: SubfileElementIds.Sex);
    internal static readonly Field<EyeColor?> UninitializedNullableEyeColor = Uninitialized<EyeColor?>(elementId: SubfileElementIds.EyeColor);
    internal static readonly Field<HairColor?> UninitializedNullableHairColor = Uninitialized<HairColor?>(elementId: SubfileElementIds.HairColor);
    internal static readonly Field<Ethnicity?> UninitializedNullableEthnicity = Uninitialized<Ethnicity?>(elementId: SubfileElementIds.Ethnicity);
    internal static readonly Field<Height?> UninitializedNullableHeight = Uninitialized<Height?>(elementId: SubfileElementIds.Height);
    internal static readonly Field<Weight?> UninitializedNullableWeight = Uninitialized<Weight?>(elementId: null); // Could be WeightInPounds or WeightInKilograms.
    internal static readonly Field<WeightRange?> UninitializedNullableWeightRange = Uninitialized<WeightRange?>(elementId: SubfileElementIds.WeightRange);

    internal static readonly Field<bool> UninitializedBoolean = Uninitialized<bool>(elementId: null);
    internal static readonly Field<bool?> UninitializedNullableBoolean = Uninitialized<bool?>(elementId: null);
    internal static readonly Field<DateTime?> UninitializedNullableDateTime = Uninitialized<DateTime?>(null);
    internal static readonly Field<int> UninitializedInt32 = Uninitialized<int>(elementId: null);
    internal static readonly Field<string> UninitializedString = Uninitialized<string>(elementId: null);
    internal static readonly Field<string?> UninitializedNullableString = Uninitialized<string?>(elementId: null);
}
