namespace Rxmxnx.JNetInterface.Native;

public partial class JGlobalBase
{
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
	/// Indicate whether the current instance is disposed.
	/// </summary>
	private Boolean _isDisposed;

	/// <summary>
	/// Indicates whether JNI execution is secure.
	/// </summary>
	/// <returns>
	/// <see langword="true"/> if is secure execute JNI calls; otherwise, <see langword="false"/>.
	/// </returns>
	private Boolean JniSecure() => this.VirtualMachine.GetEnvironment() is { } env && env.JniSecure();
	/// <summary>
	/// Performs application-defined tasks associated with freeing, releasing, or resetting
	/// unmanaged resources.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	private void Unload(IEnvironment env) => this.Dispose(true, env);
}