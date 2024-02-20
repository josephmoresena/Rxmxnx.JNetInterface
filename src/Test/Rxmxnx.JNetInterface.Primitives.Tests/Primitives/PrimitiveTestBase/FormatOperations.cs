namespace Rxmxnx.JNetInterface.Tests.Primitives;

public partial class PrimitiveTestBase
{
	private static IEnumerable<CultureInfo> GetCultures(Int32 count)
	{
		for (Int32 i = 0; i < count; i++)
			yield return PrimitiveTestBase.cultures[Random.Shared.Next(0, PrimitiveTestBase.cultures.Length)];
	}
	private static void SpanParseableTest<TPrimitive, TValue>(TValue value, IFormatProvider? provider = default)
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
		PrimitiveTestBase.ParseableTest<TPrimitive, TValue>(text, provider);
	}
	private static void ParseableTest<TPrimitive, TValue>(String text, IFormatProvider? provider)
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
	private static void SpanFormattableTest<TPrimitive, TValue>(TPrimitive primitive0, IFormatProvider? provider)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveEquatable, ISpanFormattable
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>, ISpanFormattable
	{
		Span<Char> chars0 = stackalloc Char[primitive0.Value.ToString(provider).Length];
		Span<Char> chars1 = stackalloc Char[chars0.Length];
		Assert.Equal(primitive0.Value.TryFormat(chars0, out Int32 charsW0, default, provider),
		             primitive0.TryFormat(chars1, out Int32 charsW1, default, provider));
		Assert.Equal(charsW0, charsW1);
		Assert.True(chars0.SequenceEqual(chars1));
	}
}