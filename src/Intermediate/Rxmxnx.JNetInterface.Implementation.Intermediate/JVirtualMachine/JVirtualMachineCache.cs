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
		public ThreadCache ThreadCache { get; private init; }

		public JVirtualMachineCache(JVirtualMachineRef reference)
		{
			this.Reference = reference;
			this.DelegateCache = new();
			this.ThreadCache = new();
		}
	}
}