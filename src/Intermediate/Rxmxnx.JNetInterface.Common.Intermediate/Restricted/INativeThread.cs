namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// Represents a native java thread object.
/// </summary>
internal interface INativeThread : IEnvironment, IAccessibleManager, ILocalCacheOwner
{
	/// <summary>
	/// Indicates whether current native thread is attached to a JVM.
	/// </summary>
	Boolean IsAttached { get; }
}