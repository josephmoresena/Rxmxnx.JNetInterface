namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// This record stores cache for a <see cref="JVirtualMachine"/> instance.
	/// </summary>
	private sealed record JVirtualMachineCache
	{
		/// <inheritdoc cref="JVirtualMachine.Reference"/>
		public JVirtualMachineRef Reference { get; }
		/// <summary>
		/// Delegate cache.
		/// </summary>
		public DelegateHelperCache DelegateCache { get; }
		/// <summary>
		/// Thread cache.
		/// </summary>
		public ThreadCache ThreadCache { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vmRef">A <see cref="JVirtualMachineRef"/> reference.</param>
		public JVirtualMachineCache(JVirtualMachineRef vmRef)
		{
			this.Reference = vmRef;
			this.DelegateCache = new();
			this.ThreadCache = new(vmRef);
		}
	}
}