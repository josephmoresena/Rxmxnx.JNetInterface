namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// <c>JavaVM</c> struct. Contains a pointer to a <c>JNIInvokeInterface_</c> object.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly partial struct JVirtualMachineValue : INativeReference<JVirtualMachineValue, JInvokeInterface>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JVirtualMachine;

	/// <summary>
	/// Internal <see cref="JInvokeInterface"/> pointer.
	/// </summary>
	private readonly IntPtr _functions;

	/// <summary>
	/// <see langword="readonly ref"/> <see cref="JInvokeInterface"/> from this value.
	/// </summary>
	public ref readonly JInvokeInterface Reference
		=> ref this._functions.GetUnsafeReadOnlyReference<JInvokeInterface>();
	/// <inheritdoc/>
	public IntPtr Pointer => this._functions;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JVirtualMachineValue() => this._functions = IntPtr.Zero;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Int32 GetHashCode() => HashCode.Combine(this._functions);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Boolean Equals([NotNullWhen(true)] Object? obj)
		=> obj is JVirtualMachineValue jVirtualMValue && this._functions.Equals(jVirtualMValue._functions);
}