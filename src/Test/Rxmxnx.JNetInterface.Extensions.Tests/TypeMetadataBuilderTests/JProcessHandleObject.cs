namespace Rxmxnx.JNetInterface.Tests;

public sealed partial class TypeMetadataBuilderTests
{
	public sealed class JProcessHandleObject : JInterfaceObject<JProcessHandleObject>,
		IInterfaceType<JProcessHandleObject>, IInterfaceObject<JComparableObject>
	{
		private static readonly JInterfaceTypeMetadata<JProcessHandleObject> typeMetadata =
			TypeMetadataBuilder<JProcessHandleObject>
				.Create(TypeMetadataBuilderTests.classNames[typeof(JProcessHandleObject)]).Extends<JComparableObject>()
				.Build();
		static JInterfaceTypeMetadata<JProcessHandleObject> IInterfaceType<JProcessHandleObject>.Metadata
			=> JProcessHandleObject.typeMetadata;
		private JProcessHandleObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
		static JProcessHandleObject IInterfaceType<JProcessHandleObject>.Create(
			IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
	}
}