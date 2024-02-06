namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public abstract record ObjectProxy : IObject
{
	public abstract CString ObjectClassName { get; }
	public abstract CString ObjectSignature { get; }

	void IObject.CopyTo(Span<Byte> span, ref Int32 offset)
	{
		Byte[] bytes = new Byte[span.Length];
		IMutableReference<Int32> wOffset = IMutableReference.Create(offset);
		span.CopyTo(bytes);
		this.CopyTo(bytes, wOffset);
		bytes.CopyTo(span);
		offset = wOffset.Value;
	}
	void IObject.CopyTo(Span<JValue> span, Int32 index)
	{
		ProxyValue[] values = new ProxyValue[span.Length];
		span.AsBytes().CopyTo(values.AsSpan().AsBytes());
		this.CopyTo(values, index);
		values.AsSpan().AsBytes().CopyTo(span.AsBytes());
	}

	public abstract void CopyTo(Byte[] bytes, IMutableReference<Int32> offsetRef);
	public abstract void CopyTo(ProxyValue[] values, Int32 index);
}