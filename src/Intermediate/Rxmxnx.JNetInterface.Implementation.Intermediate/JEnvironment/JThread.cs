namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	/// <summary>
	/// This class implements <see cref="IThread"/> interface.
	/// </summary>
	internal sealed class JThread : JEnvironment, IThread
	{
		/// <summary>
		/// <see cref="ThreadValue"/> instance.
		/// </summary>
		private readonly ThreadValue _value;

		/// <inheritdoc/>
		public override Boolean IsDaemon => this._value.IsDaemon;
		/// <inheritdoc/>
		public override Boolean IsDisposable => this._value.IsDisposable;
		/// <inheritdoc/>
		public override Boolean IsAttached => this._value.IsAttached(this._m.Core);

		/// <inheritdoc/>
		public JThread(IVirtualMachineHost host, JEnvironmentRef envRef, ThreadCreationArgs args) : base(host, envRef)
			=> this._value = new(args);
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="env">Original env instance.</param>
		/// <param name="newThread">Indicates whether the created thread is new.</param>
		public JThread(JEnvironment env, Boolean newThread) : base(env._m.Core)
			=> this._value = new((env as JThread)?._value, newThread);
		/// <inheritdoc cref="IThread.Name"/>
		public override CString Name => this._value.Name;

		Boolean IThread.Attached => this.IsAttached;
		Boolean IThread.Daemon => this.IsDaemon;

		/// <inheritdoc/>
		public void Dispose() => this._value.FinalizeThread(this._m.Core, this, true);
	}
}