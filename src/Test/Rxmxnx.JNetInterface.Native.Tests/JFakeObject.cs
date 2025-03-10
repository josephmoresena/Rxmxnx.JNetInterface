namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class JFakeObject<TObject>(TObject obj) : IReferenceType<TObject>
	where TObject : JLocalObject, IClassType<TObject>
{
	public static JTypeKind Kind => TObject.Kind;
	public static JDataTypeMetadata Metadata => TObject.Metadata;
	CString IObject.ObjectClassName => obj.ObjectClassName;
	CString IObject.ObjectSignature => obj.ObjectSignature;
	void IObject.CopyTo(Span<Byte> span, ref Int32 offset) => obj.CopyTo(span, ref offset);
	void IObject.CopyTo(Span<JValue> span, Int32 index) => obj.CopyTo(span, index);

	public void Dispose() => obj.Dispose();
	IVirtualMachine ILocalObject.VirtualMachine => obj.VirtualMachine;
	Boolean ILocalObject.IsProxy => obj.IsProxy;
	ObjectLifetime ILocalObject.Lifetime => obj.Lifetime;
	JObjectLocalRef ILocalObject.LocalReference => obj.LocalReference;
	ObjectMetadata ILocalObject.CreateMetadata() => ILocalObject.CreateMetadata(obj);
	void ILocalObject.ProcessMetadata(ObjectMetadata instanceMetadata)
		=> ILocalObject.ProcessMetadata(obj, instanceMetadata);
	TReference ILocalObject.CastTo<TReference>() => obj.CastTo<TReference>();

	public static TObject Clone(TObject obj) => TObject.Create(obj);
	static TObject IReferenceType<TObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> TObject.Create(initializer);
}