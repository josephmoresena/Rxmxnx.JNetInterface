﻿namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// <c>JNIEnv</c> struct. Contains a pointer to a <c>JNINativeInterface_</c> object.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly partial struct JEnvironmentValue : INativeReferenceType, IReadOnlyReferenceable<JNativeInterface>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JEnvironment;

	/// <summary>
	/// Internal <see cref="JNativeInterface"/> pointer.
	/// </summary>
	private readonly ReadOnlyValPtr<JNativeInterface> _functions;

	/// <summary>
	/// <see langword="readonly ref"/> <see cref="JNativeInterface"/> from this value.
	/// </summary>
	public ref readonly JNativeInterface Reference => ref this._functions.Reference;
	/// <inheritdoc/>
	public IntPtr Pointer => this._functions;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JEnvironmentValue() => this._functions = ReadOnlyValPtr<JNativeInterface>.Zero;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Int32 GetHashCode() => this._functions.GetHashCode();
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Boolean Equals([NotNullWhen(true)] Object? obj)
		=> obj is JEnvironmentValue jEnvValue && this._functions.Equals(jEnvValue._functions);

	/// <summary>
	/// Retrieves additional function pointers for given <paramref name="jniVersion"/>.
	/// </summary>
	/// <param name="jniVersion">Current JNI version.</param>
	/// <returns>A read-only span of pointers.</returns>
	public ReadOnlySpan<IntPtr> GetAdditionalPointers(Int32 jniVersion)
	{
		Int32 size = jniVersion switch
		{
			>= 0x00090000 and < 0x00130000 => 1,
			>= 0x00130000 => 2,
			_ => 0,
		};
		if (size == 0)
			return ReadOnlySpan<IntPtr>.Empty;
		IntPtr ptr = this._functions + 1;
		return ptr.GetUnsafeReadOnlySpan<IntPtr>(size);
	}
}