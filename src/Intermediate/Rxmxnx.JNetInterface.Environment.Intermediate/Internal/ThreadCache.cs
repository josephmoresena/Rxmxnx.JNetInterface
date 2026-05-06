namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// <see cref="INativeThread"/> cache implementation.
/// </summary>
internal sealed class ThreadCache<TThread> : ReferenceHelperCache<TThread, JEnvironmentRef, ThreadCreationArgs?>
	where TThread : class, INativeThread<TThread>
{
	/// <summary>
	/// A <see cref="IVirtualMachineHost"/> instance.
	/// </summary>
	private readonly IVirtualMachineHost _host;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="host">A <see cref="IVirtualMachineHost"/> instance.</param>
	public ThreadCache(IVirtualMachineHost host) => this._host = host;

	/// <inheritdoc/>
	public override TThread Get(JEnvironmentRef reference, out Boolean isNew, ThreadCreationArgs? arg = default)
	{
		TThread result = base.Get(reference, out isNew, arg);
		JTrace.EnvironmentLoad(reference, isNew);
		return result;
	}

	/// <inheritdoc/>
	protected override TThread Create(JEnvironmentRef reference, ThreadCreationArgs? args)
		=> !args.HasValue ? TThread.Create(this._host, reference) : TThread.Create(this._host, reference, args.Value);
	/// <inheritdoc/>
	protected override void Destroy(TThread instance)
	{
		if (instance is IDisposable disposable)
			disposable.Dispose();
	}
}