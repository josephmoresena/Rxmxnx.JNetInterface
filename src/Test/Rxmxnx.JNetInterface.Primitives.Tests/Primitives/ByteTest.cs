namespace Rxmxnx.JNetInterface.Tests.Primitives;

[ExcludeFromCodeCoverage]
public sealed class ByteTest : PrimitiveTestBase
{
	[Fact]
	internal void Test()
	{
		PrimitiveTestBase.SpanParseableTest<JByte, SByte>();
		SByte value = PrimitiveTestBase.Fixture.Create<SByte>();
		JByte primitive = value;
		PrimitiveTestBase.IntegerTest<JByte, SByte>();
		PrimitiveTestBase.SignedNumberTypeTest<JByte, SByte>();
		PrimitiveTestBase.NumericOperationsTest<JByte, SByte>();
		PrimitiveTestBase.NumericTypeTest<JByte, SByte>();
		PrimitiveTestBase.SpanFormattableTest<JByte, SByte>(primitive);
		Assert.IsType<JPrimitiveObject<JByte>>((JObject)primitive);
		foreach (SByte newValue in PrimitiveTestBase.Fixture.CreateMany<SByte>(10))
			ByteTest.EqualityTest(primitive, newValue);
	}
	[Fact]
	internal void MetadataTest()
	{
		JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<JByte>();

		Assert.Equal(ClassNames.BytePrimitive, metadata.ClassName.ToString());
		Assert.True(UnicodeClassNames.BytePrimitive().SequenceEqual(metadata.ClassName));
		Assert.Equal(PrimitiveSignatures.ByteSignature, metadata.Signature.ToString());
		Assert.Equal(UnicodePrimitiveSignatures.ByteSignatureChar, metadata.Signature[0]);

		Assert.Equal(ClassNames.ByteObject, metadata.WrapperClassName.ToString());
		Assert.True(UnicodeClassNames.ByteObject().SequenceEqual(metadata.WrapperClassName));
	}
	private static void EqualityTest(JByte primitive0, JByte primitive1)
	{
		Boolean equals = primitive0.Value == primitive1.Value;
		Assert.Equal(equals, primitive0 == primitive1);
		Assert.Equal(equals, primitive0 == primitive1.Value);
		Assert.Equal(!equals, primitive0 != primitive1);
		Assert.Equal(!equals, primitive0 != primitive1.Value);

		Assert.Equal(equals, primitive1.Value == primitive0);
		Assert.Equal(!equals, primitive1.Value != primitive0);
	}
}