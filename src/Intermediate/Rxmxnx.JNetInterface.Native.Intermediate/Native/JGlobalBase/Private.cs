namespace Rxmxnx.JNetInterface.Native;

public partial class JGlobalBase
{
	/// <summary>
	/// This collection stores the weak references to the <see cref="JLocalObject"/> associated with
	/// this instance.
	/// </summary>
	private readonly ConcurrentBag<WeakReference<JLocalObject>> _objects = new();
	/// <summary>
	/// <see cref="IVirtualMachine"/> instance.
	/// </summary>
	private readonly IVirtualMachine _vm;

	/// <summary>
	/// Indicate whether the current instance is disposed.
	/// </summary>
	private Boolean _isDisposed;
}