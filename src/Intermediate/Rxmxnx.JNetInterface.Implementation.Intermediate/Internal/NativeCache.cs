namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Native method cache
/// </summary>
public sealed class NativeCache
{
	/// <summary>
	/// Class dictionary.
	/// </summary>
	private readonly ConcurrentDictionary<String, ConcurrentDictionary<String, JNativeCallEntry>> _calls = new();

	/// <summary>
	/// Cached class.
	/// </summary>
	/// <param name="hash">Class hash.</param>
	public IReadOnlyList<JNativeCallEntry> this[String hash]
	{
		set
		{
			if (!this._calls.ContainsKey(hash))
				this._calls[hash] = new();
			ConcurrentDictionary<String, JNativeCallEntry> calls = this._calls[hash];
			foreach (JNativeCallEntry t in value)
				calls[t.Hash] = t;
		}
	}

	/// <summary>
	/// Clears cache.
	/// </summary>
	/// <param name="hash"></param>
	public void Clear(String hash)
	{
		if (this._calls.TryGetValue(hash, out ConcurrentDictionary<String, JNativeCallEntry>? value))
			value.Clear();
	}
}