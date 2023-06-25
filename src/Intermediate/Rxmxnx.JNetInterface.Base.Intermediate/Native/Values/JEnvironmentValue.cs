namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// <c>JNIEnv</c> struct. Contains a pointer to a <c>JNINativeInterface_</c> object.
/// </summary>
public readonly struct JEnvironmentValue : IFixedPointer, INative<JEnvironmentValue>,
	IReadOnlyReferenceable<JNativeInterface>
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
	public ref readonly JNativeInterface Reference =>
		ref this._functions.GetUnsafeReadOnlyReference<JNativeInterface>();
	/// <inheritdoc/>
	public IntPtr Pointer => this._functions;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	public JEnvironmentValue()
	{
		this._functions = IntPtr.Zero;
	}

	/// <inheritdoc/>
	public override Int32 GetHashCode()
	{
		return HashCode.Combine(this._functions);
	}
	/// <inheritdoc/>
	public override Boolean Equals([NotNullWhen(true)] Object? obj)
	{
		return obj is JEnvironmentValue jEnvValue && this._functions.Equals(jEnvValue._functions);
	}
}