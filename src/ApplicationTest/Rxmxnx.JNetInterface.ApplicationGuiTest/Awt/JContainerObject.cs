using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.ApplicationTest.Awt;

public class JContainerObject : JComponentObject, IClassType<JContainerObject>
{
	private static readonly JClassTypeMetadata<JContainerObject> typeMetadata =
		TypeMetadataBuilder<JComponentObject>.Create<JContainerObject>("java/awt/Container"u8).Build();

	static JClassTypeMetadata<JContainerObject> IClassType<JContainerObject>.Metadata => JContainerObject.typeMetadata;

	protected JContainerObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JContainerObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JContainerObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
	static JContainerObject IClassType<JContainerObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JContainerObject IClassType<JContainerObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JContainerObject IClassType<JContainerObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}