using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Swing;

public class JButtonObject : JAbstractButtonObject, IClassType<JButtonObject>
{
	private static readonly IndeterminateCall constructorDef =
		IndeterminateCall.CreateConstructorDefinition([JArgumentMetadata.Get<JStringObject>(),]);
	private static readonly JClassTypeMetadata<JButtonObject> typeMetadata = TypeMetadataBuilder<JAbstractButtonObject>
	                                                                         .Create<JButtonObject>(
		                                                                         "javax/swing/JButton"u8).Build();
	static JClassTypeMetadata<JButtonObject> IClassType<JButtonObject>.Metadata => JButtonObject.typeMetadata;
	protected JButtonObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JButtonObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JButtonObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public static JButtonObject Create(IEnvironment env, ReadOnlySpan<Byte> text)
	{
		using JClassObject jClass = JClassObject.GetClass<JButtonObject>(env);
		using JStringObject jString = JStringObject.Create(env, text);
		return JButtonObject.constructorDef.NewCall<JButtonObject>(env, [jString,]);
	}

	static JButtonObject IClassType<JButtonObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JButtonObject IClassType<JButtonObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JButtonObject IClassType<JButtonObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}