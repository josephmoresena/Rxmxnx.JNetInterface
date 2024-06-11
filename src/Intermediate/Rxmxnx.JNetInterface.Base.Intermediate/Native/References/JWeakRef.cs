namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI weak global handle for fully-qualified-class objects (<c>jweak</c>).
/// Represents a native-signed integer which serves as opaque identifier for any
/// object.
/// </summary>
/// <remarks>This identifier may be valid until it is explicitly unloaded.</remarks>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JWeakRef : IObjectGlobalReferenceType, INativeType<JWeakRef>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JWeak;

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
	public JWeakRef() => this._value = default;

	/// <inheritdoc/>
	public override Int32 GetHashCode() => this._value.GetHashCode();
	/// <inheritdoc/>
	public override Boolean Equals([NotNullWhen(true)] Object? obj)
		=> obj switch
		{
			JWeakRef weakRef => this._value.Equals(weakRef._value),
			IWrapper<JWeakRef> wrapper => this._value.Equals(wrapper.Value._value),
			_ => false,
		};
}