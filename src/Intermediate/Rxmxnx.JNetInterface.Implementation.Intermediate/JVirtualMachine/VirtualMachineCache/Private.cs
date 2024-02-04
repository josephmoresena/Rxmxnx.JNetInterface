namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	private partial record VirtualMachineCache
	{
		/// <summary>
		/// Global object dictionary.
		/// </summary>
		private readonly ConcurrentDictionary<JGlobalRef, WeakReference<JGlobal>> _globalObjects = new();
		/// <summary>
		/// JNI transaction dictionary.
		/// </summary>
		private readonly ConcurrentDictionary<Guid, INativeTransaction> _transactions = new();
		/// <summary>
		/// Main <see cref="IVirtualMachine"/> instance.
		/// </summary>
		private readonly IVirtualMachine _vm;
		/// <summary>
		/// Weak global object dictionary.
		/// </summary>
		private readonly ConcurrentDictionary<JWeakRef, WeakReference<JWeak>> _weakObjects = new();
		/// <summary>
		/// Delegate cache.
		/// </summary>
		private readonly DelegateHelperCache _delegateCache;
	}
}