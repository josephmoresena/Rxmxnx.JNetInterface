using Rxmxnx.JNetInterface.Awt;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Swing;

public class JDialogObjectSwing : JDialogObject, IClassType<JDialogObjectSwing>
{
	private static readonly IndeterminateCall constructorDef =
		IndeterminateCall.CreateConstructorDefinition([JArgumentMetadata.Get<JWindowObject>(),]);
	private static readonly JClassTypeMetadata<JDialogObjectSwing> typeMetadata =
		TypeMetadataBuilder<JDialogObject>.Create<JDialogObjectSwing>("javax/swing/JDialog"u8).Build();
	static JClassTypeMetadata<JDialogObjectSwing> IClassType<JDialogObjectSwing>.Metadata
		=> JDialogObjectSwing.typeMetadata;
	protected JDialogObjectSwing(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JDialogObjectSwing(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JDialogObjectSwing(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public static JDialogObjectSwing Create(JWindowObject window, String title)
	{
		IEnvironment env = window.Environment;
		using JClassObject jClass = JClassObject.GetClass<JDialogObject>(env);
		using JStringObject jString = JStringObject.Create(env, title);
		return JDialogObjectSwing.constructorDef.NewCall<JDialogObjectSwing>(env, [jClass, jString,]);
	}

	static JDialogObjectSwing IClassType<JDialogObjectSwing>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JDialogObjectSwing IClassType<JDialogObjectSwing>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JDialogObjectSwing IClassType<JDialogObjectSwing>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}