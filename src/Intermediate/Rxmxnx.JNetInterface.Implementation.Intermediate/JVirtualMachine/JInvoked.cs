namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// This class implements <see cref="IInvokedVirtualMachine"/> interface.
	/// </summary>
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	private sealed class Invoked : JVirtualMachine, IInvokedVirtualMachine
	{
		/// <summary>
		/// Indicates whether current instance is disposed.
		/// </summary>
		private readonly IMutableWrapper<Boolean> _isDisposed;

		/// <inheritdoc/>
		public override Boolean IsAlive => !this.IsDisposable || !this._isDisposed.Value;
		/// <inheritdoc/>
		public override Boolean IsDisposable { get; }

		/// <inheritdoc/>
		public Invoked(JVirtualMachineRef vmRef) : base(vmRef)
		{
			this._isDisposed = IMutableReference<Boolean>.Create();
			this.IsDisposable = true;
		}
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vm">Original vm instance.</param>
		public Invoked(JVirtualMachine vm) : base(vm._cache)
		{
			Invoked? invoked = vm as Invoked;
			this.IsDisposable = invoked is not null;
			this._isDisposed = invoked?._isDisposed ?? IMutableReference<Boolean>.Create();
		}

		/// <inheritdoc/>
		public unsafe void Dispose()
		{
			if (!this.IsDisposable || this._isDisposed.Value) return;
			this._cache.ClearCache();
			JResult result = this._cache.GetInvokeInterface().DestroyVirtualMachine(this._cache.Reference);
			ImplementationValidationUtilities.ThrowIfInvalidResult(result);
			this._isDisposed.Value = true;
			JVirtualMachine.RemoveVirtualMachine(this._cache.Reference);
		}
	}
}