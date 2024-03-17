namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class JFakeObject<TObject>(TObject obj) : IReferenceType<TObject>
	where TObject : JLocalObject, IClassType<TObject>
{
	CString IObject.ObjectClassName => obj.ObjectClassName;
	CString IObject.ObjectSignature => obj.ObjectSignature;
	void IObject.CopyTo(Span<Byte> span, ref Int32 offset) => obj.CopyTo(span, ref offset);
	void IObject.CopyTo(Span<JValue> span, Int32 index) => obj.CopyTo(span, index);

	public void Dispose() => obj.Dispose();

	public static TObject Clone(TObject obj) => TObject.Create(obj);
	static TObject IReferenceType<TObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> TObject.Create(initializer);
}