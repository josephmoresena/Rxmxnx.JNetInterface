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

		Assert.True("byte"u8.SequenceEqual(metadata.ClassName));
		Assert.Equal(CommonNames.ByteSignatureChar, metadata.Signature[0]);

		Assert.True("java/lang/Byte"u8.SequenceEqual(metadata.WrapperClassName));
	}
	[Fact]
	private void ConstructorsTest()
	{
		Assert.Equal(SByte.MinValue, new JByte(SByte.MinValue).Value);
		Assert.Equal(SByte.MaxValue, new JByte(SByte.MaxValue).Value);
		Assert.Equal(0, new JByte('\u0000').Value);
		Assert.Equal(-1, new JByte('\uffff').Value);
		Assert.Equal(0, new JByte(Double.MinValue).Value);
		Assert.Equal(-1, new JByte(Double.MaxValue).Value);
		Assert.Equal(0, new JByte(Single.MinValue).Value);
		Assert.Equal(-1, new JByte(Single.MaxValue).Value);
		Assert.Equal(0, new JByte(Int32.MinValue).Value);
		Assert.Equal(-1, new JByte(Int32.MaxValue).Value);
		Assert.Equal(0, new JByte(Int64.MinValue).Value);
		Assert.Equal(-1, new JByte(Int64.MaxValue).Value);
		Assert.Equal(0, new JByte(Int16.MinValue).Value);
		Assert.Equal(-1, new JByte(Int16.MaxValue).Value);

		Assert.Equal(SByte.MinValue, new JByte((Single)SByte.MinValue).Value);
		Assert.Equal(SByte.MaxValue, new JByte((Single)SByte.MaxValue).Value);
		Assert.Equal(0, new JByte((Single)'\u0000').Value);
		Assert.Equal(-1, new JByte((Single)'\uffff').Value);
		Assert.Equal(0, new JByte((Single)Double.MinValue).Value);
		Assert.Equal(-1, new JByte((Single)Double.MaxValue).Value);
		Assert.Equal(0, new JByte((Single)Int32.MinValue).Value);
		Assert.Equal(-1, new JByte((Single)Int32.MaxValue).Value);
		Assert.Equal(0, new JByte((Single)Int64.MinValue).Value);
		Assert.Equal(-1, new JByte((Single)Int64.MaxValue).Value);
		Assert.Equal(0, new JByte((Single)Int16.MinValue).Value);
		Assert.Equal(-1, new JByte((Single)Int16.MaxValue).Value);

		Assert.Equal(SByte.MinValue, new JByte((Double)SByte.MinValue).Value);
		Assert.Equal(SByte.MaxValue, new JByte((Double)SByte.MaxValue).Value);
		Assert.Equal(0, new JByte((Double)'\u0000').Value);
		Assert.Equal(-1, new JByte((Double)'\uffff').Value);
		Assert.Equal(0, new JByte((Double)Int32.MinValue).Value);
		Assert.Equal(-1, new JByte((Double)Int32.MaxValue).Value);
		Assert.Equal(0, new JByte((Double)Int64.MinValue).Value);
		Assert.Equal(-1, new JByte((Double)Int64.MaxValue).Value);
		Assert.Equal(0, new JByte((Double)Int16.MinValue).Value);
		Assert.Equal(-1, new JByte((Double)Int16.MaxValue).Value);
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