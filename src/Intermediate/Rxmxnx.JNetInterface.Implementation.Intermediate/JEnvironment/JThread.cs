namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	/// <summary>
	/// This class implements <see cref="IThread"/> interface.
	/// </summary>
	internal sealed class JThread : JEnvironment, IThread
	{
		/// <summary>
		/// Creation argument.
		/// </summary>
		private readonly ThreadCreationArgs _args;
		/// <summary>
		/// Indicates whether the current instance is disposed.
		/// </summary>
		private readonly IMutableWrapper<Boolean> _isDisposed;

		public override Boolean IsAttached => base.IsAttached && (!this.IsDisposable || !this._isDisposed.Value);
		/// <inheritdoc/>
		public override Boolean IsDaemon => this._args.IsDaemon;
		/// <inheritdoc/>
		public override Boolean IsDisposable { get; }

		/// <inheritdoc/>
		public JThread(IVirtualMachine vm, JEnvironmentRef envRef, ThreadCreationArgs args) : base(vm, envRef)
		{
			this._args = args;
			this._isDisposed = IMutableReference<Boolean>.Create();
			this.IsDisposable = true;
		}
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="env">Original env instance.</param>
		public JThread(JEnvironment env) : base(env._cache)
		{
			JThread? thread = env as JThread;
			this.IsDisposable = false;
			this._isDisposed = thread?._isDisposed ?? IMutableReference<Boolean>.Create();
			this._args = thread?._args ?? new();
		}

		/// <inheritdoc cref="IThread.Name"/>
		public override CString Name => this._args.Name ?? CString.Zero;

		Boolean IThread.Attached => this.IsAttached;
		Boolean IThread.Daemon => this.IsDaemon;

		/// <inheritdoc/>
		public void Dispose()
		{
			if (!this.IsDisposable || this._isDisposed.Value) return;
			this._isDisposed.Value = true;

			JVirtualMachine.RemoveEnvironment(this._cache.VirtualMachine.Reference, this.Reference);
			this._cache.FreeReferences();
			JVirtualMachine.DetachCurrentThread(this._cache.VirtualMachine.Reference, this.Reference,
			                                    this._cache.Thread);
		}
	}
}