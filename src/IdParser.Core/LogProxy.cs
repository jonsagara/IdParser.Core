namespace IdParser.Core;

// Mad props to StackExchange.Redis for this.
// https://github.com/StackExchange/StackExchange.Redis

internal sealed class LogProxy : IDisposable
{
    public static LogProxy? TryCreate(TextWriter? writer)
        => writer is null
        ? null
        : new LogProxy(writer);

    public override string ToString()
    {
        string? s = null;
        if (_log is not null)
        {
            lock (SyncLock)
            {
                s = _log?.ToString();
            }
        }
        return s ?? base.ToString() ?? string.Empty;
    }

#pragma warning disable CA2213 // Disposable fields should be disposed: We don't want to dispose the caller's text writer.
    private TextWriter? _log;
#pragma warning restore CA2213 // Disposable fields should be disposed

    internal static Action<string> NullWriter = _ => { };

    public object SyncLock => this;

    private LogProxy(TextWriter log)
        => _log = log;

    public void WriteLine()
    {
        if (_log is not null) // note: double-checked
        {
            lock (SyncLock)
            {
                _log?.WriteLine();
            }
        }
    }

    public void WriteLine(string? message = null)
    {
        if (_log is not null) // note: double-checked
        {
            lock (SyncLock)
            {
                _log?.WriteLine($"{DateTime.UtcNow:HH:mm:ss.ffff}: {message}");
            }
        }
    }
    public void WriteLine(string prefix, string message)
    {
        if (_log is not null) // note: double-checked
        {
            lock (SyncLock)
            {
                _log?.WriteLine($"{DateTime.UtcNow:HH:mm:ss.ffff}: {prefix}{message}");
            }
        }
    }
    public void Dispose()
    {
        if (_log is not null) // note: double-checked
        {
            lock (SyncLock)
            {
                // We're releasing the reference, but not disposing of the caller's TextWriter.
                _log = null;
            }
        }
    }
}
