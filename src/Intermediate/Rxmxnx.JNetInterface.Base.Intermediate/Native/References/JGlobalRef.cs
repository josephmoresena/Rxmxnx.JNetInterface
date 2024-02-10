namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI global handle for fully-qualified-class objects (<c>jobject</c>).
/// Represents a native signed integer which serves as opaque identifier for any
/// object.
/// </summary>
/// <remarks>This identifier will be valid until it is explicitly unloaded.</remarks>
[StructLayout(LayoutKind.Sequential)]
internal readonly partial struct JGlobalRef : IObjectGlobalReferenceType<JGlobalRef>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JGlobal;

	/// <summary>
	/// Internal <see cref="JObjectLocalRef"/> reference.
	/// </summary>
	private readonly JObjectLocalRef _value;

	/// <summary>
	/// JNI value as local reference.
	/// </summary>
	public JObjectLocalRef Value => this._value;
	/// <inheritdoc/>
	public IntPtr Pointer => this._value.Pointer;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JGlobalRef() => this._value = default;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Int32 GetHashCode() => this._value.GetHashCode();
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Boolean Equals([NotNullWhen(true)] Object? obj)
		=> obj is JGlobalRef jGlobalRef && this._value.Equals(jGlobalRef._value);
}