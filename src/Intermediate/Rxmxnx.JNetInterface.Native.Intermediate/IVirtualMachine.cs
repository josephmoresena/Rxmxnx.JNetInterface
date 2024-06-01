namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes the invocation interface of a Java Virtual Machine.
/// </summary>
public interface IVirtualMachine : IWrapper<JVirtualMachineRef>
{
	/// <summary>
	/// Minimum virtual machine version required for any JNI thread.
	/// </summary>
	public const Int32 MinimalVersion = 0x00010006;

	/// <summary>
	/// Capacity for Throwable.getStackTrace()
	/// </summary>
	internal const Int32 GetStackTraceCapacity = 10;
	/// <summary>
	/// Capacity Class.getModifiers(), Class.getName() and FindClass()
	/// </summary>
	internal const Int32 IsFinalArrayCapacity = 5;
	/// <summary>
	/// Capacity Class.getModifiers(), Class.getName() and FindClass()
	/// </summary>
	internal const Int32 GetAccessibleDefinitionCapacity = 5;
	/// <summary>
	/// Capacity Class.getModifiers(), Class.getSuperclass() and Class.getName()
	/// </summary>
	internal const Int32 GetTypeMetadataCapacity = 8;
	/// <summary>
	/// Capacity Throwable.message()
	/// </summary>
	internal const Int32 CreateThrowableExceptionCapacity = 5;
	/// <summary>
	/// Capacity GetObjectArrayElement()
	/// </summary>
	internal const Int32 IndexOfObjectCapacity = 5;
	/// <summary>
	/// Capacity Class.getModifiers(), Class.getInterfaces() and Class.getName()
	/// </summary>
	internal const Int32 GetSuperInterfaceMetadataCapacity = 10;
	/// <summary>
	/// Capacity GetObjectClass()
	/// </summary>
	internal const Int32 GetObjectClassCapacity = 10;

	/// <summary>
	/// Indicates whether trace output is enabled.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static Boolean TraceEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => AppContext.TryGetSwitch("JNetInterface.EnableTrace", out Boolean enable) && enable;
	}
	/// <summary>
	/// Indicates whether metadata validation is enabled.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static Boolean MetadataValidationEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !AppContext.TryGetSwitch("JNetInterface.DisableMetadataValidation", out Boolean disable) || !disable;
	}

	/// <summary>
	/// JNI reference to the interface.
	/// </summary>
	JVirtualMachineRef Reference { get; }
	/// <summary>
	/// Indicates whether the current instance is not a proxy.
	/// </summary>
	internal Boolean NoProxy { get; }

	JVirtualMachineRef IWrapper<JVirtualMachineRef>.Value => this.Reference;

	/// <summary>
	/// Retrieves the <see cref="IEnvironment"/> instance for the current thread.
	/// </summary>
	/// <returns>The <see cref="IEnvironment"/> instance for the current thread.</returns>
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

	/// <summary>
	/// Attaches the current thread to the virtual machine for <paramref name="purpose"/>.
	/// </summary>
	/// <param name="purpose">The purpose of requested thread.</param>
	/// <returns>A <see cref="IThread"/> instance for given purpose.</returns>
	internal IThread CreateThread(ThreadPurpose purpose)
	{
		ThreadCreationArgs args = ThreadCreationArgs.Create(purpose);
		return this.InitializeThread(args.Name, args.ThreadGroup);
	}
}