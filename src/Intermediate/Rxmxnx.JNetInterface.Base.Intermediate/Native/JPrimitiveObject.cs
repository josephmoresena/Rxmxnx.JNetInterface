namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// Java object representing a java primitive value.
/// </summary>
internal partial class JPrimitiveObject : JObject
{
	/// <inheritdoc cref="JPrimitiveObject.ObjectClassName"/>
	private readonly CString _className;
	/// <inheritdoc cref="JPrimitiveObject.ObjectSignature"/>
	private readonly CString _signature;
	/// <inheritdoc cref="JDataTypeMetadata.SizeOf"/>
	private readonly Int32 _sizeOf;

	/// <inheritdoc cref="IObject.ObjectClassName"/>
	public override CString ObjectClassName => this._className;
	/// <inheritdoc cref="IObject.ObjectSignature"/>
	public override CString ObjectSignature => this._signature;

	/// <inheritdoc/>
	protected JPrimitiveObject(JValue value, Int32 sizeOf, CString signature, CString className) : base(value)
	{
		this._sizeOf = sizeOf;
		this._signature = signature;
		this._className = className;
	}

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected internal override void CopyTo(Span<Byte> span, ref Int32 offset)
	{
		NativeUtilities.AsBytes(this.As<JValue>()).CopyTo(span[offset..]);
		offset += this._sizeOf;
	}
}

/// <summary>
/// Java object representing a java primitive value.
/// </summary>
/// <typeparam name="TPrimitive">Type of java primitive value.</typeparam>
internal sealed class JPrimitiveObject<TPrimitive> : JPrimitiveObject, IPrimitiveType, IWrapper<TPrimitive>,
	IEquatable<JPrimitiveObject<TPrimitive>>
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IEquatable<TPrimitive>
{
	static Type IDataType.FamilyType => typeof(TPrimitive);
	static JDataTypeMetadata IDataType.Metadata => IPrimitiveType.GetMetadata<TPrimitive>();
	static JNativeType IPrimitiveType.JniType => IPrimitiveType.GetMetadata<TPrimitive>().NativeType;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	public JPrimitiveObject(TPrimitive value) : base(JValue.Create(value), NativeUtilities.SizeOf<TPrimitive>(),
	                                                 IPrimitiveType.GetMetadata<TPrimitive>().Signature,
	                                                 IPrimitiveType.GetMetadata<TPrimitive>().ClassName) { }
	/// <inheritdoc cref="IEquatable{TPrimitive}"/>
	public Boolean Equals(JPrimitiveObject<TPrimitive>? other) => other is not null && this.Value.Equals(other.Value);

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

	/// <summary>
	/// Internal primitive value.
	/// </summary>
	public new TPrimitive Value => this.As<TPrimitive>();
	/// <inheritdoc/>
	public Boolean Equals(TPrimitive other) => this.Value.Equals(other);

	/// <inheritdoc cref="IComparable.CompareTo"/>
	public Int32 CompareTo(Object? obj) => this.Value.CompareTo(obj);

	/// <inheritdoc/>
	public override Boolean Equals(JObject? other)
		=> other is JPrimitiveObject<TPrimitive> jPrimitive && this.Equals(jPrimitive);
	/// <inheritdoc/>
	public override Boolean Equals(Object? obj)
		=> obj is JPrimitiveObject<TPrimitive> jPrimitive && this.Equals(jPrimitive);
	/// <inheritdoc/>
	public override Int32 GetHashCode() => this.Value.GetHashCode();
}