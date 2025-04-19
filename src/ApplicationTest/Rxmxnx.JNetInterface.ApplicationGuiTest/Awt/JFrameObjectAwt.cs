using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Awt;

public class JFrameObjectAwt : JWindowObject, IClassType<JFrameObjectAwt>
{
	private static readonly JClassTypeMetadata<JFrameObjectAwt> typeMetadata =
		TypeMetadataBuilder<JWindowObject>.Create<JFrameObjectAwt>("java/awt/Frame"u8).Build();

	static JClassTypeMetadata<JFrameObjectAwt> IClassType<JFrameObjectAwt>.Metadata => JFrameObjectAwt.typeMetadata;

	protected JFrameObjectAwt(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JFrameObjectAwt(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JFrameObjectAwt(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
	static JFrameObjectAwt IClassType<JFrameObjectAwt>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JFrameObjectAwt IClassType<JFrameObjectAwt>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JFrameObjectAwt IClassType<JFrameObjectAwt>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}