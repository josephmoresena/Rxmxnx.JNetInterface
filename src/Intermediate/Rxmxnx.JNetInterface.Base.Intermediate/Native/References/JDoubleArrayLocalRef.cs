namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for array objects (<c>jdoubleArray</c>). Represents a native signed integer
/// which serves as opaque identifier for a primitive array object (<c>double[]</c>).
/// This handle is valid only for the thread who owns the reference.
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JDoubleArrayLocalRef : IArrayReference<JDoubleArrayLocalRef>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JDoubleArray;

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

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean Equals(JArrayLocalRef other) => this._value.Equals(other);
}