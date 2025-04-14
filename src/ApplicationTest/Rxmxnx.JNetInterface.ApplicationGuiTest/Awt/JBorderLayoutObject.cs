using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Awt;

public class JBorderLayoutObject : JLocalObject, IClassType<JBorderLayoutObject>, IInterfaceObject<JLayoutManagerObject>
{
	private static readonly JClassTypeMetadata<JBorderLayoutObject> typeMetadata =
		TypeMetadataBuilder<JBorderLayoutObject>.Create("java/awt/BorderLayout"u8).Implements<JLayoutManagerObject>()
		                                        .Build();
	private static readonly JConstructorDefinition.Parameterless constructor = new();
	static JClassTypeMetadata<JBorderLayoutObject> IClassType<JBorderLayoutObject>.Metadata
		=> JBorderLayoutObject.typeMetadata;

	protected JBorderLayoutObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JBorderLayoutObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JBorderLayoutObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public static JBorderLayoutObject Create(IEnvironment env)
	{
		using JClassObject jClass = JClassObject.GetClass<JBorderLayoutObject>(env);
		return JBorderLayoutObject.constructor.New<JBorderLayoutObject>(env);
	}
	static JBorderLayoutObject IClassType<JBorderLayoutObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JBorderLayoutObject IClassType<JBorderLayoutObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JBorderLayoutObject IClassType<JBorderLayoutObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}