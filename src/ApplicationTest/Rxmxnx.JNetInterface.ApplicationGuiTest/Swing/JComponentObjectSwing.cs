using Rxmxnx.JNetInterface.Awt;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Swing;

public class JComponentObjectSwing : JContainerObject, IClassType<JComponentObjectSwing>
{
	private static readonly JClassTypeMetadata<JComponentObjectSwing> typeMetadata =
		TypeMetadataBuilder<JContainerObject>
			.Create<JComponentObjectSwing>("javax/swing/JComponent"u8, JTypeModifier.Abstract).Build();
	static JClassTypeMetadata<JComponentObjectSwing> IClassType<JComponentObjectSwing>.Metadata
		=> JComponentObjectSwing.typeMetadata;
	protected JComponentObjectSwing(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JComponentObjectSwing(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JComponentObjectSwing(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
	static JComponentObjectSwing IClassType<JComponentObjectSwing>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JComponentObjectSwing IClassType<JComponentObjectSwing>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JComponentObjectSwing IClassType<JComponentObjectSwing>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}