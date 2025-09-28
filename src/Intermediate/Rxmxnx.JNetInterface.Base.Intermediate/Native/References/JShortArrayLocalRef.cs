namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for array objects (<c>jshortArray</c>). Represents a native-signed integer
/// which serves as opaque identifier for a primitive array object (<c>short[]</c>).
/// This handle is valid only for the thread who owns the reference.
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JShortArrayLocalRef : IArrayReferenceType, INativePointerType<JShortArrayLocalRef>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JShortArray;

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
	internal JShortArrayLocalRef(IntPtr value) : this(new JArrayLocalRef(value)) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	internal JShortArrayLocalRef(JArrayLocalRef value) => this._value = value;

	/// <inheritdoc/>
	public Boolean Equals(JArrayLocalRef other) => this._value.Equals(other);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JShortArrayLocalRef INativePointerType<JShortArrayLocalRef>.New(IntPtr value) => new(value);
}