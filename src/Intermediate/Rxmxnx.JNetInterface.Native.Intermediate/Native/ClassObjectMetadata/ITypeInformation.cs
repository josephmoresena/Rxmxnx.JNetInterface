namespace Rxmxnx.JNetInterface.Native;

public partial record ClassObjectMetadata : ITypeInformation
{
	[ExcludeFromCodeCoverage]
	CString ITypeInformation.ClassName => this.Name;
	[ExcludeFromCodeCoverage]
	CString ITypeInformation.Signature => this.ClassSignature;
	[ExcludeFromCodeCoverage]
	JTypeKind ITypeInformation.Kind => ClassObjectMetadata.GetKind(this) ?? JTypeKind.Undefined;
}