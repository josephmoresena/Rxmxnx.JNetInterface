namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// JNI method information.
/// </summary>
internal record JniMethodInfo
{
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
	public JniMethodInfo() => this.Name = default!;
}

/// <summary>
/// JNI delegate information.
/// </summary>
internal sealed record JniDelegateInfo : JniMethodInfo
{
	/// <summary>
	/// Index of delegate.
	/// </summary>
	public Int32 Index { get; init; }
}