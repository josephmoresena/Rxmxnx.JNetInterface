using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Swing;

public class JLabelObject : JComponentObjectSwing, IClassType<JLabelObject>
{
	private static readonly IndeterminateCall constructorTextDef = IndeterminateCall.CreateConstructorDefinition(
#if !NET9_0_OR_GREATER
		[JArgumentMetadata.Get<JStringObject>(),]
#else
		JArgumentMetadata.Get<JStringObject>()
#endif
	);
	private static readonly IndeterminateCall constructorIconDef = IndeterminateCall.CreateConstructorDefinition(
#if !NET9_0_OR_GREATER
		[JArgumentMetadata.Get<JIconObject>(),]
#else
		JArgumentMetadata.Get<JIconObject>()
#endif
	);
	private static readonly JClassTypeMetadata<JLabelObject> typeMetadata =
		TypeMetadataBuilder<JComponentObjectSwing>.Create<JLabelObject>("javax/swing/JLabel"u8).Build();

	static JClassTypeMetadata<JLabelObject> IClassType<JLabelObject>.Metadata => JLabelObject.typeMetadata;
#if !NET8_0_OR_GREATER
	// .NET 7.0 has issues inheriting static abstract members in non-generic interfaces from base classes.
	static JRuntimeVersion IDataType.Since => JRuntimeVersion.SEd1;
#endif

	protected JLabelObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JLabelObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JLabelObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public static JLabelObject Create(IInterfaceObject<JIconObject> icon)
	{
		IEnvironment env = icon.Environment;
		using JClassObject jClass = JClassObject.GetClass<JLabelObject>(env);
		return JLabelObject.constructorIconDef.NewCall<JLabelObject>(env,
#if !NET9_0_OR_GREATER
		                                                             [icon,]
#else
		                                                             icon
#endif
		);
	}
	public static JLabelObject Create(IEnvironment env, String htmlText)
	{
		using JClassObject jClass = JClassObject.GetClass<JLabelObject>(env);
		using JStringObject jString = JStringObject.Create(env, htmlText);
		return JLabelObject.constructorTextDef.NewCall<JLabelObject>(env,
#if !NET9_0_OR_GREATER
		                                                             [jString,]
#else
		                                                             jString
#endif
		);
	}

	static JLabelObject IClassType<JLabelObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JLabelObject IClassType<JLabelObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JLabelObject IClassType<JLabelObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}