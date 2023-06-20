namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes an object that represents a JNI primitive value.
/// </summary>
public interface IPrimitive : IObject, IDataType, IComparable, IConvertible
{
	/// <summary>
	/// JNI signature for an array of current primitive type.
	/// </summary>
	static abstract CString ArraySignature { get; }
    /// <summary>
    /// Primitive metadata.
    /// </summary>
    static new abstract JPrimitiveMetadata PrimitiveMetadata { get; }
}

/// <summary>
/// This interface exposes an object that represents a JNI primitive value.
/// </summary>
/// <typeparam name="TValue">Type of the .NET equivalent structure.</typeparam>
public interface IPrimitive<TValue> : IPrimitive, IWrapper<TValue>, IComparable<TValue>
	where TValue : unmanaged, IComparable<TValue>, IEquatable<TValue>, IConvertible
{
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
	Object IConvertible.ToType(Type conversionType, IFormatProvider? provider) => this.Value.ToType(conversionType, provider);
	UInt16 IConvertible.ToUInt16(IFormatProvider? provider) => this.Value.ToUInt16(provider);
	UInt32 IConvertible.ToUInt32(IFormatProvider? provider) => this.Value.ToUInt32(provider);
	UInt64 IConvertible.ToUInt64(IFormatProvider? provider) => this.Value.ToUInt64(provider);
}

/// <summary>
/// This interface exposes an object that represents a JNI primitive value.
/// </summary>
/// <typeparam name="TSelf">Type of JNI primitive structure.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent structure.</typeparam>
public interface IPrimitive<TSelf, TValue> : IPrimitive<TValue>, IDataType<TSelf>
	where TSelf : unmanaged, IPrimitive<TSelf, TValue>, IComparable<TSelf>, IEquatable<TSelf>
	where TValue : unmanaged, IComparable<TValue>, IEquatable<TValue>, IConvertible
{
	Int32 IComparable<TValue>.CompareTo(TValue other) => this.Value.CompareTo(other);
	Boolean IEquatable<TValue>.Equals(TValue other) => this.Value.Equals(other);

	void IObject.CopyTo(Span<Byte> span, ref Int32 offset)
	{
		ref TValue refValue = ref Unsafe.AsRef(this.Value);
		refValue.AsBytes().CopyTo(span[offset..]);
		offset += TSelf.PrimitiveMetadata.SizeOf;
	}
	void IObject.CopyTo(Span<JValue> span, Int32 index) => JValue.Create(this.Value);
}