namespace Rxmxnx.JNetInterface.Tests;

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