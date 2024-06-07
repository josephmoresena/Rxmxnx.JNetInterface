namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Set of interfaces for given type.
/// </summary>
internal sealed class TypeInterfaceHelper<
	[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TReference>
{
	/// <summary>
	/// Cached type interface set.
	/// </summary>
	private static readonly WeakReference<TypeInterfaceHelper<TReference>?> singleton = new(default);

	/// <summary>
	/// Retrieves current type interfaces set.
	/// </summary>
	public static IReadOnlySet<Type> TypeInterfaces
	{
		get
		{
			if (TypeInterfaceHelper<TReference>.singleton.TryGetTarget(out TypeInterfaceHelper<TReference>? result))
				return result._set;
			result = new();
			TypeInterfaceHelper<TReference>.singleton.SetTarget(result);
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
	private TypeInterfaceHelper() => this._set = typeof(TReference).GetInterfaces().ToImmutableHashSet();
}