namespace Rxmxnx.JNetInterface.Tests.Primitives;

[ExcludeFromCodeCoverage]
public sealed class LongTest : PrimitiveTestBase
{
	[Fact]
	internal void Test()
	{
		PrimitiveTestBase.SpanParseableTest<JLong, Int64>();
		Int64 value = PrimitiveTestBase.Fixture.Create<Int64>();
		JLong primitive = value;
		PrimitiveTestBase.IntegerTest<JLong, Int64>();
		PrimitiveTestBase.NegativeOneTest<JLong, Int64>();
		PrimitiveTestBase.OperationNumericTypeTest<JLong, Int64>();
		PrimitiveTestBase.NumericTypeTest<JLong, Int64>();
		PrimitiveTestBase.SpanFormattableTest<JLong, Int64>(primitive);
		Assert.IsType<JPrimitiveObject<JLong>>((JObject)primitive);
		foreach (Int64 newValue in PrimitiveTestBase.Fixture.CreateMany<Int64>(10))
			LongTest.EqualityTest(primitive, newValue);
	}
	[Fact]
	internal void MetadataTest()
	{
		JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<JLong>();

		Assert.Equal(ClassNames.LongPrimitive, metadata.ClassName.ToString());
		Assert.True(UnicodeClassNames.LongPrimitive().SequenceEqual(metadata.ClassName));
		Assert.Equal(PrimitiveSignatures.LongSignature, metadata.Signature.ToString());
		Assert.Equal(UnicodePrimitiveSignatures.LongSignatureChar, metadata.Signature[0]);
		
		Assert.Equal(ClassNames.LongObject, metadata.WrapperClassName.ToString());
		Assert.True(UnicodeClassNames.LongObject().SequenceEqual(metadata.WrapperClassName));
	}
	private static void EqualityTest(JLong primitive0, JLong primitive1)
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