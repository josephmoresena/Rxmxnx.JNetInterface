namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// IEnvironment cache implementation.
	/// </summary>
	private sealed record ThreadCache : ReferenceHelperCache<JEnvironment, JEnvironmentRef, ThreadCreationArgs>
	{
		/// <inheritdoc cref="JVirtualMachine.Reference"/>
		private readonly JVirtualMachineRef _envRef;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="envRef">A <see cref="JVirtualMachineRef"/> reference.</param>
		public ThreadCache(JVirtualMachineRef envRef) => this._envRef = envRef;

		/// <inheritdoc/>
		protected override JEnvironment Create(JEnvironmentRef reference, ThreadCreationArgs? args)
			=> args is null ?
				new JEnvironment(JVirtualMachine.GetVirtualMachine(this._envRef), reference) :
				new JEnvironment.JThread(JVirtualMachine.GetVirtualMachine(this._envRef), reference, args);
		/// <inheritdoc/>
		protected override void Destroy(JEnvironment instance)
		{
			if (instance is IDisposable disposable)
				disposable.Dispose();
		}
	}
}