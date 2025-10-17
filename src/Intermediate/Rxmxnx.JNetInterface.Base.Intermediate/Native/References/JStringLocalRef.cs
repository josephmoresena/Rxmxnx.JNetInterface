namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for string objects (<c>jstring</c>). Represents a native-signed integer
/// which serves as opaque identifier for an string object (<c>java.lang.String</c>).
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
[StructLayout(LayoutKind.Explicit)]
public readonly partial struct JStringLocalRef : IObjectReferenceType, INativePointerType<JStringLocalRef>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JString;

	/// <summary>
	/// Internal <see cref="JObjectLocalRef"/> reference.
	/// </summary>
	[FieldOffset(0)]
	private readonly JObjectLocalRef _value;

	/// <summary>
	/// JNI local reference.
	/// </summary>
	public JObjectLocalRef Value => this._value;
	/// <inheritdoc/>
	public IntPtr Pointer => this._value.Pointer;

	/// <summary>
	/// Constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private JStringLocalRef(IntPtr value) : this(new JObjectLocalRef(value)) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	internal JStringLocalRef(JObjectLocalRef value) => this._value = value;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JStringLocalRef INativePointerType<JStringLocalRef>.New(IntPtr value) => new(value);
}