namespace Rxmxnx.JNetInterface.Tests.Primitives;

[ExcludeFromCodeCoverage]
public sealed class BooleanTest : PrimitiveTestBase
{
	[Fact]
	internal void Test()
	{
		PrimitiveTestBase.SpanParseableTest<JBoolean, Boolean>();
		Boolean value = PrimitiveTestBase.Fixture.Create<Boolean>();
		JBoolean primitive = value;

		BooleanTest.TryFormatTest(primitive);
		Assert.IsType<JPrimitiveObject<JBoolean>>((JObject)primitive);
		BooleanTest.ByteTest(primitive);
		foreach (Boolean newValue in PrimitiveTestBase.Fixture.CreateMany<Boolean>(10))
			BooleanTest.EqualityTest(primitive, newValue);
	}
	[Fact]
	internal void MetadataTest()
	{
		JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<JBoolean>();

		Assert.True(UnicodeClassNames.BooleanPrimitive().SequenceEqual(metadata.ClassName));
		Assert.Equal(UnicodePrimitiveSignatures.BooleanSignatureChar, metadata.Signature[0]);
	}

	private static void TryFormatTest(JBoolean primitive0)
	{
		Span<Char> chars0 = stackalloc Char[primitive0.Value.ToString().Length];
		Span<Char> chars1 = stackalloc Char[chars0.Length];
		Assert.Equal(primitive0.Value.TryFormat(chars0, out Int32 charsW0),
		             primitive0.TryFormat(chars1, out Int32 charsW1));
		Assert.Equal(charsW0, charsW1);
		Assert.True(chars0.SequenceEqual(chars1));
	}
	private static void EqualityTest(JBoolean primitive0, JBoolean primitive1)
	{
		Boolean equals = primitive0.Value == primitive1.Value;
		Assert.Equal(equals, primitive0 == primitive1);
		Assert.Equal(equals, primitive0 == primitive1.Value);
		Assert.Equal(!equals, primitive0 != primitive1);
		Assert.Equal(!equals, primitive0 != primitive1.Value);

		Assert.Equal(equals, primitive1.Value == primitive0);
		Assert.Equal(!equals, primitive1.Value != primitive0);

		BooleanTest.ByteTest(primitive1);
	}
	private static void ByteTest(JBoolean primitive)
	{
		Assert.Equal(primitive.Value ? JBoolean.TrueValue : JBoolean.FalseValue, primitive.ByteValue);
		Assert.Equal(primitive, new(primitive.ByteValue == JBoolean.TrueValue));
	}
}