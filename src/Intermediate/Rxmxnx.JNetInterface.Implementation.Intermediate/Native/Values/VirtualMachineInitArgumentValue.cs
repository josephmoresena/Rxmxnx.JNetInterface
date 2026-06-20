namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Unmanaged type for <see cref="JVirtualMachineInitArg"/> value.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
public readonly unsafe struct VirtualMachineInitArgumentValue : INativeType
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JVirtualMachineInitArgument;

	/// <summary>
	/// JNI version.
	/// </summary>
	internal Int32 Version { get; init; }
	/// <summary>
	/// Length of options values.
	/// </summary>
	internal Int32 OptionsLength { get; init; }
	/// <summary>
	/// Pointer to options values.
	/// </summary>
	internal VirtualMachineInitOptionValue* Options { get; init; }
	/// <summary>
	/// Indicates whether initialization ignores any unrecognized option.
	/// </summary>
	internal JBoolean IgnoreUnrecognized { get; init; }
}