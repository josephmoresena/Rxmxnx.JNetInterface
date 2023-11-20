namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	/// <summary>
	/// This record stores cache for a <see cref="JEnvironment"/> instance.
	/// </summary>
	private sealed partial record JEnvironmentCache
	{
		/// <inheritdoc cref="JEnvironment.VirtualMachine"/>
		public IVirtualMachine VirtualMachine { get; }
		/// <inheritdoc cref="JEnvironment.Reference"/>
		public JEnvironmentRef Reference { get; }
		/// <summary>
		/// Delegate cache.
		/// </summary>
		public DelegateHelperCache DelegateCache { get; private init; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vm">A <see cref="IVirtualMachine"/> instance.</param>
		/// <param name="reference">A <see cref="JEnvironmentRef"/> instance.</param>
		public JEnvironmentCache(IVirtualMachine vm, JEnvironmentRef reference)
		{
			this.VirtualMachine = vm;
			this.Reference = reference;
			this.DelegateCache = new();
		}
	}
}