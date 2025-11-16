namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This structure represents a result of an indeterminate java function.
/// </summary>
public readonly ref struct IndeterminateResult
{
	/// <summary>
	/// Empty result.
	/// </summary>
	public static IndeterminateResult Empty => default;

	/// <summary>
	/// Internal value to hold primitive value.
	/// </summary>
	private readonly JValue.PrimitiveValue _primitive;
	/// <summary>
	/// Internal reference to a <see cref="PrimitiveWrapperObjectMetadata"/> instance.
	/// </summary>
	private readonly ref PrimitiveWrapperObjectMetadata? _objectMetadata;

	/// <summary>
	/// Signature of function result data type.
	/// </summary>
	public ReadOnlySpan<Byte> Signature { get; }
	/// <summary>
	/// Resulting boolean value.
	/// </summary>
	public JBoolean BooleanValue
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !this._primitive.IsDefault || IndeterminateHelper.GetBooleanValue(this.Object, ref this._objectMetadata);
	}
	/// <summary>
	/// Resulting byte value.
	/// </summary>
	public JByte ByteValue
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => this.GetNumberValue<JByte>();
	}
	/// <summary>
	/// Resulting char value.
	/// </summary>
	public JChar CharValue
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
			=> IndeterminateHelper.GetCharValue(this.Object, ref this._objectMetadata) ??
				this.GetPrimitiveValue<JChar>();
	}
	/// <summary>
	/// Resulting double value.
	/// </summary>
	public JDouble DoubleValue
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => this.GetNumberValue<JDouble>();
	}

	/// <summary>
	/// Resulting float value.
	/// </summary>
	public JFloat FloatValue
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => this.GetNumberValue<JFloat>();
	}
	/// <summary>
	/// Resulting int value.
	/// </summary>
	public JInt IntValue
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => this.GetNumberValue<JInt>();
	}
	/// <summary>
	/// Resulting long value.
	/// </summary>
	public JLong LongValue
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => this.GetNumberValue<JLong>();
	}
	/// <summary>
	/// Resulting short value.
	/// </summary>
	public JShort ShortValue
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => this.GetNumberValue<JShort>();
	}
	/// <summary>
	/// Resulting reference object.
	/// </summary>
	public JLocalObject? Object { get; }

	/// <summary>
	/// Object constructor.
	/// </summary>
	/// <param name="jObject">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="signature">Definition signature.</param>
	internal IndeterminateResult(JLocalObject? jObject, ReadOnlySpan<Byte> signature)
	{
		this._primitive = default;
		this._objectMetadata = ref IMutableReference.CreateObject<PrimitiveWrapperObjectMetadata>(default!).Reference!;
		this.Object = jObject;
		this.Signature = signature;
	}
	/// <summary>
	/// Primitive constructor.
	/// </summary>
	/// <param name="primitive">Primitive value as <see cref="Int64"/>.</param>
	/// <param name="signature">Definition signature.</param>
	internal IndeterminateResult(JValue.PrimitiveValue primitive, ReadOnlySpan<Byte> signature)
	{
		this._primitive = primitive;
		this._objectMetadata = ref Unsafe.NullRef<PrimitiveWrapperObjectMetadata?>();
		this.Object = default;
		this.Signature = signature;
	}

	/// <summary>
	/// Copies the sequence of bytes of <paramref name="primitiveSignature"/> from current instance to
	/// <paramref name="bytes"/>.
	/// </summary>
	/// <param name="primitiveSignature">Required primitive type JNI signature.</param>
	/// <param name="bytes">Destination buffer.</param>
	public void CopyPrimitiveValue(Byte primitiveSignature, Span<Byte> bytes)
	{
		switch (primitiveSignature)
		{
			case CommonNames.BooleanSignatureChar:
				IndeterminateResult.CopyPrimitiveValue(this.BooleanValue, bytes);
				break;
			case CommonNames.ByteSignatureChar:
				IndeterminateResult.CopyPrimitiveValue(this.ByteValue, bytes);
				break;
			case CommonNames.CharSignatureChar:
				IndeterminateResult.CopyPrimitiveValue(this.CharValue, bytes);
				break;
			case CommonNames.DoubleSignatureChar:
				IndeterminateResult.CopyPrimitiveValue(this.DoubleValue, bytes);
				break;
			case CommonNames.FloatSignatureChar:
				IndeterminateResult.CopyPrimitiveValue(this.FloatValue, bytes);
				break;
			case CommonNames.IntSignatureChar:
				IndeterminateResult.CopyPrimitiveValue(this.IntValue, bytes);
				break;
			case CommonNames.LongSignatureChar:
				IndeterminateResult.CopyPrimitiveValue(this.LongValue, bytes);
				break;
			case CommonNames.ShortSignatureChar:
				IndeterminateResult.CopyPrimitiveValue(this.ShortValue, bytes);
				break;
		}
	}

	/// <summary>
	/// Retrieves numeric value.
	/// </summary>
	/// <typeparam name="TNumber">Destination number type.</typeparam>
	/// <returns>A <typeparamref name="TNumber"/></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private TNumber GetNumberValue<TNumber>()
		where TNumber : unmanaged, IPrimitiveType<TNumber>, ISignedNumber<TNumber>, IPrimitiveNumericType<TNumber>,
		IBinaryNumber<TNumber>
		=> IndeterminateHelper.GetNumericValue<TNumber>(this.Object, ref this._objectMetadata) ??
			this.GetPrimitiveValue<TNumber>();
	/// <summary>
	/// Retrieves numeric primitive value.
	/// </summary>
	/// <typeparam name="TPrimitive">Destination number type.</typeparam>
	/// <returns>A <typeparamref name="TPrimitive"/></returns>
	private TPrimitive GetPrimitiveValue<TPrimitive>() where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		if (this.Signature.Length == 0) return default;

		Byte signature = IPrimitiveType.GetMetadata<TPrimitive>().Signature[0];
		ref JValue.PrimitiveValue valueRef = ref Unsafe.AsRef(in this._primitive);
		return this.Signature[0] switch
		{
			CommonNames.BooleanSignatureChar when signature != CommonNames.BooleanSignatureChar =>
				TPrimitive.CreateFrom(Unsafe.As<JValue.PrimitiveValue, JBoolean>(ref valueRef)),
			CommonNames.ByteSignatureChar when signature != CommonNames.ByteSignatureChar => TPrimitive.CreateFrom(
				Unsafe.As<JValue.PrimitiveValue, JByte>(ref valueRef)),
			CommonNames.CharSignatureChar when signature != CommonNames.CharSignatureChar => TPrimitive.CreateFrom(
				Unsafe.As<JValue.PrimitiveValue, JChar>(ref valueRef)),
			CommonNames.DoubleSignatureChar when signature != CommonNames.DoubleSignatureChar => TPrimitive.CreateFrom(
				Unsafe.As<JValue.PrimitiveValue, JDouble>(ref valueRef)),
			CommonNames.FloatSignatureChar when signature != CommonNames.FloatSignatureChar => TPrimitive.CreateFrom(
				Unsafe.As<JValue.PrimitiveValue, JFloat>(ref valueRef)),
			CommonNames.IntSignatureChar when signature != CommonNames.IntSignatureChar => TPrimitive.CreateFrom(
				Unsafe.As<JValue.PrimitiveValue, JInt>(ref valueRef)),
			CommonNames.LongSignatureChar when signature != CommonNames.LongSignatureChar => TPrimitive.CreateFrom(
				Unsafe.As<JValue.PrimitiveValue, JLong>(ref valueRef)),
			CommonNames.ShortSignatureChar when signature != CommonNames.ShortSignatureChar => TPrimitive.CreateFrom(
				Unsafe.As<JValue.PrimitiveValue, JShort>(ref valueRef)),
			_ => Unsafe.As<JValue.PrimitiveValue, TPrimitive>(ref valueRef),
		};
	}
	/// <summary>
	/// Copies the sequence of bytes of <paramref name="value"/> to <paramref name="bytes"/>.
	/// </summary>
	/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> instance.</typeparam>
	/// <param name="value">A <typeparamref name="TPrimitive"/> value.</param>
	/// <param name="bytes">Destination binary span.</param>
	private static void CopyPrimitiveValue<TPrimitive>(TPrimitive value, Span<Byte> bytes)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> value.CopyTo(bytes);
}