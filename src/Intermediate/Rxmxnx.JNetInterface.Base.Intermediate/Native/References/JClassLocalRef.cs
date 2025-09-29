namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for class objects (<c>jclass</c>). Represents a native-signed integer
/// which serves as opaque identifier for a class object (<c>java.lang.Class&lt;?&gt;</c>).
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
[StructLayout(LayoutKind.Explicit)]
public readonly partial struct JClassLocalRef : IObjectReferenceType, INativePointerType<JClassLocalRef>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JClass;

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
	internal JClassLocalRef(IntPtr value) : this(new JObjectLocalRef(value)) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal JClassLocalRef(JWeakRef value) : this(value.Value) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal JClassLocalRef(JGlobalRef value) : this(value.Value) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	internal JClassLocalRef(JObjectLocalRef value) => this._value = value;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JClassLocalRef INativePointerType<JClassLocalRef>.New(IntPtr value) => new(value);
}