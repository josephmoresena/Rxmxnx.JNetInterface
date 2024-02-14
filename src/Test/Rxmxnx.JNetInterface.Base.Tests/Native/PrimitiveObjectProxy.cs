namespace Rxmxnx.JNetInterface.Tests.Native;

[ExcludeFromCodeCoverage]
internal sealed class PrimitiveObjectProxy<T>(CString className, CString signature, T value)
	: JPrimitiveObject.Generic<T>(value) where T : unmanaged, IEquatable<T>, IComparable, IConvertible
{
	public override CString ObjectClassName { get; } = className;
	public override CString ObjectSignature { get; } = signature;
}