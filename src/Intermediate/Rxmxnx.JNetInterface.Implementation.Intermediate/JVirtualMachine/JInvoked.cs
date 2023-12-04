namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// This class implements <see cref="IInvokedVirtualMachine"/> interface.
	/// </summary>
	private sealed class Invoked : JVirtualMachine, IInvokedVirtualMachine
	{
		/// <summary>
		/// Indicates whether current instance is disposable.
		/// </summary>
		private readonly Boolean _isDisposable;
		/// <summary>
		/// Indicates whether current instance is disposed.
		/// </summary>
		private readonly IMutableWrapper<Boolean> _isDisposed;

		/// <inheritdoc/>
		public override Boolean IsDisposable => this._isDisposable;

		/// <inheritdoc/>
		public Invoked(JVirtualMachineRef vmRef) : base(vmRef)
		{
			this._isDisposed = IMutableReference<Boolean>.Create();
			this._isDisposable = true;
		}
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vm">Original vm instance.</param>
		public Invoked(JVirtualMachine vm) : base(vm._cache)
		{
			Invoked? invoked = vm as Invoked;
			this._isDisposable = invoked is not null;
			this._isDisposed = invoked?._isDisposed ?? IMutableReference<Boolean>.Create();
		}

		/// <inheritdoc/>
		public void Dispose()
		{
			if (!this._isDisposable || this._isDisposed.Value) return;
			DestroyVirtualMachineDelegate destroyVirtualMachine =
				this._cache.GetDelegate<DestroyVirtualMachineDelegate>();
			JResult result =  destroyVirtualMachine(this._cache.Reference);
			if (result != JResult.Ok) throw new JniException(result);
			this._isDisposed.Value = true;
			JVirtualMachine.RemoveVirtualMachine(this._cache.Reference);
		}
	}
}