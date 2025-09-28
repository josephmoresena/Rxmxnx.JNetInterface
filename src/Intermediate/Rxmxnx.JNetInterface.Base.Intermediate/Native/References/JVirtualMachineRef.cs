namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// <c>JavaVM</c> pointer. Represents a pointer to a <c>JavaVM</c> object.
/// </summary>
/// <remarks>
/// This identifier will be valid until the library is unloaded or the JVM instance is destroyed.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JVirtualMachineRef : INativePointerType
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JVirtualMachineRef;

	/// <summary>
	/// Internal pointer value.
	/// </summary>
	private readonly ReadOnlyValPtr<IntPtr> _value;

	/// <inheritdoc/>
	public IntPtr Pointer => this._value;

	/// <summary>
	/// Pointer to native interface.
	/// </summary>
	internal unsafe void* InterfacePointer => this._value.Reference.ToPointer();

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	public JVirtualMachineRef() => this._value = ReadOnlyValPtr<IntPtr>.Zero;

	/// <inheritdoc/>
	public override Int32 GetHashCode() => this._value.GetHashCode();
	/// <inheritdoc/>
	public override Boolean Equals([NotNullWhen(true)] Object? obj)
		=> obj is JVirtualMachineRef jVirtualMRef && this._value.Equals(jVirtualMRef._value);
}