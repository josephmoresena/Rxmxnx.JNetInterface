namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for array objects (<c>jbooleanArray</c>). Represents a native-signed integer
/// which serves as opaque identifier for a primitive array object (<c>boolean[]</c>).
/// This handle is valid only for the thread who owns the reference.
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
[StructLayout(LayoutKind.Explicit)]
public readonly partial struct JBooleanArrayLocalRef : IArrayReferenceType, INativePointerType<JBooleanArrayLocalRef>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JBooleanArray;

	/// <summary>
	/// Internal <see cref="JArrayLocalRef"/> reference.
	/// </summary>
	[FieldOffset(0)]
	private readonly JArrayLocalRef _value;

	/// <summary>
	/// JNI local reference.
	/// </summary>
	public JObjectLocalRef Value => this._value.Value;
	/// <inheritdoc/>
	public JArrayLocalRef ArrayValue => this._value;
	/// <inheritdoc/>
	public IntPtr Pointer => this._value.Value.Pointer;

	/// <summary>
	/// Constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal JBooleanArrayLocalRef(IntPtr value) : this(new JArrayLocalRef(value)) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	internal JBooleanArrayLocalRef(JArrayLocalRef value) => this._value = value;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean Equals(JArrayLocalRef other) => this._value.Equals(other);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JBooleanArrayLocalRef INativePointerType<JBooleanArrayLocalRef>.New(IntPtr value) => new(value);
}