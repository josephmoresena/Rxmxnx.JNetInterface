namespace Rxmxnx.JNetInterface.Tests;

internal sealed class PrimitiveProxy(JPrimitiveObject primitive) : JPrimitiveObject
{
	public override CString ObjectClassName => primitive.ObjectClassName;
	public override CString ObjectSignature => primitive.ObjectSignature;
	protected override Int32 SizeOf => this.AsSpan().Length;
	public override Boolean Equals(JObject? other) => primitive.Equals(other);
	private protected override void CopyTo(Span<Byte> span, ref Int32 offset)
		=> (primitive as IObject).CopyTo(span, ref offset);
	private protected override void CopyTo(Span<JValue> span, Int32 index)
		=> (primitive as IObject).CopyTo(span, index);
	private protected override ReadOnlySpan<Byte> AsSpan() => JPrimitiveObject.GetSpan(primitive);
	public override Byte ToByte() => this.AsSpan()[0];
}

[ExcludeFromCodeCoverage]
public abstract class PrimitiveProxy<T> : IPrimitiveType, IWrapper<T> where T : unmanaged
{
	public abstract CString ObjectClassName { get; }
	public abstract CString ObjectSignature { get; }

	public abstract Int32 CompareTo(Object? obj);
	public abstract TypeCode GetTypeCode();
	public abstract Boolean ToBoolean(IFormatProvider? provider);
	public abstract Byte ToByte(IFormatProvider? provider);
	public abstract Char ToChar(IFormatProvider? provider);
	public abstract DateTime ToDateTime(IFormatProvider? provider);
	public abstract Decimal ToDecimal(IFormatProvider? provider);
	public abstract Double ToDouble(IFormatProvider? provider);
	public abstract Int16 ToInt16(IFormatProvider? provider);
	public abstract Int32 ToInt32(IFormatProvider? provider);
	public abstract Int64 ToInt64(IFormatProvider? provider);
	public abstract SByte ToSByte(IFormatProvider? provider);
	public abstract Single ToSingle(IFormatProvider? provider);
	public abstract String ToString(IFormatProvider? provider);
	public abstract Object ToType(Type conversionType, IFormatProvider? provider);
	public abstract UInt16 ToUInt16(IFormatProvider? provider);
	public abstract UInt32 ToUInt32(IFormatProvider? provider);
	public abstract UInt64 ToUInt64(IFormatProvider? provider);

	void IObject.CopyTo(Span<Byte> span, ref Int32 offset) { throw new NotImplementedException(); }
	void IObject.CopyTo(Span<JValue> span, Int32 index) { throw new NotImplementedException(); }
	public abstract T Value { get; }
}