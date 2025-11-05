using Rxmxnx.JNetInterface.Awt;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Swing;

public sealed class JRootPaneContainerObject : JInterfaceObject<JRootPaneContainerObject>,
	IInterfaceType<JRootPaneContainerObject>
{
	private static readonly JFunctionDefinition<JContainerObject>.Parameterless getContentPaneDef =
		new("getContentPane"u8);
	private static readonly JInterfaceTypeMetadata<JRootPaneContainerObject> typeMetadata =
		TypeMetadataBuilder<JRootPaneContainerObject>.Create("javax/swing/RootPaneContainer"u8).Build();

	static JInterfaceTypeMetadata<JRootPaneContainerObject> IInterfaceType<JRootPaneContainerObject>.Metadata
		=> JRootPaneContainerObject.typeMetadata;

	private JRootPaneContainerObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public JContainerObject? GetContentPane()
	{
		IEnvironment env = this.Environment;
		using JClassObject jClass = JClassObject.GetClass<JRootPaneContainerObject>(env);
		return JRootPaneContainerObject.getContentPaneDef.Invoke(this.Object, jClass);
	}

	static JRootPaneContainerObject IInterfaceType<JRootPaneContainerObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}