namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a Java datatype.
/// </summary>
public interface IDataType
{
    /// <summary>
    /// Java datatype class name.
    /// </summary>
    static abstract CString ClassName { get; }
    /// <summary>
    /// Java datatype signature name.
    /// </summary>
    static abstract CString Signature { get; }

    /// <summary>
    /// Primitive metadata.
    /// </summary>
    internal static abstract JPrimitiveMetadata? PrimitiveMetadata { get; }
}

/// <summary>
/// This interface exposes a Java datatype.
/// </summary>
/// <typeparam name="TSelf">Type of current Java datatype.</typeparam>
public interface IDataType<out TSelf> : IDataType where TSelf : IDataType<TSelf>
{
    /// <summary>
    /// Creates a <typeparamref name="TSelf"/> instance from <paramref name="jObject"/>.
    /// </summary>
    /// <param name="jObject">A <see cref="JObject"/> instance.</param>
    /// <returns>A <typeparamref name="TSelf"/> instance from <paramref name="jObject"/>.</returns>
    static abstract TSelf? Create(JObject? jObject);
}