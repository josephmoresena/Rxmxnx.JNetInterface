namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Exposes invocation function set.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal unsafe interface IInvocationFunctionSet
{
	/// <inheritdoc cref="IVirtualMachineLibraryType.GetDefaultVirtualMachineInitArgs(ref VirtualMachineInitArgumentValue)"/>
	JResult GetDefaultVirtualMachineInitArgs(ref VirtualMachineInitArgumentValue initArg);
	/// <inheritdoc
	///     cref="IVirtualMachineLibraryType.CreateVirtualMachine(out JVirtualMachineRef, out JEnvironmentRef, in VirtualMachineInitArgumentValue)"/>
	JResult CreateVirtualMachine(out JVirtualMachineRef vmRef, out JEnvironmentRef envRef,
		in VirtualMachineInitArgumentValue initArg);
	/// <inheritdoc cref="IVirtualMachineLibraryType.GetCreatedVirtualMachines(ValPtr{JVirtualMachineRef}, Int32, out Int32)"/>
	JResult GetCreatedVirtualMachines(JVirtualMachineRef* arr, Int32 arrSize, out Int32 count);
}