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
	[Fact]
	private void ConstructorsTest()
	{
		Assert.Equal(SByte.MinValue, new JInt(SByte.MinValue).Value);
		Assert.Equal(SByte.MaxValue, new JInt(SByte.MaxValue).Value);
		Assert.Equal(0, new JInt('\u0000').Value);
		Assert.Equal(UInt16.MaxValue, new JInt('\uffff').Value);
		Assert.Equal(0, new JInt(Double.MinValue).Value);
		Assert.Equal(0x7FFFFFFF, new JInt(Double.MaxValue).Value);
		Assert.Equal(0, new JInt(Single.MinValue).Value);
		Assert.Equal(0x7FFFFFFF, new JInt(Single.MaxValue).Value);
		Assert.Equal(Int32.MinValue, new JInt(Int32.MinValue).Value);
		Assert.Equal(Int32.MaxValue, new JInt(Int32.MaxValue).Value);
		Assert.Equal(0, new JInt(Int64.MinValue).Value);
		Assert.Equal(-1, new JInt(Int64.MaxValue).Value);
		Assert.Equal(Int16.MinValue, new JInt(Int16.MinValue).Value);
		Assert.Equal(Int16.MaxValue, new JInt(Int16.MaxValue).Value);

		Assert.Equal(SByte.MinValue, new JInt((Single)SByte.MinValue).Value);
		Assert.Equal(SByte.MaxValue, new JInt((Single)SByte.MaxValue).Value);
		Assert.Equal(0, new JInt((Single)'\u0000').Value);
		Assert.Equal(UInt16.MaxValue, new JInt((Single)'\uffff').Value);
		Assert.Equal(0, new JInt((Single)Double.MinValue).Value);
		Assert.Equal(0x7FFFFFFF, new JInt((Single)Double.MaxValue).Value);
		Assert.Equal(Int32.MinValue, new JInt((Single)Int32.MinValue).Value);
		Assert.Equal(Int32.MaxValue, new JInt((Single)Int32.MaxValue).Value);
		Assert.Equal(0, new JInt((Single)Int64.MinValue).Value);
		Assert.Equal(0x7FFFFFFF, new JInt((Single)Int64.MaxValue).Value);
		Assert.Equal(Int16.MinValue, new JInt((Single)Int16.MinValue).Value);
		Assert.Equal(Int16.MaxValue, new JInt((Single)Int16.MaxValue).Value);

		Assert.Equal(SByte.MinValue, new JInt((Double)SByte.MinValue).Value);
		Assert.Equal(SByte.MaxValue, new JInt((Double)SByte.MaxValue).Value);
		Assert.Equal(0, new JInt((Double)'\u0000').Value);
		Assert.Equal(UInt16.MaxValue, new JInt((Double)'\uffff').Value);
		Assert.Equal(0, new JInt((Double)Single.MinValue).Value);
		Assert.Equal(0x7FFFFFFF, new JInt((Double)Single.MaxValue).Value);
		Assert.Equal(Int32.MinValue, new JInt((Double)Int32.MinValue).Value);
		Assert.Equal(Int32.MaxValue, new JInt((Double)Int32.MaxValue).Value);
		Assert.Equal(0, new JInt((Double)Int64.MinValue).Value);
		Assert.Equal(0x7FFFFFFF, new JInt((Double)Int64.MaxValue).Value);
		Assert.Equal(Int16.MinValue, new JInt((Double)Int16.MinValue).Value);
		Assert.Equal(Int16.MaxValue, new JInt((Double)Int16.MaxValue).Value);
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