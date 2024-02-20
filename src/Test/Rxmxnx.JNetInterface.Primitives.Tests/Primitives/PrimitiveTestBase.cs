namespace Rxmxnx.JNetInterface.Tests.Primitives;

[ExcludeFromCodeCoverage]
public abstract partial class PrimitiveTestBase
{
	private static readonly CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

	protected static readonly Fixture Fixture = new();

	private protected static void FloatingTest<TPrimitive, TValue>()
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IFloatingPoint<TPrimitive>, IBinaryNumber<TPrimitive>, IFloatingPointIeee754<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IFloatingPoint<TValue>, IBinaryNumber<TValue>,
		IFloatingPointIeee754<TValue>
	{
		Assert.Equal(TValue.E, TPrimitive.E.Value);
		Assert.Equal(TValue.Pi, TPrimitive.Pi.Value);
		Assert.Equal(TValue.Tau, TPrimitive.Tau.Value);
		Assert.Equal(TValue.Epsilon, TPrimitive.Epsilon.Value);
		Assert.Equal(TValue.NaN, TPrimitive.NaN.Value);
		Assert.Equal(TValue.NegativeInfinity, TPrimitive.NegativeInfinity.Value);
		Assert.Equal(TValue.PositiveInfinity, TPrimitive.PositiveInfinity.Value);
		Assert.Equal(TValue.NegativeZero, TPrimitive.NegativeZero.Value);
		foreach (TValue value in PrimitiveTestBase.Fixture.CreateMany<TValue>())
			PrimitiveTestBase.FloatingValueTest<TPrimitive, TValue>(value);
	}
	private protected static void IntegerTest<TPrimitive, TValue>()
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryInteger<TPrimitive>, IShiftOperators<TPrimitive, Int32, TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryInteger<TValue>,
		IShiftOperators<TValue, Int32, TValue>
	{
		foreach (TValue value in PrimitiveTestBase.Fixture.CreateMany<TValue>())
		{
			TPrimitive primitive = value;
			Assert.Equal(TValue.PopCount(primitive.Value), TPrimitive.PopCount(primitive).Value);
			Assert.Equal(TValue.TrailingZeroCount(primitive.Value), TPrimitive.TrailingZeroCount(primitive).Value);
			PrimitiveTestBase.ShiftTest<TPrimitive, TValue>(primitive);
			PrimitiveTestBase.IntegerValueTest<TPrimitive, TValue>(primitive);
		}
	}
	private protected static void SignedNumberTypeTest<TPrimitive, TValue>()
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveSignedType<TPrimitive, TValue>, IPrimitiveEquatable, IBinaryNumber<TPrimitive>
		, INumberBase<TPrimitive>, ISignedNumber<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryNumber<TValue>, ISignedNumber<TValue>
		=> Assert.Equal(TValue.NegativeOne, TPrimitive.NegativeOne.Value);
	private protected static void NumericOperationsTest<TPrimitive, TValue>()
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryNumber<TPrimitive>, INumberBase<TPrimitive>, IMinMaxValue<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryNumber<TValue>, INumberBase<TValue>
	{
		TPrimitive primitive = PrimitiveTestBase.Fixture.Create<TValue>();
		foreach (TValue value in PrimitiveTestBase.Fixture.CreateMany<TValue>())
			PrimitiveTestBase.NumericOperationsTest<TPrimitive, TValue>(primitive, value);
		PrimitiveTestBase.CheckedOperationNumericTypeTest<TPrimitive, TValue>();
	}
	private protected static void NumericTypeTest<TPrimitive, TValue>()
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryNumber<TPrimitive>, INumberBase<TPrimitive>, IMinMaxValue<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryNumber<TValue>, INumberBase<TValue>
	{
		Assert.Equal(TValue.AllBitsSet, TPrimitive.AllBitsSet.Value);
		Assert.Equal(TValue.MinValue, TPrimitive.MinValue.Value);
		Assert.Equal(TValue.MaxValue, TPrimitive.MaxValue.Value);
		Assert.Equal(TValue.One, TPrimitive.One.Value);
		Assert.Equal(TValue.Zero, TPrimitive.Zero.Value);
		Assert.Equal(TValue.Radix, TPrimitive.Radix);
		Assert.Equal(TValue.AdditiveIdentity, TPrimitive.AdditiveIdentity.Value);
		Assert.Equal(TValue.MultiplicativeIdentity, TPrimitive.MultiplicativeIdentity.Value);

		if (typeof(TValue) == typeof(Char))
			PrimitiveTestBase.UnsignedCreateTest<TPrimitive, TValue>();
		else
			PrimitiveTestBase.SignedCreateTest<TPrimitive, TValue>();

		foreach (TValue value in PrimitiveTestBase.Fixture.CreateMany<TValue>(10))
		{
			TPrimitive primitive = value;
			PrimitiveTestBase.NumericTypeTest<TPrimitive, TValue>(primitive);
			PrimitiveTestBase.DoubleNumericTypeTest<TPrimitive, TValue>(primitive);
			PrimitiveTestBase.ConversionNumericTypeTest<TPrimitive, TValue>(primitive);
		}
	}
	private protected static void SpanParseableTest<TPrimitive, TValue>()
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveEquatable, ISpanParsable<TPrimitive>
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>,
		ISpanParsable<TValue>
	{
		TValue value = PrimitiveTestBase.Fixture.Create<TValue>();
		PrimitiveTestBase.SpanParseableTest<TPrimitive, TValue>(value);
		PrimitiveTestBase.SpanParseableTest<TPrimitive, TValue>(value, CultureInfo.CurrentCulture);
		PrimitiveTestBase.SpanParseableTest<TPrimitive, TValue>(value, CultureInfo.InvariantCulture);
		foreach (CultureInfo culture in PrimitiveTestBase.GetCultures(10))
			PrimitiveTestBase.SpanParseableTest<TPrimitive, TValue>(value, culture);
	}
	private protected static void SpanFormattableTest<TPrimitive, TValue>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveEquatable, ISpanFormattable
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>, ISpanFormattable
	{
		foreach (CultureInfo culture in PrimitiveTestBase.GetCultures(10))
			PrimitiveTestBase.SpanFormattableTest<TPrimitive, TValue>(primitive, culture);
	}
}