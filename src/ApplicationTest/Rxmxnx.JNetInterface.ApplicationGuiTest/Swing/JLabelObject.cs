using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Swing;

public class JLabelObject : JComponentObjectSwing, IClassType<JLabelObject>
{
	private static readonly IndeterminateCall constructorTextDef =
		IndeterminateCall.CreateConstructorDefinition([JArgumentMetadata.Get<JStringObject>(),]);
	private static readonly IndeterminateCall constructorIconDef =
		IndeterminateCall.CreateConstructorDefinition([JArgumentMetadata.Get<JIconObject>(),]);
	private static readonly JClassTypeMetadata<JLabelObject> typeMetadata =
		TypeMetadataBuilder<JComponentObjectSwing>.Create<JLabelObject>("javax/swing/JLabel"u8).Build();

	static JClassTypeMetadata<JLabelObject> IClassType<JLabelObject>.Metadata => JLabelObject.typeMetadata;
	protected JLabelObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JLabelObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JLabelObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public static JLabelObject Create(IInterfaceObject<JIconObject> icon)
	{
		IEnvironment env = icon.CastTo<JLocalObject>().Environment;
		using JClassObject jClass = JClassObject.GetClass<JLabelObject>(env);
		return JLabelObject.constructorIconDef.NewCall<JLabelObject>(env, [icon,]);
	}
	public static JLabelObject Create(IEnvironment env, String htmlText)
	{
		using JClassObject jClass = JClassObject.GetClass<JLabelObject>(env);
		using JStringObject jString = JStringObject.Create(env, htmlText);
		return JLabelObject.constructorTextDef.NewCall<JLabelObject>(env, [jString,]);
	}

	static JLabelObject IClassType<JLabelObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JLabelObject IClassType<JLabelObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JLabelObject IClassType<JLabelObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}