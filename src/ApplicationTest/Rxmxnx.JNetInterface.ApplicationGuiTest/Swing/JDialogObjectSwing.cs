using Rxmxnx.JNetInterface.Awt;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Swing;

public class JDialogObjectSwing : JDialogObject, IClassType<JDialogObjectSwing>
{
	private static readonly IndeterminateCall constructorDef = IndeterminateCall.CreateConstructorDefinition(
#if !NET9_0_OR_GREATER
		[JArgumentMetadata.Get<JWindowObject>(), JArgumentMetadata.Get<JStringObject>(),]
#else
		JArgumentMetadata.Get<JWindowObject>(), JArgumentMetadata.Get<JStringObject>()
#endif
	);
	private static readonly JClassTypeMetadata<JDialogObjectSwing> typeMetadata =
		TypeMetadataBuilder<JDialogObject>.Create<JDialogObjectSwing>("javax/swing/JDialog"u8).Build();
	static JClassTypeMetadata<JDialogObjectSwing> IClassType<JDialogObjectSwing>.Metadata
		=> JDialogObjectSwing.typeMetadata;
	static JRuntimeVersion IDataType.Since => JRuntimeVersion.SEd2;

	protected JDialogObjectSwing(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JDialogObjectSwing(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JDialogObjectSwing(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public static JDialogObjectSwing Create(JWindowObject window, String title)
	{
		IEnvironment env = window.Environment;
		using JClassObject jClass = JClassObject.GetClass<JDialogObject>(env);
		using JStringObject jString = JStringObject.Create(env, title);
#if !NET9_0_OR_GREATER
		return JDialogObjectSwing.constructorDef.NewCall<JDialogObjectSwing>(env, [window, jString,]);
#else
		return JDialogObjectSwing.constructorDef.NewCall<JDialogObjectSwing>(env, window, jString);
#endif
	}

	static JDialogObjectSwing IClassType<JDialogObjectSwing>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JDialogObjectSwing IClassType<JDialogObjectSwing>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JDialogObjectSwing IClassType<JDialogObjectSwing>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}