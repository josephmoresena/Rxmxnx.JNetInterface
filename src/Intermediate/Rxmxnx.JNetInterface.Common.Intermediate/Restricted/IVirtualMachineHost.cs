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

	/// <summary>
	/// Retrieves the <see cref="JEnvironmentRef"/> reference for current thread.
	/// </summary>
	/// <param name="envRef">Output. Current thread <see cref="JEnvironmentRef"/> reference.</param>
	/// <param name="jniVersion">JNI version.</param>
	/// <returns>JNI code result.</returns>
	JResult GetEnv(out JEnvironmentRef envRef, Int32 jniVersion);
	/// <summary>
	/// Attach current thread to VM.
	/// </summary>
	/// <param name="isDaemon">Indicates current thread will be attached as daemon.</param>
	/// <param name="arg">Attach argument.</param>
	/// <param name="envRef">Output. Attached thread <see cref="JEnvironmentRef"/> reference.</param>
	/// <returns>JNI code result.</returns>
	JResult AttachThread(Boolean isDaemon, VirtualMachineArgumentValue arg, out JEnvironmentRef envRef);
	/// <summary>
	/// Removes the <see cref="IEnvironment"/> instance referenced by <paramref name="envRef"/>
	/// into the current host.
	/// </summary>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	/// <param name="owner">A <see cref="ILocalCacheOwner"/> instance.</param>
	/// <param name="thread">A <see cref="Thread"/> instance.</param>
	void FinalizeThread(JEnvironmentRef envRef, ILocalCacheOwner owner, Thread thread);
}