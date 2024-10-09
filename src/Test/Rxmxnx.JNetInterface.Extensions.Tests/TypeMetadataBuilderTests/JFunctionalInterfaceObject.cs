namespace Rxmxnx.JNetInterface.Tests;

public sealed partial class TypeMetadataBuilderTests
{
	public sealed class JFunctionalInterfaceObject : JAnnotationObject<JFunctionalInterfaceObject>,
		IInterfaceType<JFunctionalInterfaceObject>
	{
		private static readonly JInterfaceTypeMetadata<JFunctionalInterfaceObject> typeMetadata =
			TypeMetadataBuilder<JFunctionalInterfaceObject>
				.Create(TypeMetadataBuilderTests.classNames[typeof(JFunctionalInterfaceObject)]).Build();
		static JInterfaceTypeMetadata<JFunctionalInterfaceObject> IInterfaceType<JFunctionalInterfaceObject>.Metadata
			=> JFunctionalInterfaceObject.typeMetadata;

		private JFunctionalInterfaceObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
		static JFunctionalInterfaceObject IInterfaceType<JFunctionalInterfaceObject>.Create(
			IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
	}
}