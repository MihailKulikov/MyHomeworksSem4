namespace LazyRealisation

/// <summary>
/// Provides support for lazy initialization.
/// </summary>
/// <typeparam name="T">The type of object that is being lazily initialized.</typeparam>
type ILazy<'a> =
    /// <summary>
    /// Gets the lazily initialized value of the current <see cref="ILazy{T}"/> instance.
    /// </summary>
    /// <returns>The lazily initialized value of the current <see cref="ILazy{T}"/> instance.</returns>
    abstract member Get: unit -> 'a