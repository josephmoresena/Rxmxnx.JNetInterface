namespace Rxmxnx.JNetInterface.Native;

public partial class JGlobalBase
{
	/// <summary>
	/// Object class name.
	/// </summary>
	private readonly CString _objectClassName;
	/// <summary>
	/// This collection stores the weak references to the <see cref="JLocalObject"/> associated with
	/// this instance.
	/// </summary>
	private readonly ConcurrentDictionary<Int64, WeakReference<JLocalObject>> _objects = new();
	/// <summary>
	/// Object signature.
	/// </summary>
	private readonly CString _objectSignature;
	/// <summary>
	/// <see cref="IVirtualMachine"/> instance.
	/// </summary>
	private readonly IVirtualMachine _vm;

	/// <summary>
	/// Indicate whether the current instance is disposed.
	/// </summary>
	private Boolean _isDisposed;
}