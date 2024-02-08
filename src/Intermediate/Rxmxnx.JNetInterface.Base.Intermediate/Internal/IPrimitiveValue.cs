namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that represents a java primitive wrapper.
/// </summary>
/// <typeparam name="TValue">Type of the .NET equivalent structure.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IPrimitiveValue<TValue> : IObject, IComparable, IConvertible, IWrapper<TValue>, IComparable<TValue>
	where TValue : unmanaged, IComparable, IConvertible, IEquatable<TValue>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	Int32 IComparable.CompareTo(Object? obj) => this.Value.CompareTo(obj);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	Int32 IComparable<TValue>.CompareTo(TValue other)
		=> (this.Value as IComparable<TValue>)?.CompareTo(other) ?? this.Value.CompareTo(other);
	TypeCode IConvertible.GetTypeCode() => this.Value.GetTypeCode();
	Boolean IConvertible.ToBoolean(IFormatProvider? provider) => this.Value.ToBoolean(provider);
	Byte IConvertible.ToByte(IFormatProvider? provider) => this.Value.ToByte(provider);
	Char IConvertible.ToChar(IFormatProvider? provider) => this.Value.ToChar(provider);
	DateTime IConvertible.ToDateTime(IFormatProvider? provider) => this.Value.ToDateTime(provider);
	Decimal IConvertible.ToDecimal(IFormatProvider? provider) => this.Value.ToDecimal(provider);
	Double IConvertible.ToDouble(IFormatProvider? provider) => this.Value.ToDouble(provider);
	Int16 IConvertible.ToInt16(IFormatProvider? provider) => this.Value.ToInt16(provider);
	Int32 IConvertible.ToInt32(IFormatProvider? provider) => this.Value.ToInt32(provider);
	Int64 IConvertible.ToInt64(IFormatProvider? provider) => this.Value.ToInt64(provider);
	SByte IConvertible.ToSByte(IFormatProvider? provider) => this.Value.ToSByte(provider);
	Single IConvertible.ToSingle(IFormatProvider? provider) => this.Value.ToSingle(provider);
	String IConvertible.ToString(IFormatProvider? provider) => this.Value.ToString(provider);
	Object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
		=> this.Value.ToType(conversionType, provider);
	UInt16 IConvertible.ToUInt16(IFormatProvider? provider) => this.Value.ToUInt16(provider);
	UInt32 IConvertible.ToUInt32(IFormatProvider? provider) => this.Value.ToUInt32(provider);
	UInt64 IConvertible.ToUInt64(IFormatProvider? provider) => this.Value.ToUInt64(provider);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	Boolean IEquatable<TValue>.Equals(TValue other) => this.Value.Equals(other);
}