namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a JNI native value.
/// </summary>
public interface INative
{
    /// <summary>
    /// Current instance text value.
    /// </summary>
    internal String TextValue
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            if (this is IFixedPointer fprt)
                return fprt.Pointer.ToString(CommonConstants.IntPtrToStringFormat);
            if (this is JValue jValue)
                return Convert.ToHexString(NativeUtilities.AsBytes(jValue));
            if (this is JNativeInterface jNative)
                return Convert.ToHexString(NativeUtilities.AsBytes(jNative));
            if (this is JInvokeInterface jInvoke)
                return Convert.ToHexString(NativeUtilities.AsBytes(jInvoke));
            return this.ToString()!;
        }
    }

    /// <summary>
    /// Current value as <see cref="String"/>.
    /// </summary>
    /// <returns>Current instance as <see cref="String"/>.</returns>
    String AsString();

    /// <summary>
    /// Indicates the native type.
    /// </summary>
    static abstract JNativeType Type { get; }

    /// <summary>
    /// <paramref name="native"/> as <see cref="String"/>.
    /// </summary>
    /// <param name="native"><see cref="INative"/> instance.</param>
    /// <returns><see cref="INative"/> instance as <see cref="String"/>.</returns>
    internal static String ToString(INative native) => native.AsString();
}

/// <summary>
/// This interface exposes a JNI native value.
/// </summary>
internal interface INative<TSelf> : INative
    where TSelf : unmanaged, INative<TSelf>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    String INative.AsString() => String.Format(CommonConstants.NativeReferenceFormat, TSelf.Type.GetTypeName(), this.TextValue);
}