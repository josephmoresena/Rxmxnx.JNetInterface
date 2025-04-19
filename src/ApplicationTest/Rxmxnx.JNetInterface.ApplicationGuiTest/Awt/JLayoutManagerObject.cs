using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Awt;

public sealed class JLayoutManagerObject : JInterfaceObject<JLayoutManagerObject>, IInterfaceType<JLayoutManagerObject>
{
	private static readonly JInterfaceTypeMetadata<JLayoutManagerObject> typeMetadata =
		TypeMetadataBuilder<JLayoutManagerObject>.Create("java/awt/LayoutManager"u8).Build();
	static JInterfaceTypeMetadata<JLayoutManagerObject> IInterfaceType<JLayoutManagerObject>.Metadata
		=> JLayoutManagerObject.typeMetadata;
	private JLayoutManagerObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
	static JLayoutManagerObject IInterfaceType<JLayoutManagerObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}