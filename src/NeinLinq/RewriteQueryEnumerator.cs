﻿namespace NeinLinq;

/// <summary>
/// Proxy for query enumerator.
/// </summary>
public class RewriteQueryEnumerator<T> : IEnumerator<T>
#if ASYNC_INTERFACES
    , IAsyncEnumerator<T>
#endif
{
    private readonly IEnumerator<T> enumerator;

    /// <summary>
    /// Create a new enumerator proxy.
    /// </summary>
    /// <param name="enumerator">The actual enumerator.</param>
    public RewriteQueryEnumerator(IEnumerator<T> enumerator)
    {
        if (enumerator is null)
            throw new ArgumentNullException(nameof(enumerator));

        this.enumerator = enumerator;
    }

    /// <inheritdoc />
    object? IEnumerator.Current
        => enumerator.Current;

    /// <inheritdoc />
    public T Current
        => enumerator.Current;

    /// <inheritdoc />
    public bool MoveNext()
        => enumerator.MoveNext();

    /// <inheritdoc />
    public void Reset()
        => enumerator.Reset();

#if ASYNC_INTERFACES

    /// <inheritdoc />
    public ValueTask<bool> MoveNextAsync()
        => new(enumerator.MoveNext());

    /// <summary>
    /// Releases all resources.
    /// </summary>
    public ValueTask DisposeAsync()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
        return default;
    }

#endif

    /// <summary>
    /// Releases all resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes of the resources (other than memory).
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources;
    /// false to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            enumerator.Dispose();
        }
    }
}
