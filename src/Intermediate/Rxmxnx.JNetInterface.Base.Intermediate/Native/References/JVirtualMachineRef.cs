namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// <c>JavaVM</c> pointer. Represents a pointer to a <c>JavaVM</c> object.
/// </summary>
/// <remarks>
/// This identifier will be valid until the library is unloaded or the JVM instance is destroyed.
/// </remarks>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
[StructLayout(LayoutKind.Explicit)]
public readonly unsafe partial struct JVirtualMachineRef : INativePointerType
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JVirtualMachineRef;

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
	public JVirtualMachineRef() => this._value = (void**)IntPtr.Zero;

	/// <inheritdoc/>
	public override Int32 GetHashCode() => this.Pointer.GetHashCode();
	/// <inheritdoc/>
	public override Boolean Equals([NotNullWhen(true)] Object? obj)
		=> obj is JVirtualMachineRef jVirtualMRef && this.Pointer.Equals(jVirtualMRef.Pointer);
}