# IdParser.Core

[![publish](https://github.com/jonsagara/IdParser.Core/actions/workflows/build-and-publish.yml/badge.svg)](https://github.com/jonsagara/IdParser.Core/actions?query=workflow%3Apublish)
[![NuGet](https://img.shields.io/nuget/v/IdParser.Core?label=NuGet)](https://www.nuget.org/packages/IdParser.Core/)

`IdParser.Core` can be used to parse AAMVA-compliant driver's licenses and ID cards into objects that you can
work with. More information on the versions of the AAMVA standard can be found [here](http://www.aamva.org/DL-ID-Card-Design-Standard/).
More information on the D20 Data Dictionary can be found [here](https://www.aamva.org/getmedia/d4c16fd8-2193-490c-a5ea-21607a3bd51a/D20-Traffic-Records-Systems-Data-Dictionary-(AMIE).pdf).

## Usage

1. Include the using
```csharp
using IdParser.Core;
```

2. Then you're off to the races!

```csharp
var parseResult = Barcode.Parse(barcode);
Console.WriteLine(parseResult.Card.StreetLine1.Value); // "123 NORTH STATE ST."
Console.WriteLine(parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault()); // "New York"

if (parseResult.Card is DriversLicense license)
{
    Console.WriteLine(license.Jurisdiction.VehicleClass.Value); // "C"
}
```

If a field fails to parse, you can inspect the field's raw value:

```csharp
if (parseResult.Card.FirstName.HasError)
{
    Console.WriteLine(parseResult.Card.FirstName.Error); // The error message, if any.
    Console.WriteLine(parseResult.Card.FirstName.RawValue); // The raw value from the scanned ID text.
}
```

You can also check to see whether a field was present in the scanned ID text:

```csharp
Console.WriteLine($"First Name present? {parseResult.Card.FirstName.Present}");
```

### More Examples

Take a look at the unit test project for more examples and usage.


## Client

The ```IdParser.Core.Client``` project is a handy GUI application to help test and verify that an ID
will be parsed correctly. The app works with both OPOS and HID keyboard emulation scanners.

![](https://raw.githubusercontent.com/jonsagara/IdParser.Core/main/IdParser.Client.png)


## Credit

This is a fork of Connor O'Shea's [IdParser](https://github.com/c0shea/IdParser) library. Big thanks to him for all of his work. 

The main changes I made for `v1.0.0` of `IdParser.Core` are:

- Dropped support for `.NET Framework`. If you need that, please use the original `IdParser`.
- Supports only `.NET 6` and above. If you need support for earlier versions of `.NET`/`.NET Core`, please use the original `IdParser`.
- The parser classes are now static and no longer instantiated by `Activator.CreateInstance`. This reduced memory allocations per call considerably,
  and also sped things up.
- The `Abbreviation`, `Country`, and `Description` attribute values are now cached so that we don't have to repeatedly use reflection to get their values.
- Unhandled parsing exceptions are now rethrown, regardless of what the caller passes for the validation level (`None` or `Strict`).
- Where possible, modernized the code base to use newer framework and language features, such as `Span<T>`.
- The return value of `Parse` is now an object that returns both the ID card and a collection of any unknown element Ids.
- `Parse` now accepts an optional `Microsoft.Extensions.Logging.ILoggerFactory` parameter. When not null, the library will log to an instance of `ILogger`.