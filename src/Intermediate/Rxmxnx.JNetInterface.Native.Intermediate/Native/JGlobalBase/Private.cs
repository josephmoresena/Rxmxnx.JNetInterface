namespace Rxmxnx.JNetInterface.Native;

public partial class JGlobalBase
{
	/// <summary>
	/// Assignation cache.
	/// </summary>
	private readonly AssignableTypeCache _assignableTypes = new();
	/// <summary>
	/// This collection stores the weak references to the <see cref="JLocalObject"/> associated with
	/// this instance.
	/// </summary>
	private readonly ConcurrentDictionary<Int64, WeakReference<ObjectLifetime>> _objects = new();
	/// <summary>
	/// Internal <see cref="IntPtr"/> instance.
	/// </summary>
	private readonly IMutableReference<IntPtr> _value;
	/// <summary>
	/// <see cref="IVirtualMachine"/> instance.
	/// </summary>
	private readonly IVirtualMachine _vm;

	/// <summary>
	/// Indicate whether the current instance is disposed.
	/// </summary>
	private Boolean _isDisposed;
	/// <summary>
	/// Object metadata.
	/// </summary>
	private JObjectMetadata _objectMetadata;
}