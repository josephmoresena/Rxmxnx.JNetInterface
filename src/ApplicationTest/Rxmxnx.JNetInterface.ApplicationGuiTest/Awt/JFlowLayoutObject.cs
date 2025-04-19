using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Awt;

public class JFlowLayoutObject : JLocalObject, IClassType<JFlowLayoutObject>, IInterfaceObject<JLayoutManagerObject>
{
	private static readonly JClassTypeMetadata<JFlowLayoutObject> typeMetadata = TypeMetadataBuilder<JFlowLayoutObject>
		.Create("java/awt/FlowLayout"u8).Implements<JLayoutManagerObject>().Build();
	private static readonly JConstructorDefinition.Parameterless constructor = new();
	static JClassTypeMetadata<JFlowLayoutObject> IClassType<JFlowLayoutObject>.Metadata
		=> JFlowLayoutObject.typeMetadata;

	protected JFlowLayoutObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JFlowLayoutObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JFlowLayoutObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public static JFlowLayoutObject Create(IEnvironment env)
	{
		using JClassObject jClass = JClassObject.GetClass<JFlowLayoutObject>(env);
		return JFlowLayoutObject.constructor.New<JFlowLayoutObject>(env);
	}
	static JFlowLayoutObject IClassType<JFlowLayoutObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JFlowLayoutObject IClassType<JFlowLayoutObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JFlowLayoutObject IClassType<JFlowLayoutObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}