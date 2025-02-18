namespace Rxmxnx.JNetInterface.Internal;

internal partial struct JniTransactionHandle
{
	/// <summary>
	/// Represents a JNI Enter/Exit monitor transaction.
	/// </summary>
	private sealed class Synchronizer : UnaryTransaction
	{
		/// <summary>
		/// A <see cref="IVirtualMachine"/> instance.
		/// </summary>
		private readonly IVirtualMachine _vm;

		/// <summary>
		/// Indicates whether current monitor is active.
		/// </summary>
		private Boolean _active;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vm">A <see cref="IVirtualMachine"/> instance.</param>
		/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
		public Synchronizer(IVirtualMachine vm, JReferenceObject jObject)
		{
			this._vm = vm;
			(this as INativeTransaction).Add(jObject);
		}

		/// <summary>
		/// Activates the instance monitor.
		/// </summary>
		public void Activate(IEnvironment env)
		{
			if (this._active) return;
			env.ReferenceFeature.MonitorEnter(this.LocalRef);
			this._active = true;
		}

		/// <inheritdoc/>
		protected override void Dispose(Boolean disposing)
		{
			if (disposing && !this.Disposed && this._active)
			{
				using IThread thread = this._vm.CreateThread(ThreadPurpose.SynchronizeGlobalReference);
				thread.ReferenceFeature.MonitorExit(this.LocalRef);
				this._active = false;
			}
			base.Dispose(disposing);
		}
	}
}