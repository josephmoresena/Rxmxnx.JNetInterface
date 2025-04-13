using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.ApplicationTest.Util.Concurrent;

public sealed class JCallableObject : JInterfaceObject<JCallableObject>, IInterfaceType<JCallableObject>
{
	private static readonly JInterfaceTypeMetadata<JCallableObject> typeMetadata =
		TypeMetadataBuilder<JCallableObject>.Create("java/util/concurrent/Callable"u8).Build();

	static JInterfaceTypeMetadata<JCallableObject> IInterfaceType<JCallableObject>.Metadata
		=> JCallableObject.typeMetadata;

	private JCallableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
	static JCallableObject IInterfaceType<JCallableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}