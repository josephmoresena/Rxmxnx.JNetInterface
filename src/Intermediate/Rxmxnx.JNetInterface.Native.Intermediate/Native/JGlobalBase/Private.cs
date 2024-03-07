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
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="metadata"><see cref="Native.ObjectMetadata"/> instance.</param>
	/// <param name="isProxy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="value">Internal value.</param>
	private JGlobalBase(IVirtualMachine vm, ObjectMetadata metadata, Boolean isProxy, IntPtr value) : base(isProxy)
	{
		this.VirtualMachine = vm;
		this._value = IMutableReference.Create(value);
		this.ObjectMetadata = metadata;
	}

	/// <summary>
	/// Indicates whether JNI execution is secure.
	/// </summary>
	/// <returns>
	/// <see langword="true"/> if is secure execute JNI calls; otherwise, <see langword="false"/>.
	/// </returns>
	private Boolean JniSecure() => this.VirtualMachine.GetEnvironment() is { } env && env.JniSecure();
	/// <inheritdoc cref="IDisposable.Dispose()"/>
	/// <param name="disposing">
	/// Indicates whether this method was called from the <see cref="IDisposable.Dispose"/> method.
	/// </param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	private void Dispose(Boolean disposing, IEnvironment env)
	{
		if (this._isDisposed) return;

		if (disposing && !this.IsDisposable)
		{
			ImmutableArray<Int64> keys = this._objects.Keys.ToImmutableArray();
			foreach (Int64 key in keys)
			{
				if (this._objects.TryRemove(key, out WeakReference<ObjectLifetime>? wObj) &&
				    wObj.TryGetTarget(out ObjectLifetime? objectLifetime))
					objectLifetime.UnloadGlobal(this);
			}
		}

		if (!env.ReferenceFeature.Unload(this)) return;
		this.ClearValue();
		this._isDisposed = true;
	}
}