using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.ApplicationTest.Util.Concurrent;

public sealed class JRunnableFutureObject : JInterfaceObject<JRunnableFutureObject>,
	IInterfaceType<JRunnableFutureObject>
{
	private static readonly JInterfaceTypeMetadata<JRunnableFutureObject> typeMetadata =
		TypeMetadataBuilder<JRunnableFutureObject>.Create("java/util/concurrent/RunnableFuture"u8).Build();

	static JInterfaceTypeMetadata<JRunnableFutureObject> IInterfaceType<JRunnableFutureObject>.Metadata
		=> JRunnableFutureObject.typeMetadata;

	private JRunnableFutureObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
	static JRunnableFutureObject IInterfaceType<JRunnableFutureObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}