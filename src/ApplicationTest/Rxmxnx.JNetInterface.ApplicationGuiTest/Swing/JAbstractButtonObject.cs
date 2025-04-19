using Rxmxnx.JNetInterface.Awt.Event;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Swing;

public class JAbstractButtonObject : JComponentObjectSwing, IClassType<JAbstractButtonObject>
{
	private static readonly IndeterminateCall addActionListenerDef =
		IndeterminateCall.CreateMethodDefinition("addActionListener"u8,
		                                         [JArgumentMetadata.Get<JActionListenerObject>(),]);
	private static readonly JClassTypeMetadata<JAbstractButtonObject> typeMetadata =
		TypeMetadataBuilder<JComponentObjectSwing>.Create<JAbstractButtonObject>("javax/swing/AbstractButton"u8)
		                                          .Build();

	static JClassTypeMetadata<JAbstractButtonObject> IClassType<JAbstractButtonObject>.Metadata
		=> JAbstractButtonObject.typeMetadata;

	protected JAbstractButtonObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JAbstractButtonObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JAbstractButtonObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public void AddActionListener(IInterfaceObject<JActionListenerObject> listener)
	{
		IEnvironment env = this.Environment;
		using JClassObject jClass = JClassObject.GetClass<JAbstractButtonObject>(env);
		JAbstractButtonObject.addActionListenerDef.MethodCall(this, jClass, false, [listener,]);
	}

	static JAbstractButtonObject IClassType<JAbstractButtonObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JAbstractButtonObject IClassType<JAbstractButtonObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JAbstractButtonObject IClassType<JAbstractButtonObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}