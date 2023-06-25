namespace Rxmxnx.JNetInterface.Native.Identifiers;

/// <summary>
///     JNI handle for methods (<c>methodID</c>). Represents a native signed integer which serves
///     as opaque identifier for a declared method in a <c>class</c>.
/// </summary>
/// <remarks>This handle will be valid until the associated <c>class</c> is unloaded.</remarks>
internal readonly struct JMethodId : IFixedPointer, INative<JMethodId>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JMethod;

	/// <summary>
	///     Internal native signed integer
	/// </summary>
	private readonly IntPtr _value;

	/// <inheritdoc/>
	public IntPtr Pointer => this._value;

	/// <summary>
	///     Parameterless constructor.
	/// </summary>
	public JMethodId()
	{
		this._value = IntPtr.Zero;
	}

	/// <inheritdoc/>
	public override Int32 GetHashCode()
	{
		return HashCode.Combine(this._value);
	}
	/// <inheritdoc/>
	public override Boolean Equals([NotNullWhen(true)] Object? obj)
	{
		return obj is JMethodId methodId && this._value.Equals(methodId._value);
	}
}