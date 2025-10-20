namespace Rxmxnx.JNetInterface;

public partial interface IVirtualMachine
{
	/// <summary>
	/// Capacity for thread initialization.
	/// </summary>
	internal const Int32 MinimalCapacity = 16;
	/// <summary>
	/// Capacity for Throwable.getStackTrace()
	/// </summary>
	internal const Int32 GetStackTraceCapacity = 5;
	/// <summary>
	/// Capacity Class.getModifiers(), Class.getName() and FindClass()
	/// </summary>
	internal const Int32 IsFinalArrayCapacity = 5;
	/// <summary>
	/// Capacity Class.getModifiers(), Class.getName() and FindClass()
	/// </summary>
	internal const Int32 GetAccessibleDefinitionCapacity = 3;
	/// <summary>
	/// Capacity Class.getModifiers(), Class.getSuperclass() and Class.getName()
	/// </summary>
	internal const Int32 GetSuperTypeCapacity = 5;
	/// <summary>
	/// Capacity Throwable.message()
	/// </summary>
	internal const Int32 CreateThrowableExceptionCapacity = 5;
	/// <summary>
	/// Capacity GetObjectArrayElement()
	/// </summary>
	internal const Int32 IndexOfObjectCapacity = 5;
	/// <summary>
	/// Capacity GetObjectClass()
	/// </summary>
	internal const Int32 GetObjectClassCapacity = 5;

	/// <summary>
	/// Indicates whether the current instance is not a proxy.
	/// </summary>
	internal Boolean NoProxy { get; }

	/// <summary>
	/// Attaches the current thread to the virtual machine for <paramref name="purpose"/>.
	/// </summary>
	/// <param name="purpose">The purpose of requested thread.</param>
	/// <returns>A <see cref="IThread"/> instance for given purpose.</returns>
	internal IThread CreateThread(ThreadPurpose purpose)
	{
		ThreadCreationArgs args = ThreadCreationArgs.Create(purpose);
		return this.InitializeThread(args.Name, args.ThreadGroup, (Int32)JRuntimeVersion.SEd2);
	}
}