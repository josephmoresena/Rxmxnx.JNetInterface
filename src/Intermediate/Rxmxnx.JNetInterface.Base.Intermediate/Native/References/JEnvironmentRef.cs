﻿namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// <c>JNIEnv</c> pointer. Represents a pointer to a <c>JNIEnv</c> object.
/// </summary>
/// <remarks>This references is valid only for the thread who owns the reference.</remarks>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JEnvironmentRef : INativeReferenceType, IReadOnlyReferenceable<JEnvironmentValue>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JEnvironmentRef;

	/// <summary>
	/// Internal pointer value.
	/// </summary>
	private readonly ReadOnlyValPtr<JEnvironmentValue> _value;

	/// <inheritdoc/>
	public IntPtr Pointer => this._value;

	/// <summary>
	/// <see langword="readonly ref"/> <see cref="JEnvironmentValue"/> from this pointer.
	/// </summary>
	internal ref readonly JEnvironmentValue Reference => ref this._value.Reference;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JEnvironmentRef() => this._value = ReadOnlyValPtr<JEnvironmentValue>.Zero;

	ref readonly JEnvironmentValue IReadOnlyReferenceable<JEnvironmentValue>.Reference => ref this.Reference;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Int32 GetHashCode() => this._value.GetHashCode();
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Boolean Equals([NotNullWhen(true)] Object? obj)
		=> obj is JEnvironmentRef jEnvRef && this._value.Equals(jEnvRef._value);
}