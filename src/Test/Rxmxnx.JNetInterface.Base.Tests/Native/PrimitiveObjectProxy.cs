namespace Rxmxnx.JNetInterface.Tests.Native;

[ExcludeFromCodeCoverage]
internal sealed class PrimitiveObjectProxy<T>(CString className, CString signature, T value)
	: JPrimitiveObject.Generic<T>(value) where T : unmanaged, IEquatable<T>, IComparable, IConvertible, IComparable<T>
{
	public override CString ObjectClassName { get; } = className;
	public override CString ObjectSignature { get; } = signature;

	public override String ToTraceText() => this.ToString()!;
}