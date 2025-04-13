using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.ApplicationTest.Swing;

public sealed class JSwingUtilitiesObject : JLocalObject.Uninstantiable<JSwingUtilitiesObject>,
	IUninstantiableType<JSwingUtilitiesObject>
{
	private static readonly JClassTypeMetadata<JSwingUtilitiesObject> typeMetadata =
		TypeMetadataBuilder<JSwingUtilitiesObject>.Create("javax/swing/SwingUtilities"u8).Build();
	private static readonly IndeterminateCall invokeAndWaitDef =
		IndeterminateCall.CreateMethodDefinition("invokeAndWait"u8, [JArgumentMetadata.Get<JRunnableObject>(),]);

	static JClassTypeMetadata<JSwingUtilitiesObject> IClassType<JSwingUtilitiesObject>.Metadata
		=> JSwingUtilitiesObject.typeMetadata;

	public static void InvokeAndWait(JRunnableObject runnableObject)
	{
		IEnvironment env = runnableObject.Environment;
		using JClassObject jClass = JClassObject.GetClass<JSwingUtilitiesObject>(env);
		JSwingUtilitiesObject.invokeAndWaitDef.StaticMethodCall(jClass, [runnableObject,]);
	}
}