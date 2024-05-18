namespace Rxmxnx.JNetInterface.Tests.Primitives;

[ExcludeFromCodeCoverage]
public sealed class DoubleTest : PrimitiveTestBase
{
	[Fact]
	internal void Test()
	{
		PrimitiveTestBase.SpanParseableTest<JDouble, Double>();
		Double value = PrimitiveTestBase.Fixture.Create<Double>();
		JDouble primitive = value;
		PrimitiveTestBase.FloatingTest<JDouble, Double>();
		PrimitiveTestBase.SignedNumberTypeTest<JDouble, Double>();
		PrimitiveTestBase.NumericOperationsTest<JDouble, Double>();
		PrimitiveTestBase.NumericTypeTest<JDouble, Double>();
		PrimitiveTestBase.SpanFormattableTest<JDouble, Double>(primitive);
		Assert.IsType<JPrimitiveObject<JDouble>>((JObject)primitive);
		foreach (Double newValue in PrimitiveTestBase.Fixture.CreateMany<Double>(10))
			DoubleTest.EqualityTest(primitive, newValue);
	}
	[Fact]
	internal void MetadataTest()
	{
		JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<JDouble>();

		Assert.Equal(ClassNames.DoublePrimitive, metadata.ClassName.ToString());
		Assert.True(UnicodeClassNames.DoublePrimitive().SequenceEqual(metadata.ClassName));
		Assert.Equal(PrimitiveSignatures.DoubleSignature, metadata.Signature.ToString());
		Assert.Equal(UnicodePrimitiveSignatures.DoubleSignatureChar, metadata.Signature[0]);

		Assert.Equal(ClassNames.DoubleObject, metadata.WrapperClassName.ToString());
		Assert.True(UnicodeClassNames.DoubleObject().SequenceEqual(metadata.WrapperClassName));

		using IFixedPointer.IDisposable fPtr = (metadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, metadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
	[Fact]
	private void ConstructorsTest()
	{
		Assert.Equal(SByte.MinValue, new JDouble(SByte.MinValue).Value);
		Assert.Equal(SByte.MaxValue, new JDouble(SByte.MaxValue).Value);
		Assert.Equal(0, new JDouble('\u0000').Value);
		Assert.Equal(UInt16.MaxValue, new JDouble('\uffff').Value);
		Assert.Equal(Double.MinValue, new JDouble(Double.MinValue).Value);
		Assert.Equal(Double.MaxValue, new JDouble(Double.MaxValue).Value);
		Assert.Equal(Single.MinValue, new JDouble(Single.MinValue).Value);
		Assert.Equal(Single.MaxValue, new JDouble(Single.MaxValue).Value);
		Assert.Equal(Int32.MinValue, new JDouble(Int32.MinValue).Value);
		Assert.Equal(Int32.MaxValue, new JDouble(Int32.MaxValue).Value);
		Assert.Equal(Int64.MinValue, new JDouble(Int64.MinValue).Value);
		Assert.Equal(Int64.MaxValue, new JDouble(Int64.MaxValue).Value);
		Assert.Equal(Int16.MinValue, new JDouble(Int16.MinValue).Value);
		Assert.Equal(Int16.MaxValue, new JDouble(Int16.MaxValue).Value);

		Assert.Equal(SByte.MinValue, new JDouble((Single)SByte.MinValue).Value);
		Assert.Equal(SByte.MaxValue, new JDouble((Single)SByte.MaxValue).Value);
		Assert.Equal(0, new JDouble((Single)'\u0000').Value);
		Assert.Equal(UInt16.MaxValue, new JDouble((Single)'\uffff').Value);
		Assert.Equal(Single.NegativeInfinity, new JDouble((Single)Double.MinValue).Value);
		Assert.Equal(Single.PositiveInfinity, new JDouble((Single)Double.MaxValue).Value);
		Assert.Equal(Int32.MinValue, new JDouble((Single)Int32.MinValue).Value);
		Assert.Equal(0x80000000, new JDouble((Single)Int32.MaxValue).Value);
		Assert.Equal(Int64.MinValue, new JDouble((Single)Int64.MinValue).Value);
		Assert.Equal(Int64.MaxValue, new JDouble((Single)Int64.MaxValue).Value);
		Assert.Equal(Int16.MinValue, new JDouble((Single)Int16.MinValue).Value);
		Assert.Equal(Int16.MaxValue, new JDouble((Single)Int16.MaxValue).Value);

		Assert.Equal(SByte.MinValue, new JDouble((Double)SByte.MinValue).Value);
		Assert.Equal(SByte.MaxValue, new JDouble((Double)SByte.MaxValue).Value);
		Assert.Equal(0, new JDouble((Double)'\u0000').Value);
		Assert.Equal(UInt16.MaxValue, new JDouble('\uffff').Value);
		Assert.Equal(Single.MinValue, new JDouble((Double)Single.MinValue).Value);
		Assert.Equal(Single.MaxValue, new JDouble((Double)Single.MaxValue).Value);
		Assert.Equal(Int32.MinValue, new JDouble((Double)Int32.MinValue).Value);
		Assert.Equal(Int32.MaxValue, new JDouble((Double)Int32.MaxValue).Value);
		Assert.Equal(Int64.MinValue, new JDouble((Double)Int64.MinValue).Value);
		Assert.Equal(Int64.MaxValue, new JDouble((Double)Int64.MaxValue).Value);
		Assert.Equal(Int16.MinValue, new JDouble((Double)Int16.MinValue).Value);
		Assert.Equal(Int16.MaxValue, new JDouble((Double)Int16.MaxValue).Value);
	}
	private static void EqualityTest(JDouble primitive0, JDouble primitive1)
	{
		Boolean equals = Math.Abs(primitive0.Value - primitive1.Value) <= JDouble.Epsilon;
		Assert.Equal(equals, primitive0 == primitive1);
		Assert.Equal(equals, primitive0 == primitive1.Value);
		Assert.Equal(!equals, primitive0 != primitive1);
		Assert.Equal(!equals, primitive0 != primitive1.Value);

		Assert.Equal(equals, primitive1.Value == primitive0);
		Assert.Equal(!equals, primitive1.Value != primitive0);
	}
}