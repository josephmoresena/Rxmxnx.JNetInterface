using Rxmxnx.JNetInterface.Awt;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.Swing;

public class JImageIconObject : JLocalObject, IClassType<JImageIconObject>, IInterfaceObject<JIconObject>
{
	private static readonly JClassTypeMetadata<JImageIconObject> typeMetadata = TypeMetadataBuilder<JImageIconObject>
		.Create("javax/swing/ImageIcon"u8).Implements<JIconObject>().Build();
	private static readonly IndeterminateCall byteArrayConstructor =
		IndeterminateCall.CreateConstructorDefinition([JArrayObject<JByte>.Metadata.ArgumentMetadata,]);
	private static readonly JFunctionDefinition<JImageObject>.Parameterless getImageDef = new("getImage"u8);

	static JClassTypeMetadata<JImageIconObject> IClassType<JImageIconObject>.Metadata => JImageIconObject.typeMetadata;

	protected JImageIconObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JImageIconObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JImageIconObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public JImageObject GetImage()
	{
		IEnvironment env = this.Environment;
		using JClassObject jClass = JClassObject.GetClass<JImageIconObject>(env);
		return JImageIconObject.getImageDef.Invoke(this, jClass)!;
	}

	public static JImageIconObject? Create(IEnvironment env, ReadOnlySpan<Byte> data)
	{
		if (data.IsEmpty) return default;

		using JArrayObject<JByte> jBytes = JArrayObject<JByte>.Create(env, data.Length);
		jBytes.Set(data.AsValues<Byte, JByte>());

		using JClassObject jClass = JClassObject.GetClass<JImageIconObject>(env);
		return JImageIconObject.byteArrayConstructor.NewCall<JImageIconObject>(env, [jBytes,]);
	}
	static JImageIconObject IClassType<JImageIconObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JImageIconObject IClassType<JImageIconObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JImageIconObject IClassType<JImageIconObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}