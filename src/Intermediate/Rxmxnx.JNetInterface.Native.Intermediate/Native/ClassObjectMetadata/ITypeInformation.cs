namespace Rxmxnx.JNetInterface.Native;

public partial record ClassObjectMetadata : ITypeInformation
{
	CString ITypeInformation.ClassName => this.Name;
	CString ITypeInformation.Signature => this.ClassSignature;
	JTypeKind ITypeInformation.Kind => ClassObjectMetadata.GetKind(this) ?? JTypeKind.Undefined;
}