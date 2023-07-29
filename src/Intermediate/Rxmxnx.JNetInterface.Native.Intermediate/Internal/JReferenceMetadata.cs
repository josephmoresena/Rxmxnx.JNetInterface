namespace Rxmxnx.JNetInterface.Internal;

internal record JReferenceMetadata<TObject> : JReferenceMetadata
	where TObject : JLocalObject, IDataType<TObject>
{
	public override CString ClassName => TObject.ClassName;
	public override CString ClassSignature => TObject.Signature;

	internal JReferenceMetadata(Type type, CString arraySignature, JReferenceMetadata? baseMetadata) : base(
		type, arraySignature, baseMetadata) { }
}