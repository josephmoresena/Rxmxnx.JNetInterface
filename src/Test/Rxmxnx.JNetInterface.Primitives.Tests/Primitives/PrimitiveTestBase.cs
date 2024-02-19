namespace Rxmxnx.JNetInterface.Tests.Primitives;

[ExcludeFromCodeCoverage]
public abstract class PrimitiveTestBase
{
	private static readonly CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

	protected static readonly Fixture Fixture = new();

	protected static IEnumerable<CultureInfo> GetCultures(Int32 count)
	{
		for (Int32 i = 0; i < count; i++)
			yield return PrimitiveTestBase.cultures[Random.Shared.Next(0, PrimitiveTestBase.cultures.Length)];
	}

	private protected static void TestSpanParseable<TPrimitive, TValue>()
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveEquatable, ISpanParsable<TPrimitive>
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>,
		ISpanParsable<TValue>
	{
		TValue value = PrimitiveTestBase.Fixture.Create<TValue>();
		PrimitiveTestBase.TestSpanParseable<TPrimitive, TValue>(value);
		PrimitiveTestBase.TestSpanParseable<TPrimitive, TValue>(value, CultureInfo.CurrentCulture);
		PrimitiveTestBase.TestSpanParseable<TPrimitive, TValue>(value, CultureInfo.InvariantCulture);
		foreach (CultureInfo culture in PrimitiveTestBase.GetCultures(10))
			PrimitiveTestBase.TestSpanParseable<TPrimitive, TValue>(value, culture);
	}
	private static void TestSpanParseable<TPrimitive, TValue>(TValue value, IFormatProvider? provider = default)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveEquatable, ISpanParsable<TPrimitive>
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>,
		ISpanParsable<TValue>

	{
		TPrimitive primitive = value;
		String text = primitive.ToString(provider);
		Assert.Equal(TValue.Parse(text, provider), TPrimitive.Parse(text, provider));
		Assert.Equal(TValue.TryParse(text, provider, out TValue valueO),
		             TPrimitive.TryParse(text, provider, out TPrimitive primitiveO));
		Assert.Equal(valueO, primitiveO.Value);
		PrimitiveTestBase.TestParseable<TPrimitive, TValue>(text, provider);
	}
	private static void TestParseable<TPrimitive, TValue>(String text, IFormatProvider? provider)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveEquatable, IParsable<TPrimitive>
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>, IParsable<TValue>
	{
		Assert.Equal(TValue.Parse(text, provider), TPrimitive.Parse(text, provider));
		Assert.Equal(TValue.Parse(text, provider), TPrimitive.Parse(text, provider));
		Assert.Equal(TValue.TryParse(text, provider, out TValue valueO),
		             TPrimitive.TryParse(text, provider, out TPrimitive primitiveO));
		Assert.Equal(valueO, primitiveO.Value);
	}
}