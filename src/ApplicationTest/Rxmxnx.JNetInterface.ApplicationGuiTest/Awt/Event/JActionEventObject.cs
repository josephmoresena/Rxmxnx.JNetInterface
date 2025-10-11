using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Awt.Event;

public class JActionEventObject : JAwtEventObject, IClassType<JActionEventObject>
{
	private static readonly JClassTypeMetadata<JActionEventObject> typeMetadata = TypeMetadataBuilder<JAwtEventObject>
		.Create<JActionEventObject>("java/awt/event/ActionEvent"u8).Build();

	static JClassTypeMetadata<JActionEventObject> IClassType<JActionEventObject>.Metadata
		=> JActionEventObject.typeMetadata;
#if !NET8_0_OR_GREATER
	// .NET 7.0 has issues inheriting static abstract members in non-generic interfaces from base classes.
	static JRuntimeVersion IDataType.Since => JRuntimeVersion.SEd1;
#endif

	protected JActionEventObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JActionEventObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JActionEventObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
	static JActionEventObject IClassType<JActionEventObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JActionEventObject IClassType<JActionEventObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JActionEventObject IClassType<JActionEventObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}