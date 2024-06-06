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

		Assert.True(UnicodeClassNames.ShortPrimitive().SequenceEqual(metadata.ClassName));
		Assert.Equal(UnicodePrimitiveSignatures.ShortSignatureChar, metadata.Signature[0]);

		Assert.True("java/lang/Short"u8.SequenceEqual(metadata.WrapperClassName));
	}
	[Fact]
	private void ConstructorsTest()
	{
		Assert.Equal(SByte.MinValue, new JShort(SByte.MinValue).Value);
		Assert.Equal(SByte.MaxValue, new JShort(SByte.MaxValue).Value);
		Assert.Equal(0, new JShort('\u0000').Value);
		Assert.Equal(-1, new JShort('\uffff').Value);
		Assert.Equal(0, new JShort(Double.MinValue).Value);
		Assert.Equal(-1, new JShort(Double.MaxValue).Value);
		Assert.Equal(0, new JShort(Single.MinValue).Value);
		Assert.Equal(-1, new JShort(Single.MaxValue).Value);
		Assert.Equal(0, new JShort(Int32.MinValue).Value);
		Assert.Equal(-1, new JShort(Int32.MaxValue).Value);
		Assert.Equal(0, new JShort(Int64.MinValue).Value);
		Assert.Equal(-1, new JShort(Int64.MaxValue).Value);
		Assert.Equal(Int16.MinValue, new JShort(Int16.MinValue).Value);
		Assert.Equal(Int16.MaxValue, new JShort(Int16.MaxValue).Value);

		Assert.Equal(SByte.MinValue, new JShort((Single)SByte.MinValue).Value);
		Assert.Equal(SByte.MaxValue, new JShort((Single)SByte.MaxValue).Value);
		Assert.Equal(0, new JShort((Single)'\u0000').Value);
		Assert.Equal(-1, new JShort((Single)'\uffff').Value);
		Assert.Equal(0, new JShort((Single)Double.MinValue).Value);
		Assert.Equal(-1, new JShort((Single)Double.MaxValue).Value);
		Assert.Equal(0, new JShort((Single)Int32.MinValue).Value);
		Assert.Equal(-1, new JShort((Single)Int32.MaxValue).Value);
		Assert.Equal(0, new JShort((Single)Int64.MinValue).Value);
		Assert.Equal(-1, new JShort((Single)Int64.MaxValue).Value);
		Assert.Equal(Int16.MinValue, new JShort((Single)Int16.MinValue).Value);
		Assert.Equal(Int16.MaxValue, new JShort((Single)Int16.MaxValue).Value);

		Assert.Equal(SByte.MinValue, new JShort((Double)SByte.MinValue).Value);
		Assert.Equal(SByte.MaxValue, new JShort((Double)SByte.MaxValue).Value);
		Assert.Equal(0, new JShort((Double)'\u0000').Value);
		Assert.Equal(-1, new JShort((Double)'\uffff').Value);
		Assert.Equal(0, new JShort((Double)Single.MinValue).Value);
		Assert.Equal(-1, new JShort((Double)Single.MaxValue).Value);
		Assert.Equal(0, new JShort((Double)Int32.MinValue).Value);
		Assert.Equal(-1, new JShort((Double)Int32.MaxValue).Value);
		Assert.Equal(0, new JShort((Double)Int64.MinValue).Value);
		Assert.Equal(-1, new JShort((Double)Int64.MaxValue).Value);
		Assert.Equal(Int16.MinValue, new JShort((Double)Int16.MinValue).Value);
		Assert.Equal(Int16.MaxValue, new JShort((Double)Int16.MaxValue).Value);
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