namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Access cache.
/// </summary>
internal sealed class AccessCache
{
	/// <summary>
	/// Class reference.
	/// </summary>
	private readonly JClassLocalRef _classRef;
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

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> instance.</param>
	public AccessCache(JClassLocalRef classRef) => this._classRef = classRef;

	public JFieldId GetFieldId(JFieldDefinition definition, JEnvironment env)
	{
		String hash = definition.Hash;
		if (this._fields.TryGetValue(hash, out JFieldId result)) return result;
		JTrace.GettingAccessibleId(this._classRef, definition);
		result = env.GetFieldId(definition, this._classRef);
		this._fields.TryAdd(hash, result);
		return result;
	}
	public JFieldId GetStaticFieldId(JFieldDefinition definition, JEnvironment env)
	{
		String hash = definition.Hash;
		if (this._staticFields.TryGetValue(hash, out JFieldId result)) return result;
		JTrace.GettingAccessibleId(this._classRef, definition);
		result = env.GetStaticFieldId(definition, this._classRef);
		this._staticFields.TryAdd(hash, result);
		return result;
	}
	public JMethodId GetMethodId(JCallDefinition definition, JEnvironment env)
	{
		String hash = definition.Hash;
		if (this._methods.TryGetValue(hash, out JMethodId result)) return result;
		JTrace.GettingAccessibleId(this._classRef, definition);
		result = env.GetMethodId(definition, this._classRef);
		this._methods.TryAdd(hash, result);
		return result;
	}
	public JMethodId GetStaticMethodId(JCallDefinition definition, JEnvironment env)
	{
		String hash = definition.Hash;
		if (this._staticMethods.TryGetValue(hash, out JMethodId result)) return result;
		JTrace.GettingAccessibleId(this._classRef, definition);
		result = env.GetStaticMethodId(definition, this._classRef);
		this._staticMethods.TryAdd(hash, result);
		return result;
	}
}