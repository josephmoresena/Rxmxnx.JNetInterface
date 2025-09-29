namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for throwable objects (<c>jthrowable</c>). Represents a native-signed integer
/// which serves as opaque identifier for an throwable object (<c>java.lang.Throwable</c>).
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
[StructLayout(LayoutKind.Explicit)]
public readonly partial struct JThrowableLocalRef : IObjectReferenceType, INativePointerType<JThrowableLocalRef>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JThrowable;

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
	internal JThrowableLocalRef(IntPtr value) : this(new JObjectLocalRef(value)) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	internal JThrowableLocalRef(JObjectLocalRef value) => this._value = value;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JThrowableLocalRef INativePointerType<JThrowableLocalRef>.New(IntPtr value) => new(value);
}