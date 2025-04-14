using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Swing;

public sealed class JIconObject : JInterfaceObject<JIconObject>, IInterfaceType<JIconObject>
{
	private static readonly JInterfaceTypeMetadata<JIconObject> typeMetadata =
		TypeMetadataBuilder<JIconObject>.Create("javax/swing/Icon"u8).Build();

	static JInterfaceTypeMetadata<JIconObject> IInterfaceType<JIconObject>.Metadata => JIconObject.typeMetadata;

	private JIconObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
	static JIconObject IInterfaceType<JIconObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}