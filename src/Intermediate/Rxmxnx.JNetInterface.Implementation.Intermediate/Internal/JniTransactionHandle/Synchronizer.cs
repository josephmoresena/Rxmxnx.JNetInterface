namespace Rxmxnx.JNetInterface.Internal;

internal partial struct JniTransactionHandle
{
	/// <summary>
	/// Represents a JNI Enter/Exit monitor transaction.
	/// </summary>
	private sealed class Synchronizer : UnaryTransaction
	{
		/// <summary>
		/// A <see cref="IEnvironment"/> instance.
		/// </summary>
		private readonly IEnvironment _env;
		/// <summary>
		/// Synchronized instance.
		/// </summary>
		private readonly JReferenceObject _jObject;

		/// <summary>
		/// Indicates whether current monitor is active.
		/// </summary>
		private Boolean _active;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
		/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
		public Synchronizer(IEnvironment env, JReferenceObject jObject)
		{
			this._env = env;
			(this as INativeTransaction).Add(jObject);
			this._jObject = jObject;
		}

		/// <summary>
		/// Activates the instance monitor.
		/// </summary>
		public void Activate()
		{
			if (this._active) return;
			this._env.ReferenceFeature.MonitorEnter(this.LocalRef);
			this._active = false;
		}

		protected override void Dispose(Boolean disposing)
		{
			if (disposing && !this.Disposed)
				try
				{
					if (this._active)
					{
						this._env.ReferenceFeature.MonitorExit(this.LocalRef);
						this._active = false;
					}
				}
				finally
				{
					if (this._jObject is JGlobalBase && this._env is IDisposable thread)
						thread.Dispose();
				}
			base.Dispose(disposing);
		}
	}
}