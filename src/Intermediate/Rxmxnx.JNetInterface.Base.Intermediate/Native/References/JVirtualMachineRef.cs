namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// <c>JavaVM</c> pointer. Represents a pointer to a <c>JavaVM</c> object.
/// </summary>
/// <remarks>
/// This identifier will be valid until the library is unloaded or the JVM instance is destroyed.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JVirtualMachineRef : INativeReferenceType<JVirtualMachineRef, JVirtualMachineValue>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JVirtualMachineRef;

	/// <summary>
	/// Internal pointer value.
	/// </summary>
	private readonly ReadOnlyValPtr<JVirtualMachineValue> _value;

	/// <inheritdoc/>
	public IntPtr Pointer => this._value;

	/// <summary>
	/// <see langword="readonly ref"/> <see cref="JVirtualMachineValue"/> from this pointer.
	/// </summary>
	internal ref readonly JVirtualMachineValue Reference => ref this._value.Reference;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	public JVirtualMachineRef() => this._value = ReadOnlyValPtr<JVirtualMachineValue>.Zero;

	ref readonly JVirtualMachineValue IReadOnlyReferenceable<JVirtualMachineValue>.Reference => ref this.Reference;

	/// <inheritdoc/>
	public override Int32 GetHashCode() => this._value.GetHashCode();
	/// <inheritdoc/>
	public override Boolean Equals([NotNullWhen(true)] Object? obj)
		=> obj is JVirtualMachineRef jVirtualMRef && this._value.Equals(jVirtualMRef._value);
}