namespace Rxmxnx.JNetInterface.Internal.ConstantValues;

/// <summary>
/// Java wrapper classes constants.
/// </summary>
internal static class JPrimitiveWrapperConstants
{
	/// <summary>
	/// Interfaces for <see cref="IPrimitiveWrapperType"/>.
	/// </summary>
	public static readonly IImmutableSet<JInterfaceTypeMetadata> Interfaces =
		ImmutableHashSet.Create(IInterfaceType.GetMetadata<JSerializableObject>(),
		                        IInterfaceType.GetMetadata<JComparableObject>());
}