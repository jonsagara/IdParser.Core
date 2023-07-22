# IdParser.Core

![Build Status](https://github.com/jonsagara/IdParser.Core/actions/workflows/build-and-publish.yml/badge.svg)

This is a fork of Connor O'Shea's [IdParser](https://github.com/c0shea/IdParser) library. Big thanks to him for all of his work. 

The main changes I made are:

- Dropped support for `.NET Framework`. If you need that, please use the original `IdParser`.
- Supports only `.NET 6` and above. If you need support for earlier versions of `.NET`/`.NET Core`, please use the original `IdParser`.
- The parser classes are now static and no longer instantiated by `Activator.CreateInstance`. This reduced memory allocations per call considerably,
  and also sped things up.
- The `Abbreviation`, `Country`, and `Description` attribute values are now cached so that we don't have to repeatedly use reflection to get their values.
- Unhandled parsing exceptions are now rethrown, regardless of what the caller passes for the validation level (`None` or `Strict`).
- Where possible, modernized the code base to use newer framework and language features, such as `Span<T>`.
- The return value of `Parse` is now an object that return both the ID card and a collection of any unknown element Ids.
- `Parse` now accepts an optional `TextWriter` parameter. When not null, the library will log to the `TextWriter`.
 
The original README follows, slightly modified to match this updated version of the library.

# ID Parser

ID Parser can be used to parse AAMVA-compliant driver's licenses and ID cards into objects that you can
work with. More information on the versions of the AAMVA standard can be found [here](http://www.aamva.org/DL-ID-Card-Design-Standard/).
More information on the D20 Data Dictionary can be found [here](https://www.aamva.org/getmedia/d4c16fd8-2193-490c-a5ea-21607a3bd51a/D20-Traffic-Records-Systems-Data-Dictionary-(AMIE).pdf).

## Usage

1. Include the using
```cs
using IdParser.Core;
```

2. Then you're off to the races!

```cs
var parseResult = Barcode.Parse(barcode);
Console.WriteLine(parseResult.Card.Address.StreetLine1); // "123 NORTH STATE ST."
Console.WriteLine(parseResult.Card.IssuerIdentificationNumber.GetDescriptionOrDefault()); // "New York"

if (parseResult.Card is DriversLicense license)
{
    Console.WriteLine(license.Jurisdiction.VehicleClass); // "C"
}
```

### More Examples

Take a look at the unit test project for more examples and usage.

## Client

The ```IdParser.Core.Client``` project is a handy GUI application to help test and verify that an ID
will be parsed correctly. The app works with both OPOS and HID keyboard emulation scanners.

![](https://raw.githubusercontent.com/jonsagara/IdParser.Core/main/IdParser.Client.png)

## FAQ

* **I can't build ```IdParser.Core.Client```. It's missing a required dependency.**
  You need to have [Microsoft POS for .NET](https://www.microsoft.com/en-us/download/details.aspx?id=55758&WT.mc_id=rss_alldownloads_all)
  installed. The ```Microsoft.PointOfService``` dll is GAC'd and will allow you to build and run
  the client app.

* **The ```Height``` class has the wrong ```TotalInches``` or ```Centimeters```.**
  The AAMVA standard has no decimal places in the height subfile record.
  As a result, the conversion between inches and centimeters will be off.

* **The library is throwing `ArgumentExcpetions` for every barcode I'm passing in.**
  By default, all barcodes are parsed using the `Strict` validation level. All barcodes are expected to
  adhere exactly to the AAMVA standard as defined in the PDFs for parsing to succeed. This is the
  recommended level for scanners using OPOS. However, if HID keyboard emulation is used, especially when
  scanning using a web browser, the expected data can become malformed. You can try using the `None`
  validation level, however this is not guaranteed to work in all cases. Data elements may be skipped
  and exceptions may still be thrown.

## Find IDs Not in Tests Regex

```regex
DAJ(?!(AL|AR|AZ|AK|CA|CO|CT|DE|FL|GA|HI|IA|ID|IL|IN|KS|KY|LA|MA|MD|ME|MI|MN|MO|MS|MT|NC|ND|NH|NJ|NM|NV|NY|OH|ON|OR|PA|PE|PR|RI|SC|TN|TX|UT|VA|VT|WA|WI|WV|QC|OK|NS|NE|NB|AB|SD|DC))[A-Z]+
```
