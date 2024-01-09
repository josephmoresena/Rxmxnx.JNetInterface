namespace Rxmxnx.JNetInterface.Internal;

internal partial struct JniTransactionHandle
{
	/// <summary>
	/// Represents a JNI Enter/Exit monitor transaction.
	/// </summary>
	private sealed record Synchronizer : UnaryTransaction
	{
		/// <summary>
		/// A <see cref="JEnvironment"/> instance.
		/// </summary>
		private readonly JEnvironment _env;
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
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		/// <param name="jObject">A <see cref="JLocalObject"/> instance.</param>
		public Synchronizer(JEnvironment env, JReferenceObject jObject)
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
			this._env.MonitorEnter(this.LocalRef);
			this._active = false;
		}

		/// <inheritdoc/>
		public override void Dispose()
		{
			if (!this.Disposed)
				try
				{
					if (this._active)
					{
						this._env.MonitorExit(this.LocalRef);
						this._active = false;
					}
				}
				finally
				{
					if (this._jObject is JGlobalBase && this._env is IDisposable thread)
						thread.Dispose();
				}
			base.Dispose();
		}
	}
}