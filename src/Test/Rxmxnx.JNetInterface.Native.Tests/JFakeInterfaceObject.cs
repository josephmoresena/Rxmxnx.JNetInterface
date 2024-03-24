namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public class JFakeInterfaceObject : JInterfaceObject<JFakeInterfaceObject>, IInterfaceType<JFakeInterfaceObject>
{
	public static readonly JInterfaceTypeMetadata<JFakeInterfaceObject> TypeMetadata =
		TypeMetadataBuilder<JFakeInterfaceObject>.Create((CString)new Fixture().Create<String>()).Build();
	static JInterfaceTypeMetadata<JFakeInterfaceObject> IInterfaceType<JFakeInterfaceObject>.Metadata
		=> JFakeInterfaceObject.TypeMetadata;

	private JFakeInterfaceObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
	static JFakeInterfaceObject IInterfaceType<JFakeInterfaceObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}