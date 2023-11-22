namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// IEnvironment cache implementation.
	/// </summary>
	private sealed record ThreadCache : ReferenceHelperCache<JEnvironment, JEnvironmentRef>
	{
		/// <inheritdoc cref="JVirtualMachine.Reference"/>
		private readonly JVirtualMachineRef _envRef;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="envRef">A <see cref="JVirtualMachineRef"/> reference.</param>
		public ThreadCache(JVirtualMachineRef envRef)
		{
			this._envRef = envRef;
		}

		/// <inheritdoc />
		protected override JEnvironment Create(JEnvironmentRef reference, Boolean isDestroyable)
			=> !isDestroyable ? new(JVirtualMachine.GetVirtualMachine(this._envRef), reference) : default!;
		/// <inheritdoc />
		protected override void Destroy(JEnvironment instance)
		{
			if (instance is IDisposable disposable)
				disposable.Dispose();
		}
	}
}