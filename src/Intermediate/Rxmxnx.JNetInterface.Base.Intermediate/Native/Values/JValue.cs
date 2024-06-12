namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// <c>jvalue</c> union. This structure can represent any reference type as any primitive type.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
internal readonly partial struct JValue : INativeType
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JValue;

	/// <summary>
	/// Pointer value.
	/// </summary>
	[FieldOffset(0)]
	private readonly IntPtr _pointerValue;
	/// <summary>
	/// Least significant integer (4 bytes).
	/// </summary>
	[FieldOffset(0)]
	private readonly Int32 _lsi;
	/// <summary>
	/// Most significant integer (4 bytes).
	/// </summary>
	[FieldOffset(sizeof(Int32))]
	private readonly Int32 _msi;

	/// <summary>
	/// Represents the empty <see cref="JValue"/>. This field is read-only.
	/// </summary>
	public static readonly JValue Empty = new();
	/// <summary>
	/// Size in bytes of <see cref="JValue"/> structure.
	/// </summary>
	/// <remarks>In both 32bit and 64bit process, 8 bytes.</remarks>
	public static readonly Int32 Size = NativeUtilities.SizeOf<JValue>();
	/// <summary>
	/// Indicates whether <see cref="JValue"/> size is equals to <see cref="IntPtr"/> size.
	/// </summary>
	public static readonly Boolean IsMemorySize = NativeUtilities.SizeOf<JValue>() == NativeUtilities.PointerSize;

	/// <summary>
	/// Indicates whether the current instance has the <see langword="default"/> value.
	/// </summary>
	public Boolean IsDefault => JValue.isDefault(this);

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JValue()
	{
		this._msi = default;
		this._msi = default;
		this._pointerValue = default;
	}

	/// <summary>
	/// Creates a new <see cref="JValue"/> value from a <paramref name="value"/>.
	/// </summary>
	/// <typeparam name="T">Type of value.</typeparam>
	/// <param name="value">Value.</param>
	/// <returns><see cref="JValue"/> value.</returns>
	/// <exception cref="InsufficientMemoryException"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JValue Create<T>(in T value) where T : unmanaged
	{
		CommonValidationUtilities.ThrowIfInvalidType(NativeUtilities.SizeOf<T>());
		Span<JValue> resultSpan = stackalloc JValue[1];
		Span<Byte> resultByte = resultSpan.AsBytes();
		ReadOnlySpan<Byte> source = NativeUtilities.AsBytes(value);
		source.CopyTo(resultByte);
		return resultSpan[0];
	}
}