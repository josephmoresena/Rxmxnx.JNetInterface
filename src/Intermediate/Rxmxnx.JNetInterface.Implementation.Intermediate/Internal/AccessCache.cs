namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Access cache.
/// </summary>
internal sealed class AccessCache
{
	/// <summary>
	/// Dictionary of fields.
	/// </summary>
	private readonly ConcurrentDictionary<String, JFieldId> _fields = new();
	/// <summary>
	/// Dictionary of instance methods.
	/// </summary>
	private readonly ConcurrentDictionary<String, JMethodId> _methods = new();
	/// <summary>
	/// Dictionary of static fields.
	/// </summary>
	private readonly ConcurrentDictionary<String, JFieldId> _staticFields = new();
	/// <summary>
	/// Dictionary of static methods.
	/// </summary>
	private readonly ConcurrentDictionary<String, JMethodId> _staticMethods = new();
}