using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Awt;

public class JDialogObject : JWindowObject, IClassType<JDialogObject>
{
	private static readonly JClassTypeMetadata<JDialogObject> typeMetadata =
		TypeMetadataBuilder<JWindowObject>.Create<JDialogObject>("java/awt/Dialog"u8).Build();

	static JClassTypeMetadata<JDialogObject> IClassType<JDialogObject>.Metadata => JDialogObject.typeMetadata;
#if !NET8_0_OR_GREATER
	// .NET 7.0 has issues inheriting static abstract members in non-generic interfaces from base classes.
	static Int32 IDataType.AndroidApiLevel => -1;
#endif

	protected JDialogObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JDialogObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JDialogObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JDialogObject IClassType<JDialogObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JDialogObject IClassType<JDialogObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JDialogObject IClassType<JDialogObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}