﻿namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for array objects (<c>jlongArray</c>). Represents a native signed integer
/// which serves as opaque identifier for a primitive array object (<c>long[]</c>).
/// This handle is valid only for the thread who owns the reference.
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
public readonly partial struct JLongArrayLocalRef : IFixedPointer, IArrayLocalRef, INative<JObjectArrayLocalRef>
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

	/// <inheritdoc/>
	public Boolean Equals(JArrayLocalRef other) => this._value.Equals(other);
}