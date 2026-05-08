namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// Represents a JNI environment for a native thread.
/// </summary>
internal interface INativeThread : IEnvironment, IAccessibleManager, ILocalCacheOwner
{
	/// <summary>
	/// Indicates whether the associated thread is attached to a JVM.
	/// </summary>
	Boolean IsAttached { get; }

	/// <summary>
	/// Unsafe Memory manager.
	/// </summary>
	IUnsafeMemoryManager MemoryManager { get; }
}

/// <summary>
/// Represents a JNI environment for a native thread.
/// </summary>
/// <typeparam name="TThread">A CLR <see cref="INativeThread{TThread}"/> type.</typeparam>
internal interface INativeThread<TThread> : INativeThread where TThread : class, INativeThread<TThread>
{
	/// <summary>
	/// Creates a <typeparamref name="TThread"/> instance using <paramref name="host"/> and <paramref name="envRef"/>.
	/// </summary>
	/// <param name="host">A <see cref="IVirtualMachineHost"/> instance.</param>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	/// <returns>A new <typeparamref name="TThread"/> instance.</returns>
	protected internal static abstract TThread Create(IVirtualMachineHost host, JEnvironmentRef envRef);
	/// <summary>
	/// Creates a <typeparamref name="TThread"/> instance using <paramref name="host"/>, <paramref name="envRef"/> and
	/// <paramref name="args"/>.
	/// </summary>
	/// <param name="host">A <see cref="IVirtualMachineHost"/> instance.</param>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	/// <param name="args">A <see cref="ThreadCreationArgs"/> instance.</param>
	/// <returns>A new <typeparamref name="TThread"/> instance.</returns>
	protected internal static abstract TThread Create(IVirtualMachineHost host, JEnvironmentRef envRef,
		ThreadCreationArgs args);
	/// <summary>
	/// Creates a <see cref="IThread"/> instance using <paramref name="nativeThread"/>.
	/// </summary>
	/// <param name="nativeThread">A <see cref="INativeThread"/> instance.</param>
	/// <returns>A new <see cref="IThread"/> instance.</returns>
	protected internal static abstract IThread Create(TThread nativeThread);
}