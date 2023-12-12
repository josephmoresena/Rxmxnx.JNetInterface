namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// Java object representing a java primitive value.
/// </summary>
internal abstract partial class JPrimitiveObject : JObject
{
	/// <summary>
	/// Size of current type in bytes.
	/// </summary>
	public abstract Int32 SizeOf { get; }

	/// <summary>
	/// Constructor.
	/// </summary>
	protected JPrimitiveObject() { }

	/// <summary>
	/// Interprets current instance as byte.
	/// </summary>
	/// <returns>Current instance as <see cref="Byte"/> value.</returns>
	public abstract Byte ToByte();

	/// <summary>
	/// Retrieves a <typeparamref name="TPrimitive"/> value from current instance.
	/// </summary>
	/// <typeparam name="TPrimitive"><see cref="IPrimitiveType"/> type.</typeparam>
	/// <typeparam name="TValue"><see cref="ValueType"/> type.</typeparam>
	/// <returns>
	/// The equivalent <typeparamref name="TPrimitive"/> value to current instance.
	/// </returns>
	/// <exception cref="InvalidCastException"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TPrimitive AsPrimitive<TPrimitive, TValue>()
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IWrapper<TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>
		=> this is IWrapper<TPrimitive> pw ? pw.Value : this.AsValue<TPrimitive>();

	/// <summary>
	/// Retrieves a <typeparamref name="TValue"/> value from current instance.
	/// </summary>
	/// <typeparam name="TValue"><see cref="ValueType"/> type.</typeparam>
	/// <returns>
	/// The equivalent <typeparamref name="TValue"/> value to current instance.
	/// </returns>
	/// <exception cref="InvalidCastException"/>
	private TValue AsValue<TValue>()
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
		=> this is IWrapper<TValue> vw ?
			vw.Value :
			ValidationUtilities.ThrowIfInvalidCast<TValue>(this as IConvertible);
}

/// <summary>
/// Java object representing a java primitive value.
/// </summary>
/// <typeparam name="TPrimitive">Type of java primitive value.</typeparam>
internal sealed class JPrimitiveObject<TPrimitive> : JPrimitiveObject.Generic<TPrimitive>, IPrimitiveType,
	IEquatable<JPrimitiveObject<TPrimitive>>
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IEquatable<TPrimitive>
{
	static Type IDataType.FamilyType => typeof(TPrimitive);
	static JDataTypeMetadata IDataType.Metadata => IPrimitiveType.GetMetadata<TPrimitive>();
	static JNativeType IPrimitiveType.JniType => IPrimitiveType.GetMetadata<TPrimitive>().NativeType;
	public override Int32 SizeOf => IPrimitiveType.GetMetadata<TPrimitive>().SizeOf;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	public JPrimitiveObject(TPrimitive value) : base(value) { }
	/// <inheritdoc cref="IEquatable{TPrimitive}"/>
	public Boolean Equals(JPrimitiveObject<TPrimitive>? other) => other is not null && this.Value.Equals(other.Value);

	public override CString ObjectClassName => IPrimitiveType.GetMetadata<TPrimitive>().ClassName;
	public override CString ObjectSignature => IPrimitiveType.GetMetadata<TPrimitive>().Signature;

	Int32 IComparable.CompareTo(Object? obj) => this.Value.CompareTo(obj);
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
	UInt64 IConvertible.ToUInt64(IFormatProvider? provider) => this.Value.ToUInt64(provider);
	UInt32 IConvertible.ToUInt32(IFormatProvider? provider) => this.Value.ToUInt32(provider);

	/// <inheritdoc cref="IComparable.CompareTo"/>
	public Int32 CompareTo(Object? obj) => this.Value.CompareTo(obj);

	/// <inheritdoc/>
	public override Boolean Equals(JObject? other)
		=> other is JPrimitiveObject<TPrimitive> jPrimitive && this.Equals(jPrimitive);
}