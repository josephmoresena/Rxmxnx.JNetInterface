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
	/// Signature of function result data type.
	/// </summary>
	public ReadOnlySpan<Byte> Signature { get; }
	/// <summary>
	/// Resulting boolean value.
	/// </summary>
	public JBoolean BooleanValue
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !this._primitive.IsDefault || IndeterminateHelper.GetBooleanValue(this.Object);
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
		get => IndeterminateHelper.GetCharValue(this.Object) ?? this.GetNumericPrimitive<JChar>();
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
		=> IndeterminateHelper.GetNumericValue<TNumber>(this.Object) ?? this.GetNumericPrimitive<TNumber>();
	/// <summary>
	/// Retrieves numeric primitive value.
	/// </summary>
	/// <typeparam name="TNumber">Destination number type.</typeparam>
	/// <returns>A <typeparamref name="TNumber"/></returns>
	private TNumber GetNumericPrimitive<TNumber>()
		where TNumber : unmanaged, IPrimitiveNumericType<TNumber>, IPrimitiveType<TNumber>, IBinaryNumber<TNumber>
	{
		if (this.Signature.Length == 0) return default;

		Byte signature = IPrimitiveType.GetMetadata<TNumber>().Signature[0];
		switch (this.Signature[0])
		{
			case CommonNames.BooleanSignatureChar when signature != CommonNames.ByteSignatureChar:
			case CommonNames.ByteSignatureChar when signature != CommonNames.ByteSignatureChar:
				return this.GetNumericPrimitive<JByte, TNumber>();
			case CommonNames.CharSignatureChar when signature != CommonNames.CharSignatureChar:
				return this.GetNumericPrimitive<JChar, TNumber>();
			case CommonNames.DoubleSignatureChar when signature != CommonNames.DoubleSignatureChar:
				return this.GetNumericPrimitive<JDouble, TNumber>();
			case CommonNames.FloatSignatureChar when signature != CommonNames.FloatSignatureChar:
				return this.GetNumericPrimitive<JFloat, TNumber>();
			case CommonNames.IntSignatureChar when signature != CommonNames.IntSignatureChar:
				return this.GetNumericPrimitive<JInt, TNumber>();
			case CommonNames.LongSignatureChar when signature != CommonNames.LongSignatureChar:
				return this.GetNumericPrimitive<JLong, TNumber>();
			case CommonNames.ShortSignatureChar when signature != CommonNames.ShortSignatureChar:
				return this.GetNumericPrimitive<JShort, TNumber>();
			default:
				ref JValue.PrimitiveValue valueRef = ref Unsafe.AsRef(in this._primitive);
				return Unsafe.As<JValue.PrimitiveValue, TNumber>(ref valueRef);
		}
	}
	/// <summary>
	/// Retrieves numeric primitive value.
	/// </summary>
	/// <typeparam name="TSource">Source primitive type.</typeparam>
	/// <typeparam name="TPrimitive">Destination primitive type.</typeparam>
	/// <returns>A <typeparamref name="TPrimitive"/></returns>
	private TPrimitive GetNumericPrimitive<TSource, TPrimitive>()
		where TSource : unmanaged, IPrimitiveNumericType<TSource>, IPrimitiveType<TSource>, IBinaryNumber<TSource>
		where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive>, IPrimitiveType<TPrimitive>,
		IBinaryNumber<TPrimitive>
	{
		ref JValue.PrimitiveValue valueRef = ref Unsafe.AsRef(in this._primitive);
		ref TSource result = ref Unsafe.As<JValue.PrimitiveValue, TSource>(ref valueRef);
		// ReSharper disable once RedundantCast
		return (TPrimitive)(Double)result;
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