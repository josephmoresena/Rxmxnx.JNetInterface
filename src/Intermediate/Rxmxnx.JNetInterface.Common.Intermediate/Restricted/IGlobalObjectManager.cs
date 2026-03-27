namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// Represents a global object manager.
/// </summary>
internal interface IGlobalObjectManager
{
	/// <summary>
	/// Registers a <see cref="JGlobal"/> instance in current VM.
	/// </summary>
	/// <param name="jGlobal">A <see cref="JGlobal"/> instance.</param>
	[return: NotNullIfNotNull(nameof(jGlobal))]
	JGlobal? Register(JGlobal? jGlobal);
	/// <summary>
	/// Registers a <see cref="JWeak"/> instance in current VM.
	/// </summary>
	/// <param name="jWeak">A <see cref="JWeak"/> instance.</param>
	[return: NotNullIfNotNull(nameof(jWeak))]
	JWeak? Register(JWeak? jWeak);
	/// <summary>
	/// Removes <paramref name="jGlobal"/> from current VM cache.
	/// </summary>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	void Remove(JGlobalBase? jGlobal);
}