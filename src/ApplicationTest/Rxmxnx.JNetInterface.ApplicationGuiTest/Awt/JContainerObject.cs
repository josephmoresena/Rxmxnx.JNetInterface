using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Awt;

public class JContainerObject : JComponentObject, IClassType<JContainerObject>
{
	private static readonly JClassTypeMetadata<JContainerObject> typeMetadata =
		TypeMetadataBuilder<JComponentObject>.Create<JContainerObject>("java/awt/Container"u8).Build();
	private static readonly IndeterminateCall setLayoutDefinition =
		IndeterminateCall.CreateMethodDefinition("setLayout"u8, [JArgumentMetadata.Get<JLayoutManagerObject>(),]);
	private static readonly IndeterminateCall addDefinition =
		IndeterminateCall.CreateMethodDefinition(
			"add"u8, [JArgumentMetadata.Get<JComponentObject>(), JArgumentMetadata.Get<JLocalObject>(),]);

	static JClassTypeMetadata<JContainerObject> IClassType<JContainerObject>.Metadata => JContainerObject.typeMetadata;

	protected JContainerObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JContainerObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JContainerObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public void SetLayout(IInterfaceObject<JLayoutManagerObject> layout)
		=> JContainerObject.setLayoutDefinition.MethodCall(this, [layout,]);
	public void Add(JComponentObject component, JLocalObject constraints)
		=> JContainerObject.addDefinition.MethodCall(this, [component, constraints,]);

	static JContainerObject IClassType<JContainerObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JContainerObject IClassType<JContainerObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JContainerObject IClassType<JContainerObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}