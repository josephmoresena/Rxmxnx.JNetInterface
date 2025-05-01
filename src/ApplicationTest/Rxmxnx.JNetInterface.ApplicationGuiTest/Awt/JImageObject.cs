using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Awt;

public class JImageObject : JLocalObject, IClassType<JImageObject>
{
	private static readonly JClassTypeMetadata<JImageObject> typeMetadata =
		TypeMetadataBuilder<JImageObject>.Create("java/awt/Image"u8, JTypeModifier.Abstract).Build();

	static JClassTypeMetadata<JImageObject> IClassType<JImageObject>.Metadata => JImageObject.typeMetadata;

	protected JImageObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JImageObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JImageObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JImageObject IClassType<JImageObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JImageObject IClassType<JImageObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JImageObject IClassType<JImageObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}