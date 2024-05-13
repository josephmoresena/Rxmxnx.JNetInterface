namespace Rxmxnx.JNetInterface.Native.Identifiers;

/// <summary>
/// JNI handle for methods (<c>methodID</c>). Represents a native-signed integer which serves
/// as opaque identifier for a declared method in a <c>class</c>.
/// </summary>
/// <remarks>This handle will be valid until the associated <c>class</c> is unloaded.</remarks>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JMethodId : IAccessibleIdentifierType<JMethodId>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JMethod;

	/// <summary>
	/// Internal native signed integer
	/// </summary>
	private readonly IntPtr _value;

	/// <inheritdoc/>
	public IntPtr Pointer => this._value;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JMethodId() => this._value = IntPtr.Zero;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Int32 GetHashCode() => this._value.GetHashCode();
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Boolean Equals([NotNullWhen(true)] Object? obj)
		=> obj is JMethodId methodId && this._value.Equals(methodId._value);
}