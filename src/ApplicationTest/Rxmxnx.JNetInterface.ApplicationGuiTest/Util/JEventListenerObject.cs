using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Util;

public sealed class JEventListenerObject : JInterfaceObject<JEventListenerObject>, IInterfaceType<JEventListenerObject>
{
	private static readonly JInterfaceTypeMetadata<JEventListenerObject> typeMetadata =
		TypeMetadataBuilder<JEventListenerObject>.Create("java/util/EventListener"u8).Build();

	static JInterfaceTypeMetadata<JEventListenerObject> IInterfaceType<JEventListenerObject>.Metadata
		=> JEventListenerObject.typeMetadata;

	private JEventListenerObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
	static JEventListenerObject IInterfaceType<JEventListenerObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}