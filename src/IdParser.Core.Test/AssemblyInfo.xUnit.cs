// xUnit.net v3 reintroduces the ability to capture output that is written to Console, Debug, and Trace
//   through two assembly-level attributes. This allows us to get rid of the clunky ITestOutputHelper.
// See: https://xunit.net/docs/capturing-output

// Capture both standard output and standard error.
[assembly: CaptureConsole]

// Capture both Trace and Debug
[assembly: CaptureTrace]