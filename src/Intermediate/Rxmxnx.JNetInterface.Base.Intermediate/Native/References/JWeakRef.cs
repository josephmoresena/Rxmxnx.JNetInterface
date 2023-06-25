﻿namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI weak global handle for fully-qualified-class objects (<c>jweak</c>).
/// Represents a native signed integer which serves as opaque identifier for any
/// object.
/// </summary>
/// <remarks>This identifier may be valid until it is explicitly unloaded.</remarks>
internal readonly struct JWeakRef : INative<JWeakRef>, IWrapper<JObjectLocalRef>, IFixedPointer
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JWeak;

	/// <summary>
	/// Internal <see cref="JObjectLocalRef"/> reference.
	/// </summary>
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
	public JWeakRef()
	{
		this._value = default;
	}

	/// <inheritdoc/>
	public override Int32 GetHashCode()
	{
		return HashCode.Combine(this._value);
	}
	/// <inheritdoc/>
	public override Boolean Equals([NotNullWhen(true)] Object? obj)
	{
		return obj is JWeakRef jWeakRef && this._value.Equals(jWeakRef._value);
	}
}