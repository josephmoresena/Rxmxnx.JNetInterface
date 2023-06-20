namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// <c>jvalue</c> union. This structure can represent any reference type as any primitive type.
/// </summary>
internal readonly partial struct JValue : INative<JValue>
{
    /// <inheritdoc/>
    public static JNativeType Type => JNativeType.JValue;

    /// <summary>
    /// Represents the empty <see cref="JValue"/>. This field is read-only.
    /// </summary>
    public static readonly JValue Empty = default;
    /// <summary>
    /// Size in bytes of <see cref="JValue"/> structure. 
    /// </summary>
    /// <remarks>In both 32bit and 64bit process, 8 bytes.</remarks>
    public static readonly Int32 Size = NativeUtilities.SizeOf<JValue>();
    /// <summary>
    /// Size in bytes of <see cref="IntPtr"/> structure. 
    /// </summary>
    public static readonly Int32 PointerSize = NativeUtilities.SizeOf<IntPtr>();
    /// <summary>
    /// Indicates whether <see cref="JValue"/> size is equals to <see cref="IntPtr"/> size.
    /// </summary>
    public static readonly Boolean IsMemorySize = (NativeUtilities.SizeOf<JValue>() == NativeUtilities.SizeOf<IntPtr>());

    /// <summary>
    /// Indicates whether the current instance has the <see langword="default"/> value.
    /// </summary>
    public Boolean IsDefault => isDefault(this);

    #region Overrided Methods
    /// <inheritdoc/>
    public override String ToString() => INative.ToString(this);
    #endregion

    /// <summary>
    /// Creates a new <see cref="JValue"/> value from a <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="T">Type of value.</typeparam>
    /// <param name="value">Value.</param>
    /// <returns><see cref="JValue"/> value.</returns>
    /// <exception cref="InsufficientMemoryException"/>
    public static JValue Create<T>(in T value) where T : unmanaged
    {
        if (NativeUtilities.SizeOf<T>() > JValue.Size)
            throw new InsufficientMemoryException();

        JValue result = default;
        ref JValue refResult = ref result;
        Span<Byte> resultByte = result.AsBytes();
        ReadOnlySpan<Byte> source = NativeUtilities.AsBytes(value);
        source.CopyTo(resultByte);
        return result;
    }
    /// <summary>
    /// Interprests <paramref name="jValue"/> as <typeparamref name="T"/> reference.
    /// </summary>
    /// <typeparam name="T">Type of value.</typeparam>
    /// <param name="jValue">A <see cref="JValue"/> reference.</param>
    /// <returns>A <typeparamref name="T"/> reference.</returns>
    /// <exception cref="InsufficientMemoryException"/>
    public static ref T As<T>(ref JValue jValue) where T : unmanaged
    {
        if (NativeUtilities.SizeOf<T>() > JValue.Size)
            throw new InsufficientMemoryException();

        return ref Unsafe.As<JValue, T>(ref jValue);
    }
}