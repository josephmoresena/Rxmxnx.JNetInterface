namespace Rxmxnx.JNetInterface.Tests.Primitives;

public partial class PrimitiveTestBase
{
	private static void DoubleNumericTypeTest<TPrimitive, TValue>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryNumber<TPrimitive>, INumberBase<TPrimitive>, IMinMaxValue<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryNumber<TValue>, INumberBase<TValue>
	{
		Double dValue = (Double)primitive;
		Assert.Equal(typeof(TValue) == typeof(Char) ? (Char)(Object)primitive.Value : primitive.Value.ToDouble(default),
		             dValue);
		Assert.Equal(primitive, (TPrimitive)dValue);
	}
	private static void NumericTypeTest<TPrimitive, TValue>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryNumber<TPrimitive>, INumberBase<TPrimitive>, IMinMaxValue<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryNumber<TValue>, INumberBase<TValue>
	{
		Assert.Equal(TValue.Abs(primitive.Value), TPrimitive.Abs(primitive).Value);
		if (TValue.IsPositive(primitive.Value))
			Assert.Equal(TValue.Log2(primitive.Value), TPrimitive.Log2(primitive));
		else
			Assert.Equal(TValue.Log2(-primitive.Value), TPrimitive.Log2(-primitive));
		Assert.Equal(TValue.Sign(primitive.Value), TPrimitive.Sign(primitive));
		Assert.Equal(TValue.Clamp(primitive.Value, TValue.MinValue, TValue.MaxValue),
		             TPrimitive.Clamp(primitive, TPrimitive.MinValue, TPrimitive.MaxValue));

		Assert.Equal(TValue.CreateChecked(primitive.Value), TPrimitive.CreateChecked(primitive).Value);
		Assert.Equal(TValue.CreateSaturating(primitive.Value), TPrimitive.CreateSaturating(primitive).Value);
		Assert.Equal(TValue.CreateTruncating(primitive.Value), TPrimitive.CreateTruncating(primitive).Value);

		Assert.Equal(TValue.CreateChecked(primitive), TPrimitive.CreateChecked(primitive.Value).Value);
		Assert.Equal(TValue.CreateSaturating(primitive), TPrimitive.CreateSaturating(primitive.Value).Value);
		Assert.Equal(TValue.CreateTruncating(primitive), TPrimitive.CreateTruncating(primitive.Value).Value);

		Assert.Equal(TValue.IsNegative(primitive.Value), TPrimitive.IsNegative(primitive));
		Assert.Equal(TValue.IsNormal(primitive.Value), TPrimitive.IsNormal(primitive));
		Assert.Equal(TValue.IsCanonical(primitive.Value), TPrimitive.IsCanonical(primitive));
		Assert.Equal(TValue.IsInfinity(primitive.Value), TPrimitive.IsInfinity(primitive));
		Assert.Equal(TValue.IsInteger(primitive.Value), TPrimitive.IsInteger(primitive));
		Assert.Equal(TValue.IsFinite(primitive.Value), TPrimitive.IsFinite(primitive));
		Assert.Equal(TValue.IsPositive(primitive.Value), TPrimitive.IsPositive(primitive));
		Assert.Equal(TValue.IsPow2(primitive.Value), TPrimitive.IsPow2(primitive));
		Assert.Equal(TValue.IsSubnormal(primitive.Value), TPrimitive.IsSubnormal(primitive));
		Assert.Equal(TValue.IsZero(primitive.Value), TPrimitive.IsZero(primitive));
		Assert.Equal(TValue.IsComplexNumber(primitive.Value), TPrimitive.IsComplexNumber(primitive));
		Assert.Equal(TValue.IsImaginaryNumber(primitive.Value), TPrimitive.IsImaginaryNumber(primitive));
		Assert.Equal(TValue.IsEvenInteger(primitive.Value), TPrimitive.IsEvenInteger(primitive));
		Assert.Equal(TValue.IsNaN(primitive.Value), TPrimitive.IsNaN(primitive));
		Assert.Equal(TValue.IsNegativeInfinity(primitive.Value), TPrimitive.IsNegativeInfinity(primitive));
		Assert.Equal(TValue.IsOddInteger(primitive.Value), TPrimitive.IsOddInteger(primitive));
		Assert.Equal(TValue.IsPositiveInfinity(primitive.Value), TPrimitive.IsPositiveInfinity(primitive));
		Assert.Equal(TValue.IsRealNumber(primitive.Value), TPrimitive.IsRealNumber(primitive));

		Assert.Equal(~primitive.Value, (~primitive).Value);
		Assert.Equal(primitive.Value | TValue.One, (primitive | TPrimitive.One).Value);
		Assert.Equal(primitive.Value | TValue.Zero, (primitive | TPrimitive.Zero).Value);
		Assert.Equal(primitive.Value & TValue.One, (primitive & TPrimitive.One).Value);
		Assert.Equal(primitive.Value & TValue.Zero, (primitive & TPrimitive.Zero).Value);
		Assert.Equal(primitive.Value ^ TValue.One, (primitive ^ TPrimitive.One).Value);
		Assert.Equal(primitive.Value ^ TValue.Zero, (primitive ^ TPrimitive.Zero).Value);

		PrimitiveTestBase.IncrementTest<TPrimitive, TValue>(primitive);
		PrimitiveTestBase.DecrementTest<TPrimitive, TValue>(primitive);
	}
	private static void IncrementTest<TPrimitive, TValue>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryNumber<TPrimitive>, INumberBase<TPrimitive>, IMinMaxValue<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryNumber<TValue>, INumberBase<TValue>
	{
		TValue vInc = primitive.Value;
		TPrimitive pInc = primitive;
		Assert.Equal(vInc++, pInc++);
		Assert.Equal(vInc, pInc.Value);
		if (primitive.Value < TValue.MaxValue)
		{
			vInc = TValue.MinValue;
			pInc = TPrimitive.MinValue;
		}
		Assert.Equal(checked(vInc++), checked(pInc++));
		Assert.Equal(vInc, pInc.Value);
	}
	private static void DecrementTest<TPrimitive, TValue>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryNumber<TPrimitive>, INumberBase<TPrimitive>, IMinMaxValue<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryNumber<TValue>, INumberBase<TValue>
	{
		TValue vDec = primitive.Value;
		TPrimitive pDec = primitive;
		Assert.Equal(vDec--, pDec--);
		Assert.Equal(vDec, pDec.Value);
		if (primitive.Value > TValue.MinValue)
		{
			vDec = TValue.MaxValue;
			pDec = TPrimitive.MaxValue;
		}
		Assert.Equal(checked(vDec--), checked(pDec--));
		Assert.Equal(vDec, pDec.Value);
	}
	private static void ConversionNumericTypeTest<TPrimitive, TValue>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable
		where TValue : unmanaged, IConvertible, IBinaryNumber<TValue>, IMinMaxValue<TValue>
	{
		SByte bValue = PrimitiveTestBase.Convert(primitive.Value, c => c.ToSByte(default));
		Char cValue = PrimitiveTestBase.Convert(primitive.Value, c => c.ToChar(default));
		Double dValue = typeof(TValue) == typeof(Char) ?
			(Char)(Object)primitive.Value :
			primitive.Value.ToDouble(default);
		Single fValue = typeof(TValue) == typeof(Char) ?
			(Char)(Object)primitive.Value :
			primitive.Value.ToSingle(default);
		Int32 iValue = PrimitiveTestBase.Convert(primitive.Value, c => c.ToInt32(default));
		Int64 lValue = PrimitiveTestBase.Convert(primitive.Value, c => c.ToInt64(default));
		Int16 sValue = PrimitiveTestBase.Convert(primitive.Value, c => c.ToInt16(default));

		Assert.Equal(bValue, ((JByte)primitive).Value);
		Assert.Equal(cValue, ((JChar)primitive).Value);
		Assert.Equal(dValue, ((JDouble)primitive).Value);
		Assert.Equal(fValue, ((JFloat)primitive).Value);
		Assert.Equal(iValue, ((JInt)primitive).Value);
		Assert.Equal(lValue, ((JLong)primitive).Value);
		Assert.Equal(sValue, ((JShort)primitive).Value);
	}
	private static T Convert<T>(IConvertible c, Func<IConvertible, T> func) where T : unmanaged, IBinaryInteger<T>
	{
		try
		{
			return func(c);
		}
		catch (Exception)
		{
			Int64 l = c.ToInt64(default);
			return l.AsBytes().AsValues<Byte, T>()[0];
		}
	}
	private static void CheckedOperationNumericTypeTest<TPrimitive, TValue>()
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryNumber<TPrimitive>, INumberBase<TPrimitive>, IMinMaxValue<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryNumber<TValue>, INumberBase<TValue>
	{
		TPrimitive sum = checked(TValue.One + TValue.One);
		TPrimitive diff = checked(TValue.One - TValue.One);
		TPrimitive product = checked(TValue.One * TValue.One);
		TPrimitive div = checked(TValue.One / TValue.One);

		Assert.Equal(sum, checked(TPrimitive.One + TPrimitive.One));
		Assert.Equal(product, checked(TPrimitive.One * TPrimitive.One));
		Assert.Equal(checked(-diff), checked(TPrimitive.One - TPrimitive.One));
		Assert.Equal(div, checked(TPrimitive.One / TPrimitive.One));
	}
	private static void NumericOperationsTest<TPrimitive, TValue>(TPrimitive primitive0, TPrimitive primitive1)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryNumber<TPrimitive>, INumberBase<TPrimitive>, IMinMaxValue<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryNumber<TValue>, INumberBase<TValue>
	{
		TPrimitive sum = primitive0.Value + primitive1.Value;
		TPrimitive diff0 = primitive0.Value - primitive1.Value;
		TPrimitive diff1 = primitive1.Value - primitive0.Value;
		TPrimitive product = primitive0.Value * primitive1.Value;

		Boolean gt = primitive0.Value > primitive1.Value;
		Boolean gte = primitive0.Value >= primitive1.Value;
		Boolean lt = primitive0.Value < primitive1.Value;
		Boolean lte = primitive0.Value <= primitive1.Value;

		Assert.Equal(sum, primitive0 + primitive1);
		Assert.Equal(product, primitive0 * primitive1);
		Assert.Equal(+diff0, primitive0 - primitive1);
		Assert.Equal(-diff1, primitive0 - primitive1);
		Assert.Equal(+diff1, primitive1 - primitive0);
		Assert.Equal(-diff0, primitive1 - primitive0);

		PrimitiveTestBase.DivisionTest<TPrimitive, TValue>(primitive0, primitive1);
		PrimitiveTestBase.DivisionTest<TPrimitive, TValue>(primitive1, primitive0);

		Assert.Equal(gt, primitive0 > primitive1);
		Assert.Equal(gte, primitive0 >= primitive1);
		Assert.Equal(lt, primitive0 < primitive1);
		Assert.Equal(lte, primitive0 <= primitive1);
		Assert.Equal(primitive0.Value | primitive1.Value, (primitive0 | primitive1).Value);
		Assert.Equal(primitive0.Value & primitive1.Value, (primitive0 & primitive1).Value);
		Assert.Equal(primitive0.Value ^ primitive1.Value, (primitive0 ^ primitive1).Value);

		Assert.Equal(TValue.Max(primitive0.Value, primitive1.Value), TPrimitive.Max(primitive0, primitive1).Value);
		Assert.Equal(TValue.Min(primitive0.Value, primitive1.Value), TPrimitive.Min(primitive0, primitive1).Value);
		Assert.Equal(TValue.CopySign(primitive0.Value, primitive1.Value),
		             TPrimitive.CopySign(primitive0, primitive1).Value);
		Assert.Equal(TValue.MaxNumber(primitive0.Value, primitive1.Value),
		             TPrimitive.MaxNumber(primitive0, primitive1).Value);
		Assert.Equal(TValue.MinNumber(primitive0.Value, primitive1.Value),
		             TPrimitive.MinNumber(primitive0, primitive1).Value);
		Assert.Equal(TValue.MaxMagnitude(primitive0.Value, primitive1.Value),
		             TPrimitive.MaxMagnitude(primitive0, primitive1).Value);
		Assert.Equal(TValue.MinMagnitude(primitive0.Value, primitive1.Value),
		             TPrimitive.MinMagnitude(primitive0, primitive1).Value);
		Assert.Equal(TValue.MaxMagnitudeNumber(primitive0.Value, primitive1.Value),
		             TPrimitive.MaxMagnitudeNumber(primitive0, primitive1).Value);
		Assert.Equal(TValue.MinMagnitudeNumber(primitive0.Value, primitive1.Value),
		             TPrimitive.MinMagnitudeNumber(primitive0, primitive1).Value);
	}
	private static void DivisionTest<TPrimitive, TValue>(TPrimitive num, TPrimitive div)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryNumber<TPrimitive>, IMinMaxValue<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryNumber<TValue>
	{
		if (TPrimitive.IsZero(div)) div = TPrimitive.One;
		Assert.Equal(num.Value / div.Value, num / div);
		Assert.Equal(num.Value % div.Value, num % div);
	}
	private static void SignedCreateTest<TPrimitive, TValue>()
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryNumber<TPrimitive>, INumberBase<TPrimitive>, IMinMaxValue<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryNumber<TValue>, INumberBase<TValue>
	{
		PrimitiveTestBase.MinMaxCreateTest<TPrimitive, TValue, SByte>();
		PrimitiveTestBase.MinMaxCreateTest<TPrimitive, TValue, Int16>();
		PrimitiveTestBase.MinMaxCreateTest<TPrimitive, TValue, Int32>();
		PrimitiveTestBase.MinMaxCreateTest<TPrimitive, TValue, Int64>();
	}
	private static void UnsignedCreateTest<TPrimitive, TValue>()
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryNumber<TPrimitive>, INumberBase<TPrimitive>, IMinMaxValue<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryNumber<TValue>, INumberBase<TValue>
	{
		PrimitiveTestBase.MinMaxCreateTest<TPrimitive, TValue, Byte>();
		PrimitiveTestBase.MinMaxCreateTest<TPrimitive, TValue, UInt16>();
		PrimitiveTestBase.MinMaxCreateTest<TPrimitive, TValue, UInt32>();
		PrimitiveTestBase.MinMaxCreateTest<TPrimitive, TValue, UInt64>();
	}
	private static void MinMaxCreateTest<TPrimitive, TValue, T>()
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryNumber<TPrimitive>, INumberBase<TPrimitive>, IMinMaxValue<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryNumber<TValue>, INumberBase<TValue>
		where T : unmanaged, IMinMaxValue<T>, INumberBase<T>
	{
		if (NativeUtilities.SizeOf<TPrimitive>() >= NativeUtilities.SizeOf<T>())
		{
			Assert.Equal(TValue.CreateChecked(T.MaxValue), TPrimitive.CreateChecked(T.MaxValue).Value);
			Assert.Equal(TValue.CreateChecked(T.MinValue), TPrimitive.CreateChecked(T.MinValue).Value);
		}
		Assert.Equal(TValue.CreateSaturating(T.MaxValue), TPrimitive.CreateSaturating(T.MaxValue).Value);
		Assert.Equal(TValue.CreateTruncating(T.MaxValue), TPrimitive.CreateTruncating(T.MaxValue).Value);
		Assert.Equal(TValue.CreateSaturating(T.MinValue), TPrimitive.CreateSaturating(T.MinValue).Value);
		Assert.Equal(TValue.CreateTruncating(T.MinValue), TPrimitive.CreateTruncating(T.MinValue).Value);
	}
}