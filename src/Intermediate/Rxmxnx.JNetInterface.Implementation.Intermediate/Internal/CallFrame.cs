namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Call reference frame
/// </summary>
internal sealed class CallFrame(INativeThread env) : AlienLocalCache(env)
{
	/// <inheritdoc/>
	public override String Name => "call";

	/// <summary>
	/// Sets current instance as current object cache.
	/// </summary>
	/// <param name="previous">Previous <see cref="LocalCache"/> instance.</param>
	public void Activate(out LocalCache previous)
	{
		previous = this.Environment.LocalCache;
		this.SetPrevious(previous);
		this.Environment.LocalCache = this;
	}
}