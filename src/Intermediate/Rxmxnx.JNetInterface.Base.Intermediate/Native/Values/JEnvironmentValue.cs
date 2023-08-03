namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// <c>JNIEnv</c> struct. Contains a pointer to a <c>JNINativeInterface_</c> object.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly partial struct JEnvironmentValue : INativeReferenceType<JEnvironmentValue, JNativeInterface>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JNativeInterface;

	/// <summary>
	/// Internal <see cref="JNativeInterface"/> pointer.
	/// </summary>
	private readonly IntPtr _functions;

	/// <summary>
	/// <see langword="readonly ref"/> <see cref="JNativeInterface"/> from this value.
	/// </summary>
	public ref readonly JNativeInterface Reference
		=> ref this._functions.GetUnsafeReadOnlyReference<JNativeInterface>();
	/// <inheritdoc/>
	public IntPtr Pointer => this._functions;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JEnvironmentValue() => this._functions = IntPtr.Zero;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Int32 GetHashCode() => HashCode.Combine(this._functions);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Boolean Equals([NotNullWhen(true)] Object? obj)
		=> obj is JEnvironmentValue jEnvValue && this._functions.Equals(jEnvValue._functions);
}