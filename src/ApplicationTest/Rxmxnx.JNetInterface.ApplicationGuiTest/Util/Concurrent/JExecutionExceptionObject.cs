using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.ApplicationTest.Util.Concurrent;

public sealed class JExecutionExceptionObject : JInterfaceObject<JExecutionExceptionObject>,
	IInterfaceType<JExecutionExceptionObject>
{
	private static readonly JInterfaceTypeMetadata<JExecutionExceptionObject> typeMetadata =
		TypeMetadataBuilder<JExecutionExceptionObject>.Create("java/util/concurrent/ExecutionException"u8).Build();

	static JInterfaceTypeMetadata<JExecutionExceptionObject> IInterfaceType<JExecutionExceptionObject>.Metadata
		=> JExecutionExceptionObject.typeMetadata;

	private JExecutionExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
	static JExecutionExceptionObject IInterfaceType<JExecutionExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}