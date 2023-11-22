namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	/// <summary>
	/// This record stores cache for a <see cref="JEnvironment"/> instance.
	/// </summary>
	private sealed partial record JEnvironmentCache
	{
		/// <inheritdoc cref="JEnvironment.VirtualMachine"/>
		public IVirtualMachine VirtualMachine { get; private init; }
		/// <inheritdoc cref="JEnvironment.Reference"/>
		public JEnvironmentRef Reference { get; private init; }
		/// <summary>
		/// Thread.
		/// </summary>
		public Thread Thread { get; set; }
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
			this.Thread = Thread.CurrentThread;
			Task.Factory.StartNew(JEnvironmentCache.FinalizeCache, this);
		}

		/// <summary>
		/// Cache finalize method.
		/// </summary>
		/// <param name="obj">A <see cref="JEnvironmentCache"/> instance.</param>
		private static void FinalizeCache(Object? obj)
		{
			if (obj is not JEnvironmentCache cache) return;
			cache.Thread.Join();
			JVirtualMachine.RemoveEnvironment(cache.VirtualMachine.Reference, cache.Reference);
		}
	}
}