namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This class helper stores a native delegates cache.
/// </summary>
internal sealed record DelegateHelperCache
{
	/// <summary>
	/// Dictionary of delegates.
	/// </summary>
	private readonly ConcurrentDictionary<Type, Delegate> _delegates = new();

	/// <summary>
	/// Retrieves a <typeparamref name="TDelegate"/> instance from <paramref name="ptr"/>.
	/// </summary>
	/// <typeparam name="TDelegate">Type of method delegate.</typeparam>
	/// <param name="ptr">A pointer to <typeparamref name="TDelegate"/> method.</param>
	/// <returns>A <typeparamref name="TDelegate"/> instance.</returns>
	public TDelegate GetDelegate<TDelegate>(IntPtr ptr) where TDelegate : Delegate
	{
		Type typeOfT = typeof(TDelegate);
		if (!this._delegates.ContainsKey(typeOfT))
			this._delegates.TryAdd(typeOfT, ptr.GetUnsafeDelegate<TDelegate>()!);
		return (TDelegate)this._delegates[typeOfT];
	}
}