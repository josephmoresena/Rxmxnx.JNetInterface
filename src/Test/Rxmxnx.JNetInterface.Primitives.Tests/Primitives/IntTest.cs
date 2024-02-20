namespace Rxmxnx.JNetInterface.Tests.Primitives;

[ExcludeFromCodeCoverage]
public sealed class IntTest : PrimitiveTestBase
{
	[Fact]
	internal void Test()
	{
		PrimitiveTestBase.SpanParseableTest<JInt, Int32>();
		Int32 value = PrimitiveTestBase.Fixture.Create<Int32>();
		JInt primitive = value;
		PrimitiveTestBase.IntegerTest<JInt, Int32>();
		PrimitiveTestBase.SignedNumberTypeTest<JInt, Int32>();
		PrimitiveTestBase.NumericOperationsTest<JInt, Int32>();
		PrimitiveTestBase.NumericTypeTest<JInt, Int32>();
		PrimitiveTestBase.SpanFormattableTest<JInt, Int32>(primitive);
		Assert.IsType<JPrimitiveObject<JInt>>((JObject)primitive);
		foreach (Int32 newValue in PrimitiveTestBase.Fixture.CreateMany<Int32>(10))
			IntTest.EqualityTest(primitive, newValue);
	}
	[Fact]
	internal void MetadataTest()
	{
		JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<JInt>();

		Assert.Equal(ClassNames.IntPrimitive, metadata.ClassName.ToString());
		Assert.True(UnicodeClassNames.IntPrimitive().SequenceEqual(metadata.ClassName));
		Assert.Equal(PrimitiveSignatures.IntSignature, metadata.Signature.ToString());
		Assert.Equal(UnicodePrimitiveSignatures.IntSignatureChar, metadata.Signature[0]);

		Assert.Equal(ClassNames.IntegerObject, metadata.WrapperClassName.ToString());
		Assert.True(UnicodeClassNames.IntegerObject().SequenceEqual(metadata.WrapperClassName));
	}
	private static void EqualityTest(JInt primitive0, JInt primitive1)
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