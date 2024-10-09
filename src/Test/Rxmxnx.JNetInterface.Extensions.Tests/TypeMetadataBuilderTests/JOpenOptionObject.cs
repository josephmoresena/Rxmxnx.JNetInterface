namespace Rxmxnx.JNetInterface.Tests;

public sealed partial class TypeMetadataBuilderTests
{
	public sealed class JOpenOptionObject : JInterfaceObject<JOpenOptionObject>, IInterfaceType<JOpenOptionObject>,
		IInterfaceObject<JComparableObject>
	{
		private static readonly JInterfaceTypeMetadata<JOpenOptionObject> typeMetadata =
			TypeMetadataBuilder<JOpenOptionObject>
				.Create(TypeMetadataBuilderTests.classNames[typeof(JOpenOptionObject)]).Build();
		static JInterfaceTypeMetadata<JOpenOptionObject> IInterfaceType<JOpenOptionObject>.Metadata
			=> JOpenOptionObject.typeMetadata;
		private JOpenOptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
		static JOpenOptionObject IInterfaceType<JOpenOptionObject>.Create(IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
	}
}