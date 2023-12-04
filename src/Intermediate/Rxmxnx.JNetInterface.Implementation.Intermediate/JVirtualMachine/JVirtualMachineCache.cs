namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// This record stores cache for a <see cref="JVirtualMachine"/> instance.
	/// </summary>
	private sealed record JVirtualMachineCache
	{
		/// <summary>
		/// Delegates dictionary.
		/// </summary>
		private static readonly Dictionary<Type, Func<JVirtualMachineRef, IntPtr>> getPointer = new()
		{
			{ typeof(DestroyVirtualMachineDelegate), r => r.Reference.Reference.DestroyJavaVmPointer },
			{ typeof(AttachCurrentThreadDelegate), r => r.Reference.Reference.AttachCurrentThreadPointer },
			{ typeof(DetachCurrentThreadDelegate), r => r.Reference.Reference.DetachCurrentThreadPointer },
			{ typeof(GetEnvDelegate), r => r.Reference.Reference.GetEnvPointer },
			{
				typeof(AttachCurrentThreadAsDaemonDelegate),
				r => r.Reference.Reference.AttachCurrentThreadAsDaemonPointer
			},
		};

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

		/// <summary>
		/// Retrieves a <typeparamref name="TDelegate"/> instance for <typeparamref name="TDelegate"/>.
		/// </summary>
		/// <typeparam name="TDelegate">Type of method delegate.</typeparam>
		/// <returns>A <typeparamref name="TDelegate"/> instance.</returns>
		public TDelegate GetDelegate<TDelegate>() where TDelegate : Delegate
		{
			Type typeOfT = typeof(TDelegate);
			IntPtr ptr = JVirtualMachineCache.getPointer[typeOfT](this.Reference);
			return this.DelegateCache.GetDelegate<TDelegate>(ptr);
		}
	}
}