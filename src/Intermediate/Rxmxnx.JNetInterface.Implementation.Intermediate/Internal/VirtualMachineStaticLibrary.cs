namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// <c>jvm</c> static library linked.
/// </summary>
#if !PACKAGE
[ExcludeFromCodeCoverage]
#endif
internal abstract class VirtualMachineStaticLibrary : IVirtualMachineLibraryType
{
	static Boolean IVirtualMachineLibraryType.IsStatic => true;
	static Boolean IVirtualMachineLibraryType.HasCreatedVmMethod => true;

#pragma warning disable SYSLIB1054
	/// <inheritdoc/>
	[DllImport("*", EntryPoint = IVirtualMachineLibraryType.GetDefaultVirtualMachineInitArgsSymbol)]
	public static extern JResult GetDefaultVirtualMachineInitArgs(ref VirtualMachineInitArgumentValue initArg);
	/// <inheritdoc/>
	[DllImport("*", EntryPoint = IVirtualMachineLibraryType.CreateVirtualMachineSymbol)]
	public static extern JResult CreateVirtualMachine(out JVirtualMachineRef vmRef, out JEnvironmentRef envRef,
		in VirtualMachineInitArgumentValue initArg);
	/// <inheritdoc/>
	[DllImport("*", EntryPoint = IVirtualMachineLibraryType.GetCreatedVirtualMachinesSymbol)]
	public static extern JResult GetCreatedVirtualMachines(ValPtr<JVirtualMachineRef> arr, Int32 arrSize,
		out Int32 count);
#pragma warning restore SYSLIB1054
}