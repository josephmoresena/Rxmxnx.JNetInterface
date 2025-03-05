namespace Rxmxnx.JNetInterface.Internal;

internal static partial class MetadataHelper
{
	/// <summary>
	/// Registers <paramref name="metadata"/> into the runtime metadata.
	/// </summary>
	/// <param name="metadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="metadata"/> was registered; otherwise, <see langword="false"/>.
	/// </returns>
	private static Boolean Register(JReferenceTypeMetadata? metadata)
	{
		if (metadata is null || MetadataHelper.storage.IsRegistered(metadata.Hash)) return false;
		if (metadata.BaseMetadata is not null)
		{
			AssignationKey assignationKey = new() { FromHash = metadata.Hash, ToHash = metadata.BaseMetadata.Hash, };
			MetadataHelper.storage[assignationKey] = true;
			MetadataHelper.Register(metadata.BaseMetadata);
		}
		metadata.Interfaces.ForEach(metadata, MetadataHelper.RegisterInterfaceAssignation);
		if (metadata is JArrayTypeMetadata arrayMetadata)
			MetadataHelper.Register(arrayMetadata.ElementMetadata as JReferenceTypeMetadata);
		return MetadataHelper.storage.TryAdd(metadata);
	}
	/// <summary>
	/// Registers assignation of <paramref name="metadata"/> to <paramref name="interfaceMetadata"/>.
	/// </summary>
	/// <param name="metadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <param name="interfaceMetadata">A <see cref="JInterfaceTypeMetadata"/> instance.</param>
	private static void RegisterInterfaceAssignation(JReferenceTypeMetadata metadata,
		JInterfaceTypeMetadata interfaceMetadata)
	{
		AssignationKey assignationKey = new() { FromHash = metadata.Hash, ToHash = interfaceMetadata.Hash, };
		MetadataHelper.storage[assignationKey] = true;
		MetadataHelper.Register(interfaceMetadata);
	}
	/// <summary>
	/// Indicates whether the type of <paramref name="typeMetadata"/> is built-in final type.
	/// </summary>
	/// <param name="typeMetadata">A <see cref="JDataTypeMetadata"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if the type of <paramref name="typeMetadata"/> is built-in final type;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	private static Boolean IsBuiltInFinalType(JDataTypeMetadata typeMetadata)
	{
		if (typeMetadata is JEnumTypeMetadata or JPrimitiveTypeMetadata) return true;

		if (JProxyObject.ProxyTypeMetadata.Equals((typeMetadata as JReferenceTypeMetadata)?.BaseMetadata))
			return true;

		if (MetadataHelper.storage.IsBuiltInAndFinalType(typeMetadata.Hash))
			return true;

		return typeMetadata is JArrayTypeMetadata arrayTypeMetadata &&
			MetadataHelper.IsBuiltInFinalType(arrayTypeMetadata.ElementMetadata);
	}
}