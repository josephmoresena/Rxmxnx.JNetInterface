namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// <c>JNIEnv</c> pointer. Represents a pointer to a <c>JNIEnv</c> object.
/// </summary>
/// <remarks>This reference is valid only for the thread who owns the reference.</remarks>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
[StructLayout(LayoutKind.Explicit)]
public readonly unsafe partial struct JEnvironmentRef : INativePointerType
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JEnvironmentRef;

	/// <summary>
	/// Internal pointer value.
	/// </summary>
	[FieldOffset(0)]
	private readonly void** _value;

	/// <inheritdoc/>
	public IntPtr Pointer => (IntPtr)this._value;

	/// <summary>
	/// Pointer to native interface.
	/// </summary>
	internal void* InterfacePointer => *this._value;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JEnvironmentRef() => this._value = (void**)IntPtr.Zero;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Int32 GetHashCode() => this.Pointer.GetHashCode();
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Boolean Equals([NotNullWhen(true)] Object? obj)
		=> obj is JEnvironmentRef jEnvRef && this.Pointer.Equals(jEnvRef.Pointer);
}