namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// Represents a virtual machine host.
/// </summary>
internal interface IVirtualMachineHost : IWrapper<IVirtualMachine>
{
	/// <summary>
	/// Indicates whether the current host is running a JVM.
	/// </summary>
	Boolean IsRunning { get; }
	/// <summary>
	/// Global Object manager.
	/// </summary>
	IGlobalObjectManager GlobalManager { get; }
	/// <summary>
	/// Native Memory manager.
	/// </summary>
	INativeMemoryManager MemoryManager { get; }
	/// <summary>
	/// Java Type manager.
	/// </summary>
	ITypeManager TypeManager { get; }
	/// <summary>
	/// Global main classes.
	/// </summary>
	GlobalMainClasses MainClasses { get; }
}