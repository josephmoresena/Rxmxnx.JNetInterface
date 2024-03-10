namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// JNI delegate information.
/// </summary>
internal sealed record JniDelegateInfo
{
	/// <summary>
	/// Index of delegate.
	/// </summary>
	public Int32 Index { get; init; }
	/// <summary>
	/// Name of delegate.
	/// </summary>
	public String Name { get; init; }
	/// <summary>
	/// Safety level of delegate.
	/// </summary>
	public JniSafetyLevels Level { get; init; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public JniDelegateInfo() => this.Name = default!;
}