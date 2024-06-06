namespace Rxmxnx.JNetInterface.Tests.Primitives;

[ExcludeFromCodeCoverage]
public sealed class FloatTest : PrimitiveTestBase
{
	[Fact]
	internal void Test()
	{
		PrimitiveTestBase.SpanParseableTest<JFloat, Single>();
		Single value = PrimitiveTestBase.Fixture.Create<Single>();
		JFloat primitive = value;
		PrimitiveTestBase.FloatingTest<JFloat, Single>();
		PrimitiveTestBase.SignedNumberTypeTest<JFloat, Single>();
		PrimitiveTestBase.NumericOperationsTest<JFloat, Single>();
		PrimitiveTestBase.NumericTypeTest<JFloat, Single>();
		PrimitiveTestBase.SpanFormattableTest<JFloat, Single>(primitive);
		Assert.IsType<JPrimitiveObject<JFloat>>((JObject)primitive);
		foreach (Single newValue in PrimitiveTestBase.Fixture.CreateMany<Single>(10))
			FloatTest.EqualityTest(primitive, newValue);
	}
	[Fact]
	internal void MetadataTest()
	{
		JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<JFloat>();

		Assert.True(UnicodeClassNames.FloatPrimitive().SequenceEqual(metadata.ClassName));
		Assert.Equal(PrimitiveSignatures.FloatSignature, metadata.Signature.ToString());
		Assert.Equal(UnicodePrimitiveSignatures.FloatSignatureChar, metadata.Signature[0]);

		Assert.True("java/lang/Float"u8.SequenceEqual(metadata.WrapperClassName));
	}
	[Fact]
	private void ConstructorsTest()
	{
		Assert.Equal(SByte.MinValue, new JFloat(SByte.MinValue).Value);
		Assert.Equal(SByte.MaxValue, new JFloat(SByte.MaxValue).Value);
		Assert.Equal(0, new JFloat('\u0000').Value);
		Assert.Equal(UInt16.MaxValue, new JFloat('\uffff').Value);
		Assert.Equal(Single.NegativeInfinity, new JFloat(Double.MinValue).Value);
		Assert.Equal(Single.PositiveInfinity, new JFloat(Double.MaxValue).Value);
		Assert.Equal(Single.MinValue, new JFloat(Single.MinValue).Value);
		Assert.Equal(Single.MaxValue, new JFloat(Single.MaxValue).Value);
		Assert.Equal(Int32.MinValue, new JFloat(Int32.MinValue).Value);
		Assert.Equal(Int32.MaxValue, new JFloat(Int32.MaxValue).Value);
		Assert.Equal(Int64.MinValue, new JFloat(Int64.MinValue).Value);
		Assert.Equal(Int64.MaxValue, new JFloat(Int64.MaxValue).Value);
		Assert.Equal(Int16.MinValue, new JFloat(Int16.MinValue).Value);
		Assert.Equal(Int16.MaxValue, new JFloat(Int16.MaxValue).Value);

		Assert.Equal(SByte.MinValue, new JFloat((Single)SByte.MinValue).Value);
		Assert.Equal(SByte.MaxValue, new JFloat((Single)SByte.MaxValue).Value);
		Assert.Equal(0, new JFloat((Single)'\u0000').Value);
		Assert.Equal(UInt16.MaxValue, new JFloat((Single)'\uffff').Value);
		Assert.Equal(Single.NegativeInfinity, new JFloat((Single)Double.MinValue).Value);
		Assert.Equal(Single.PositiveInfinity, new JFloat((Single)Double.MaxValue).Value);
		Assert.Equal(Int32.MinValue, new JFloat((Single)Int32.MinValue).Value);
		Assert.Equal(0x80000000, new JFloat((Single)Int32.MaxValue).Value);
		Assert.Equal(Int64.MinValue, new JFloat((Single)Int64.MinValue).Value);
		Assert.Equal(Int64.MaxValue, new JFloat((Single)Int64.MaxValue).Value);
		Assert.Equal(Int16.MinValue, new JFloat((Single)Int16.MinValue).Value);
		Assert.Equal(Int16.MaxValue, new JFloat((Single)Int16.MaxValue).Value);

		Assert.Equal(SByte.MinValue, new JFloat((Double)SByte.MinValue).Value);
		Assert.Equal(SByte.MaxValue, new JFloat((Double)SByte.MaxValue).Value);
		Assert.Equal(0, new JFloat((Double)'\u0000').Value);
		Assert.Equal(UInt16.MaxValue, new JFloat('\uffff').Value);
		Assert.Equal(Single.MinValue, new JFloat((Double)Single.MinValue).Value);
		Assert.Equal(Single.MaxValue, new JFloat((Double)Single.MaxValue).Value);
		Assert.Equal(Int32.MinValue, new JFloat((Double)Int32.MinValue).Value);
		Assert.Equal(0x80000000, new JFloat((Double)Int32.MaxValue).Value);
		Assert.Equal(Int64.MinValue, new JFloat((Double)Int64.MinValue).Value);
		Assert.Equal(Int64.MaxValue, new JFloat((Double)Int64.MaxValue).Value);
		Assert.Equal(Int16.MinValue, new JFloat((Double)Int16.MinValue).Value);
		Assert.Equal(Int16.MaxValue, new JFloat((Double)Int16.MaxValue).Value);
	}
	private static void EqualityTest(JFloat primitive0, JFloat primitive1)
	{
		Boolean equals = Math.Abs(primitive0.Value - primitive1.Value) <= JFloat.Epsilon;
		Assert.Equal(equals, primitive0 == primitive1);
		Assert.Equal(equals, primitive0 == primitive1.Value);
		Assert.Equal(!equals, primitive0 != primitive1);
		Assert.Equal(!equals, primitive0 != primitive1.Value);

		Assert.Equal(equals, primitive1.Value == primitive0);
		Assert.Equal(!equals, primitive1.Value != primitive0);
	}
}