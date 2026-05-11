namespace Rxmxnx.JNetInterface;

/// <summary>
/// Internal <see cref="IThread"/> value.
/// </summary>
internal readonly struct ThreadValue
{
	/// <summary>
	/// Indicates whether current instance is disposable.
	/// </summary>
	public readonly Boolean IsDisposable;

	/// <summary>
	/// Creation argument.
	/// </summary>
	private readonly ThreadCreationArgs _args;
	/// <summary>
	/// Indicates whether the current instance is disposed.
	/// </summary>
	private readonly IMutableWrapper<Boolean> _isDisposed;

	/// <inheritdoc cref="ThreadCreationArgs.IsDaemon"/>
	public Boolean IsDaemon => this._args.IsDaemon;
	/// <inheritdoc cref="IThread.Name"/>
	public CString Name => this._args.Name ?? CString.Zero;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="args">A <see cref="ThreadCreationArgs"/> value.</param>
	public ThreadValue(ThreadCreationArgs args)
	{
		this._args = args;
		this._isDisposed = IMutableReference<Boolean>.Create();
		this.IsDisposable = true;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">A <see cref="ThreadValue"/> value.</param>
	public ThreadValue(ThreadValue? value)
	{
		this._args = value?._args ?? new();
		this._isDisposed = value?._isDisposed ?? IMutableReference<Boolean>.Create();
		this.IsDisposable = false;
	}

	/// <inheritdoc cref="IThread.Attached"/>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if current thread is attached; otherwise <see langword="false"/>.
	/// </returns>
	public Boolean IsAttached(EnvironmentCore core)
		=> core.Host.IsRunning && (!this.IsDisposable || !this._isDisposed.Value);
	/// <summary>
	/// Removes the current thread into the current host.
	/// </summary>
	/// <param name="detach">Indicates the current thread should be detached from the JVM.</param>
	/// <param name="core">A <see cref="EnvironmentCore"/> reference.</param>
	/// <param name="owner">A <see cref="ILocalCacheOwner"/> instance.</param>
	public void FinalizeThread(EnvironmentCore core, ILocalCacheOwner owner, Boolean detach)
	{
		if (!this.IsDisposable || this._isDisposed.Value) return;

		this._isDisposed.Value = true;
		core.Host.FinalizeThread(core.Reference, owner, detach ? core.Thread : default);
	}
}