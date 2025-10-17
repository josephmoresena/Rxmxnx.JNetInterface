namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI weak global handle for fully-qualified-class objects (<c>jweak</c>).
/// Represents a native-signed integer which serves as opaque identifier for any
/// object.
/// </summary>
/// <remarks>This identifier may be valid until it is explicitly unloaded.</remarks>
[StructLayout(LayoutKind.Explicit)]
public readonly partial struct JWeakRef : IObjectGlobalReferenceType, INativePointerType<JWeakRef>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JWeak;

	/// <summary>
	/// Internal <see cref="JObjectLocalRef"/> reference.
	/// </summary>
	[FieldOffset(0)]
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
	public JWeakRef() : this(IntPtr.Zero) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private JWeakRef(IntPtr value) : this(new JObjectLocalRef(value)) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	private JWeakRef(JObjectLocalRef value) => this._value = value;

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

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JWeakRef INativePointerType<JWeakRef>.New(IntPtr value) => new(value);
}