namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for array objects (<c>jlongArray</c>). Represents a native-signed integer
/// which serves as opaque identifier for a primitive array object (<c>long[]</c>).
/// This handle is valid only for the thread who owns the reference.
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JLongArrayLocalRef : IArrayReferenceType, INativePointerType<JLongArrayLocalRef>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JLongArray;

	/// <summary>
	/// Internal <see cref="JArrayLocalRef"/> reference.
	/// </summary>
	private readonly JArrayLocalRef _value;

	/// <summary>
	/// JNI local reference.
	/// </summary>
	public JObjectLocalRef Value => this._value.Value;
	/// <inheritdoc/>
	public JArrayLocalRef ArrayValue => this._value;
	/// <inheritdoc/>
	public IntPtr Pointer => this._value.Pointer;

	/// <summary>
	/// Constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal JLongArrayLocalRef(IntPtr value) : this(new JArrayLocalRef(value)) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	internal JLongArrayLocalRef(JArrayLocalRef value) => this._value = value;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean Equals(JArrayLocalRef other) => this._value.Equals(other);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JLongArrayLocalRef INativePointerType<JLongArrayLocalRef>.New(IntPtr value) => new(value);
}