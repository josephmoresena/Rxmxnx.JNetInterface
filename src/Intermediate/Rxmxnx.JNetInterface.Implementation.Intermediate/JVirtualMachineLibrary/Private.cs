namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class stores a loaded native JVM library.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
public abstract unsafe partial class JVirtualMachineLibrary
{
	/// <inheritdoc cref="IInvocationFunctionSet.GetDefaultVirtualMachineInitArgs(ref VirtualMachineInitArgumentValue)"/>
	private protected abstract JResult GetDefaultVirtualMachineInitArgs(ref VirtualMachineInitArgumentValue initArg);
	/// <inheritdoc
	///     cref="IInvocationFunctionSet.CreateVirtualMachine(out JVirtualMachineRef, out JEnvironmentRef, in VirtualMachineInitArgumentValue)"/>
	private protected abstract JResult CreateVirtualMachine(out JVirtualMachineRef vmRef, out JEnvironmentRef envRef,
		in VirtualMachineInitArgumentValue initArg);
	/// <inheritdoc cref="IInvocationFunctionSet.GetCreatedVirtualMachines(JVirtualMachineRef* Int32, out Int32)"/>
	private protected abstract JResult GetCreatedVirtualMachines(JVirtualMachineRef* arr, Int32 arrSize,
		out Int32 count);
}