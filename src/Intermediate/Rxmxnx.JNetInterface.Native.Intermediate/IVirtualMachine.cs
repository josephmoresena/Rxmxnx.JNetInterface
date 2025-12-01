namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes the invocation interface of a Java Virtual Machine.
/// </summary>
public partial interface IVirtualMachine : IWrapper<JVirtualMachineRef>
{
	/// <summary>
	/// Java runtime version.
	/// </summary>
	JRuntimeVersion Version { get; }
	/// <summary>
	/// Android API Level.
	/// </summary>
	/// <remarks>
	///     <list type="table">
	///         <listheader>
	///             <term>Value</term>
	///             <description>Description</description>
	///         </listheader>
	///         <item>
	///             <term>-1</term>
	///             <description>The JVM does not support Android classes.</description>
	///         </item>
	///         <item>
	///             <term>0</term>
	///             <description>The JVM is not Android-based, but supports Android classes.</description>
	///         </item>
	///         <item>
	///             <term>&gt; 0</term>
	///             <description>The JVM is running on Android and the value represents the Android API level.</description>
	///         </item>
	///     </list>
	/// </remarks>
	Int32 AndroidApiLevel => 0; // 0: Not Android, but supports Android classes.
	/// <summary>
	/// JNI reference to the interface.
	/// </summary>
	JVirtualMachineRef Reference { get; }

	JVirtualMachineRef IWrapper<JVirtualMachineRef>.Value => this.Reference;

	/// <summary>
	/// Retrieves the <see cref="IEnvironment"/> instance for the current thread.
	/// </summary>
	/// <returns>The <see cref="IEnvironment"/> instance for the current thread.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	IEnvironment? GetEnvironment();
	/// <summary>
	/// Attaches the current thread to the virtual machine.
	/// </summary>
	/// <param name="threadName">A name for the current thread.</param>
	/// <param name="threadGroup">A <see cref="JGlobalBase"/> instance of <c>java.lang.ThreadGroup</c>.</param>
	/// <param name="version">Thread requested version.</param>
	/// <returns>A <see cref="IThread"/> instance.</returns>
	IThread InitializeThread(CString? threadName = default, JGlobalBase? threadGroup = default,
		Int32 version = IVirtualMachine.MinimalVersion);
	/// <summary>
	/// Attaches the current thread as daemon to the virtual machine.
	/// </summary>
	/// <param name="threadName">A name for the current thread.</param>
	/// <param name="threadGroup">A <see cref="JGlobalBase"/> instance of <c>java.lang.ThreadGroup</c>.</param>
	/// <param name="version">Thread requested version.</param>
	/// <returns>A <see cref="IThread"/> instance.</returns>
	IThread InitializeDaemon(CString? threadName = default, JGlobalBase? threadGroup = default,
		Int32 version = IVirtualMachine.MinimalVersion);
	/// <summary>
	/// Raises a fatal error and does not expect the VM to recover.
	/// </summary>
	/// <param name="message">Error message. The string is encoded in modified UTF-8.</param>
	void FatalError(CString? message);
	/// <summary>
	/// Raises a fatal error and does not expect the VM to recover.
	/// </summary>
	/// <param name="message">Error message.</param>
	void FatalError(String? message);
}