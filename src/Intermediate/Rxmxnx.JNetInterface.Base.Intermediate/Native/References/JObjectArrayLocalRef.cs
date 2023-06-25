namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for array objects (<c>jobjectArray</c>). Represents a native signed integer
/// which serves as opaque identifier for a object array object (<c>object[]</c>).
/// This handle is valid only for the thread who owns the reference.
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
public readonly partial struct JObjectArrayLocalRef : IFixedPointer,
	INative<JObjectArrayLocalRef>, IWrapper<JObjectLocalRef>, IEquatable<JArrayLocalRef>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JObjectArray;

	/// <summary>
	/// Internal <see cref="JArrayLocalRef"/> reference.
	/// </summary>
	private readonly JArrayLocalRef _value;

	/// <summary>
	/// JNI local reference.
	/// </summary>
	public JObjectLocalRef Value => this._value.Value;
	/// <summary>
	/// JNI array local reference.
	/// </summary>
	public JArrayLocalRef ArrayValue => this._value;
	/// <inheritdoc/>
	public IntPtr Pointer => this._value.Value.Pointer;

	/// <inheritdoc/>
	public Boolean Equals(JArrayLocalRef other)
	{
		return this._value.Equals(other);
	}
}