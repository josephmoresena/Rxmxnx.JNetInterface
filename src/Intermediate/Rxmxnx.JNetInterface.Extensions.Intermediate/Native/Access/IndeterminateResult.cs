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
		get
		{
			if (!this._primitive.IsDefault)
				return true;
			return this.Object switch
			{
				JBooleanObject jBoolean => jBoolean.Value,
				JCharacterObject jChar => jChar.Value != default,
				_ => this.Object is not null,
			};
		}
	}
	/// <summary>
	/// Resulting byte value.
	/// </summary>
	public JByte ByteValue
		=> this.Object switch
		{
			JBooleanObject jBoolean => jBoolean.Value.Value ? JByte.One : JByte.Zero,
			JCharacterObject jChar => (JByte)jChar.Value,
			JNumberObject jNumber => jNumber.GetValue<JByte>(),
			_ => this.GetNumericPrimitive<JByte>(),
		};
	/// <summary>
	/// Resulting char value.
	/// </summary>
	public JChar CharValue
		=> this.Object switch
		{
			JBooleanObject jBoolean => jBoolean.Value.Value ? JChar.One : JChar.Zero,
			JCharacterObject jChar => jChar.Value,
			JNumberObject jNumber => (JChar)jNumber.GetValue<JShort>(),
			_ => (JChar)this.GetNumericPrimitive<JShort>(),
		};
	/// <summary>
	/// Resulting double value.
	/// </summary>
	public JDouble DoubleValue
		=> this.Object switch
		{
			JBooleanObject jBoolean => jBoolean.Value.Value ? JDouble.One : JDouble.Zero,
			JCharacterObject jChar => (JDouble)jChar.Value,
			JNumberObject jNumber => jNumber.GetValue<JDouble>(),
			_ => this.GetNumericPrimitive<JDouble>(),
		};
	/// <summary>
	/// Resulting float value.
	/// </summary>
	public JFloat FloatValue
		=> this.Object switch
		{
			JBooleanObject jBoolean => jBoolean.Value.Value ? JFloat.One : JFloat.Zero,
			JCharacterObject jChar => (JFloat)jChar.Value,
			JNumberObject jNumber => jNumber.GetValue<JFloat>(),
			_ => this.GetNumericPrimitive<JFloat>(),
		};
	/// <summary>
	/// Resulting int value.
	/// </summary>
	public JInt IntValue
		=> this.Object switch
		{
			JBooleanObject jBoolean => jBoolean.Value.Value ? JInt.One : JInt.Zero,
			JCharacterObject jChar => (JInt)jChar.Value,
			JNumberObject jNumber => jNumber.GetValue<JInt>(),
			_ => this.GetNumericPrimitive<JInt>(),
		};
	/// <summary>
	/// Resulting long value.
	/// </summary>
	public JLong LongValue
		=> this.Object switch
		{
			JBooleanObject jBoolean => jBoolean.Value.Value ? JLong.One : JLong.Zero,
			JCharacterObject jChar => (JLong)jChar.Value,
			JNumberObject jNumber => jNumber.GetValue<JLong>(),
			_ => this.GetNumericPrimitive<JLong>(),
		};
	/// <summary>
	/// Resulting short value.
	/// </summary>
	public JShort ShortValue
		=> this.Object switch
		{
			JBooleanObject jBoolean => jBoolean.Value.Value ? JShort.One : JShort.Zero,
			JCharacterObject jChar => (JShort)jChar.Value,
			JNumberObject jNumber => jNumber.GetValue<JShort>(),
			_ => this.GetNumericPrimitive<JShort>(),
		};
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
	/// Copies the sequence of bytes of <paramref name="primitiveSignature"/> from current instance to <paramref name="bytes"/>
	/// .
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
	/// Retrieves numeric primitive value.
	/// </summary>
	/// <typeparam name="TPrimitive">Destination primitive type.</typeparam>
	/// <returns>A <typeparamref name="TPrimitive"/></returns>
	private TPrimitive GetNumericPrimitive<TPrimitive>()
		where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive>, IPrimitiveType<TPrimitive>,
		IBinaryNumber<TPrimitive>, ISignedNumber<TPrimitive>
	{
		if (this.Signature.Length == 0) return default;

		Byte signature = IPrimitiveType.GetMetadata<TPrimitive>().Signature[0];
		switch (this.Signature[0])
		{
			case CommonNames.BooleanSignatureChar when signature != CommonNames.ByteSignatureChar:
			case CommonNames.ByteSignatureChar when signature != CommonNames.ByteSignatureChar:
				return this.GetNumericPrimitive<JByte, TPrimitive>();
			case CommonNames.CharSignatureChar when signature != CommonNames.ShortSignatureChar:
				return this.GetNumericPrimitive<JShort, TPrimitive>();
			case CommonNames.DoubleSignatureChar when signature != CommonNames.DoubleSignatureChar:
				return this.GetNumericPrimitive<JDouble, TPrimitive>();
			case CommonNames.FloatSignatureChar when signature != CommonNames.FloatSignatureChar:
				return this.GetNumericPrimitive<JFloat, TPrimitive>();
			case CommonNames.IntSignatureChar when signature != CommonNames.IntSignatureChar:
				return this.GetNumericPrimitive<JInt, TPrimitive>();
			case CommonNames.LongSignatureChar when signature != CommonNames.LongSignatureChar:
				return this.GetNumericPrimitive<JLong, TPrimitive>();
			case CommonNames.ShortSignatureChar when signature != CommonNames.ShortSignatureChar:
				return this.GetNumericPrimitive<JShort, TPrimitive>();
			default:
				ref JValue.PrimitiveValue valueRef = ref Unsafe.AsRef(in this._primitive);
				return Unsafe.As<JValue.PrimitiveValue, TPrimitive>(ref valueRef);
		}
	}
	/// <summary>
	/// Retrieves numeric primitive value.
	/// </summary>
	/// <typeparam name="TSource">Source primitive type.</typeparam>
	/// <typeparam name="TPrimitive">Destination primitive type.</typeparam>
	/// <returns>A <typeparamref name="TPrimitive"/></returns>
	private TPrimitive GetNumericPrimitive<TSource, TPrimitive>()
		where TSource : unmanaged, IPrimitiveNumericType<TSource>, IPrimitiveType<TSource>, IBinaryNumber<TSource>,
		ISignedNumber<TSource>
		where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive>, IPrimitiveType<TPrimitive>,
		IBinaryNumber<TPrimitive>, ISignedNumber<TPrimitive>
	{
		ref JValue.PrimitiveValue valueRef = ref Unsafe.AsRef(in this._primitive);
		ref TSource result = ref Unsafe.As<JValue.PrimitiveValue, TSource>(ref valueRef);
		Double doubleValue = TSource.ToDouble(result);
		return TPrimitive.FromDouble(doubleValue);
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