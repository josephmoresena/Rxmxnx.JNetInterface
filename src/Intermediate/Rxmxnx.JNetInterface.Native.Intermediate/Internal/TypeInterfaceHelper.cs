namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Set of interfaces for given type.
/// </summary>
internal sealed record TypeInterfaceHelper<
	[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TReference>
{
	/// <summary>
	/// Internal set.
	/// </summary>
	public IReadOnlySet<Type> Set { get; } = typeof(TReference).GetInterfaces().ToImmutableHashSet();
}