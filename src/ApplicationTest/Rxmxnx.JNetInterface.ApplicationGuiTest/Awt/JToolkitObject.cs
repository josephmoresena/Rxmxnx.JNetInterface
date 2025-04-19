using Rxmxnx.JNetInterface.Awt.Event;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Awt;

public class JToolkitObject : JLocalObject, IClassType<JToolkitObject>
{
	private static readonly IndeterminateCall addAwtEventListenerDef = IndeterminateCall.CreateMethodDefinition(
		"addAWTEventListener"u8, [
			JArgumentMetadata.Get<JAwtEventListenerObject>(), JArgumentMetadata.Get<JLong>(),
		]);
	private static readonly JClassTypeMetadata<JToolkitObject> typeMetadata =
		TypeMetadataBuilder<JToolkitObject>.Create("java/awt/Toolkit"u8, JTypeModifier.Abstract).Build();
	// getDefaultToolDef needs to be defined after typeMetadata.
	private static readonly JFunctionDefinition<JToolkitObject>.Parameterless getDefaultToolDef =
		new("getDefaultToolkit"u8);

	static JClassTypeMetadata<JToolkitObject> IClassType<JToolkitObject>.Metadata => JToolkitObject.typeMetadata;

	protected JToolkitObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JToolkitObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JToolkitObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public void AddEventListener(IInterfaceObject<JAwtEventListenerObject> eventListener, EventMask mask)
	{
		IEnvironment env = this.Environment;
		using JClassObject jClass = JClassObject.GetClass<JToolkitObject>(env);
		JToolkitObject.addAwtEventListenerDef.MethodCall(this, jClass, false, [eventListener, (JLong)(Int64)mask,]);
	}

	public static JToolkitObject GetDefaultToolkit(IEnvironment env)
	{
		using JClassObject jClass = JClassObject.GetClass<JToolkitObject>(env);
		return JToolkitObject.getDefaultToolDef.StaticInvoke(jClass)!;
	}

	static JToolkitObject IClassType<JToolkitObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JToolkitObject IClassType<JToolkitObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JToolkitObject IClassType<JToolkitObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}