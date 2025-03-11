namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public abstract class TypeInformationProxy : ITypeInformation
{
	public abstract CString ClassName { get; }
	public abstract CString Signature { get; }
	public abstract String Hash { get; }
	public abstract JTypeKind Kind { get; }
	public abstract Boolean? IsFinal { get; }
}