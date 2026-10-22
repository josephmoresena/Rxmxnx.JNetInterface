using System.Diagnostics;

namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Call reference frame
/// </summary>
internal sealed class CallFrame(INativeThread env) : AlienLocalCache(env)
{
	/// <inheritdoc/>
	public override String Name => "call";

	/// <summary>
	/// Sets the current instance as the current object cache.
	/// </summary>
	/// <param name="previous">Previous <see cref="LocalCache"/> instance.</param>
	public void Activate(out LocalCache previous)
	{
		previous = this.Environment.LocalCache;
		Debug.Assert(previous != this);
		this.SetPrevious(previous);
		this.Environment.LocalCache = this;
	}
}