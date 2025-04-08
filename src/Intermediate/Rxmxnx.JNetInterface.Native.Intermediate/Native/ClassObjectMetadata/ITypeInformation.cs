namespace Rxmxnx.JNetInterface.Native;

public partial record ClassObjectMetadata : ITypeInformation
{
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	CString ITypeInformation.ClassName => this.Name;
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	CString ITypeInformation.Signature => this.ClassSignature;
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	JTypeKind ITypeInformation.Kind => ClassObjectMetadata.GetKind(this) ?? JTypeKind.Undefined;
}