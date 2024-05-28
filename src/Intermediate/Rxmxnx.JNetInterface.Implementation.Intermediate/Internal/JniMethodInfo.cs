namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// JNI method information.
/// </summary>
internal readonly struct JniMethodInfo
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