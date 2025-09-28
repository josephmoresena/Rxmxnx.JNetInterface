namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// <c>JNIEnv</c> pointer. Represents a pointer to a <c>JNIEnv</c> object.
/// </summary>
/// <remarks>This references is valid only for the thread who owns the reference.</remarks>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JEnvironmentRef : INativePointerType
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JEnvironmentRef;

	/// <summary>
	/// Internal pointer value.
	/// </summary>
	private readonly ReadOnlyValPtr<IntPtr> _value;

	/// <inheritdoc/>
	public IntPtr Pointer => this._value.Pointer;

	/// <summary>
	/// Pointer to native interface.
	/// </summary>
	internal unsafe void* InterfacePointer => this._value.Reference.ToPointer();

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JEnvironmentRef() => this._value = ReadOnlyValPtr<IntPtr>.Zero;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Int32 GetHashCode() => this._value.GetHashCode();
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Boolean Equals([NotNullWhen(true)] Object? obj)
		=> obj is JEnvironmentRef jEnvRef && this._value.Equals(jEnvRef._value);
}