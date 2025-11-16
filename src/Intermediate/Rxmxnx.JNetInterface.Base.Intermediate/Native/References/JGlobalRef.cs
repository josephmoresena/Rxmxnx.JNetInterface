namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI global handle for fully-qualified-class objects (<c>jobject</c>).
/// Represents a native-signed integer which serves as opaque identifier for any
/// object.
/// </summary>
/// <remarks>This identifier will be valid until it is explicitly unloaded.</remarks>
[StructLayout(LayoutKind.Explicit)]
public readonly partial struct JGlobalRef : IObjectGlobalReferenceType, INativePointerType<JGlobalRef>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JGlobal;

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
	public JGlobalRef() : this(IntPtr.Zero) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private JGlobalRef(IntPtr value) : this(new JObjectLocalRef(value)) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	private JGlobalRef(JObjectLocalRef value) => this._value = value;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Int32 GetHashCode() => this._value.GetHashCode();
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Boolean Equals([NotNullWhen(true)] Object? obj)
		=> obj switch
		{
			JGlobalRef globalRef => this._value.Equals(globalRef._value),
			IWrapper<JGlobalRef> wrapper => this._value.Equals(wrapper.Value._value),
			_ => false,
		};

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JGlobalRef INativePointerType<JGlobalRef>.New(IntPtr value) => new(value);
}