namespace Rxmxnx.JNetInterface.Tests.Primitives;

[ExcludeFromCodeCoverage]
public sealed class ShortTest : PrimitiveTestBase
{
	[Fact]
	internal void Test()
	{
		PrimitiveTestBase.SpanParseableTest<JShort, Int16>();
		Int16 value = PrimitiveTestBase.Fixture.Create<Int16>();
		JShort primitive = value;
		PrimitiveTestBase.IntegerTest<JShort, Int16>();
		PrimitiveTestBase.SignedNumberTypeTest<JShort, Int16>();
		PrimitiveTestBase.NumericOperationsTest<JShort, Int16>();
		PrimitiveTestBase.NumericTypeTest<JShort, Int16>();
		PrimitiveTestBase.SpanFormattableTest<JShort, Int16>(primitive);
		Assert.IsType<JPrimitiveObject<JShort>>((JObject)primitive);
		foreach (Int16 newValue in PrimitiveTestBase.Fixture.CreateMany<Int16>(10))
			ShortTest.EqualityTest(primitive, newValue);
	}
	[Fact]
	internal void MetadataTest()
	{
		JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<JShort>();

		Assert.Equal(ClassNames.ShortPrimitive, metadata.ClassName.ToString());
		Assert.True(UnicodeClassNames.ShortPrimitive().SequenceEqual(metadata.ClassName));
		Assert.Equal(PrimitiveSignatures.ShortSignature, metadata.Signature.ToString());
		Assert.Equal(UnicodePrimitiveSignatures.ShortSignatureChar, metadata.Signature[0]);

		Assert.Equal(ClassNames.ShortObject, metadata.WrapperClassName.ToString());
		Assert.True(UnicodeClassNames.ShortObject().SequenceEqual(metadata.WrapperClassName));
	}
	private static void EqualityTest(JShort primitive0, JShort primitive1)
	{
		Boolean equals = primitive0.Value == primitive1.Value;
		Assert.Equal(equals, primitive0 == primitive1);
		Assert.Equal(equals, primitive0 == primitive1.Value);
		Assert.Equal(!equals, primitive0 != primitive1);
		Assert.Equal(!equals, primitive0 != primitive1.Value);

		Assert.Equal(equals, primitive1.Value == primitive0);
		Assert.Equal(!equals, primitive1.Value != primitive0);
		Assert.Equal(primitive1.Value, ((JShort)(Char)primitive1.Value).Value);
	}
}