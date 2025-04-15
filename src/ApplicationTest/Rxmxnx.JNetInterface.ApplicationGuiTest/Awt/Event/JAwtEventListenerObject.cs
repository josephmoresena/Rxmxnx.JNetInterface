using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;
using Rxmxnx.JNetInterface.Util;

namespace Rxmxnx.JNetInterface.Awt.Event;

public sealed class JAwtEventListenerObject : JInterfaceObject<JAwtEventListenerObject>,
	IInterfaceType<JAwtEventListenerObject>, IInterfaceObject<JEventListenerObject>
{
	private static readonly JInterfaceTypeMetadata<JAwtEventListenerObject> typeMetadata =
		TypeMetadataBuilder<JAwtEventListenerObject>.Create("java/awt/event/AWTEventListener"u8)
		                                            .Extends<JEventListenerObject>().Build();
	static JInterfaceTypeMetadata<JAwtEventListenerObject> IInterfaceType<JAwtEventListenerObject>.Metadata
		=> JAwtEventListenerObject.typeMetadata;

	private JAwtEventListenerObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
	static JAwtEventListenerObject IInterfaceType<JAwtEventListenerObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}