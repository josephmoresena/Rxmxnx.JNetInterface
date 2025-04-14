using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;
using Rxmxnx.JNetInterface.Util;

namespace Rxmxnx.JNetInterface.Awt;

public class JAwtEventObject : JEventObject, IClassType<JAwtEventObject>
{
	private static readonly JClassTypeMetadata<JAwtEventObject> typeMetadata = TypeMetadataBuilder<JEventObject>
	                                                                           .Create<JAwtEventObject>(
		                                                                           "java/awt/AWTEvent"u8,
		                                                                           JTypeModifier.Abstract).Build();
	static JClassTypeMetadata<JAwtEventObject> IClassType<JAwtEventObject>.Metadata => JAwtEventObject.typeMetadata;

	protected JAwtEventObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JAwtEventObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JAwtEventObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JAwtEventObject IClassType<JAwtEventObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JAwtEventObject IClassType<JAwtEventObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JAwtEventObject IClassType<JAwtEventObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}