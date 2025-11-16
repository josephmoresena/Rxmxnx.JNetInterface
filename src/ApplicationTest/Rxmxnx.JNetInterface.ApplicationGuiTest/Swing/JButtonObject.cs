using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Swing;

public class JButtonObject : JAbstractButtonObject, IClassType<JButtonObject>
{
	private static readonly IndeterminateCall constructorDef = IndeterminateCall.CreateConstructorDefinition(
#if !NET9_0_OR_GREATER
		[JArgumentMetadata.Get<JStringObject>(),]
#else
		JArgumentMetadata.Get<JStringObject>()
#endif
	);
	private static readonly JClassTypeMetadata<JButtonObject> typeMetadata = TypeMetadataBuilder<JAbstractButtonObject>
	                                                                         .Create<JButtonObject>(
		                                                                         "javax/swing/JButton"u8).Build();
	static JClassTypeMetadata<JButtonObject> IClassType<JButtonObject>.Metadata => JButtonObject.typeMetadata;
#if !NET8_0_OR_GREATER
	// .NET 7.0 has issues inheriting static abstract members in non-generic interfaces from base classes.
	static JRuntimeVersion IDataType.Since => JRuntimeVersion.SEd1;
#endif

	protected JButtonObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JButtonObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JButtonObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public static JButtonObject Create(IEnvironment env, ReadOnlySpan<Byte> text)
	{
		using JClassObject jClass = JClassObject.GetClass<JButtonObject>(env);
		using JStringObject jString = JStringObject.Create(env, text);
#if !NET9_0_OR_GREATER
		return JButtonObject.constructorDef.NewCall<JButtonObject>(env, [jString,]);
#else
		return JButtonObject.constructorDef.NewCall<JButtonObject>(env, jString);
#endif
	}

	static JButtonObject IClassType<JButtonObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JButtonObject IClassType<JButtonObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JButtonObject IClassType<JButtonObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}