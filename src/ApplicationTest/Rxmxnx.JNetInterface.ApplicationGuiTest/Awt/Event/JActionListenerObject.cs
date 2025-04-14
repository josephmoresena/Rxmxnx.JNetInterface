using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Awt.Event;

public sealed class JActionListenerObject : JInterfaceObject<JActionListenerObject>,
	IInterfaceType<JActionListenerObject>
{
	private static readonly JInterfaceTypeMetadata<JActionListenerObject> typeMetadata =
		TypeMetadataBuilder<JActionListenerObject>.Create("java/awt/event/ActionListener"u8).Build();

	static JInterfaceTypeMetadata<JActionListenerObject> IInterfaceType<JActionListenerObject>.Metadata
		=> JActionListenerObject.typeMetadata;

	private JActionListenerObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
	static JActionListenerObject IInterfaceType<JActionListenerObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}