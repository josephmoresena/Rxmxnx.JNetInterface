namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Set of base types for given type.
/// </summary>
internal sealed class BaseTypeHelper<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TClass>
{
	/// <summary>
	/// Cached type interface set.
	/// </summary>
	private static readonly WeakReference<BaseTypeHelper<TClass>?> singleton = new(default);

	/// <summary>
	/// Retrieves current type base type set.
	/// </summary>
	internal static IReadOnlySet<Type> TypeBaseTypes
	{
		get
		{
			if (BaseTypeHelper<TClass>.singleton.TryGetTarget(out BaseTypeHelper<TClass>? result))
				return result._set;
			result = new();
			BaseTypeHelper<TClass>.singleton.SetTarget(result);
			return result._set;
		}
	}

	/// <summary>
	/// Internal set.
	/// </summary>
	private readonly ImmutableHashSet<Type> _set;

	/// <summary>
	/// Constructor.
	/// </summary>
	private BaseTypeHelper() => this._set = BaseTypeHelper<TClass>.GetBaseTypes().ToImmutableHashSet();

	/// <summary>
	/// Retrieves the base types from current type.
	/// </summary>
	/// <returns>Enumerable of types.</returns>
	private static IEnumerable<Type> GetBaseTypes()
	{
		Type? currentType = typeof(TClass).BaseType;
		while (currentType is not null && currentType != typeof(JReferenceObject))
		{
			yield return currentType;
			currentType = currentType.BaseType;
		}
	}
}