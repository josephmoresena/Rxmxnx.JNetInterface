using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Awt;

public class JWindowObject : JContainerObject, IClassType<JWindowObject>
{
	private static readonly IndeterminateCall setLocationRelativeToDef =
		IndeterminateCall.CreateMethodDefinition("setLocationRelativeTo"u8,
		                                         [JArgumentMetadata.Get<JComponentObject>(),]);
	private static readonly JClassTypeMetadata<JWindowObject> typeMetadata =
		TypeMetadataBuilder<JContainerObject>.Create<JWindowObject>("java/awt/Window"u8).Build();

	static JClassTypeMetadata<JWindowObject> IClassType<JWindowObject>.Metadata => JWindowObject.typeMetadata;

	protected JWindowObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JWindowObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JWindowObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public void SetRelativeTo(JComponentObject? comp)
	{
		IEnvironment env = this.Environment;
		using JClassObject jClass = JClassObject.GetClass<JWindowObject>(env);
		JWindowObject.setLocationRelativeToDef.MethodCall(this, jClass, false, [comp,]);
	}
	static JWindowObject IClassType<JWindowObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JWindowObject IClassType<JWindowObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JWindowObject IClassType<JWindowObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}