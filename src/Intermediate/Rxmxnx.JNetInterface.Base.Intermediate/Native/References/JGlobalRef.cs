namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
///     JNI global handle for fully-qualified-class objects (<c>jobject</c>).
///     Represents a native signed integer which serves as opaque identifier for any
///     object.
/// </summary>
/// <remarks>This identifier will be valid until it is explicitly unloaded.</remarks>
internal readonly struct JGlobalRef : IFixedPointer, INative<JGlobalRef>, IWrapper<JObjectLocalRef>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JGlobal;

	/// <summary>
	///     Internal <see cref="JObjectLocalRef"/> reference.
	/// </summary>
	private readonly JObjectLocalRef _value;

	/// <summary>
	///     JNI value as local reference.
	/// </summary>
	public JObjectLocalRef Value => this._value;
	/// <inheritdoc/>
	public IntPtr Pointer => this._value.Pointer;

	/// <summary>
	///     Parameterless constructor.
	/// </summary>
	public JGlobalRef()
	{
		this._value = default;
	}

	/// <inheritdoc/>
	public override Int32 GetHashCode()
	{
		return HashCode.Combine(this._value);
	}
	/// <inheritdoc/>
	public override Boolean Equals([NotNullWhen(true)] Object? obj)
	{
		return obj is JGlobalRef jGlobalRef && this._value.Equals(jGlobalRef._value);
	}
}