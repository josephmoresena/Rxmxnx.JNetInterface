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
		PrimitiveTestBase.SignedNumberTypeTest<JLong, Int64>();
		PrimitiveTestBase.NumericOperationsTest<JLong, Int64>();
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

		using IFixedPointer.IDisposable fPtr = (metadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, metadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
	[Fact]
	private void ConstructorsTest()
	{
		Assert.Equal(SByte.MinValue, new JLong(SByte.MinValue).Value);
		Assert.Equal(SByte.MaxValue, new JLong(SByte.MaxValue).Value);
		Assert.Equal(0, new JLong('\u0000').Value);
		Assert.Equal(UInt16.MaxValue, new JLong('\uffff').Value);
		Assert.Equal(0, new JLong(Double.MinValue).Value);
		Assert.Equal(0x7FFFFFFFFFFFFFFF, new JLong(Double.MaxValue).Value);
		Assert.Equal(0, new JLong(Single.MinValue).Value);
		Assert.Equal(0x7FFFFFFFFFFFFFFF, new JLong(Single.MaxValue).Value);
		Assert.Equal(Int32.MinValue, new JLong(Int32.MinValue).Value);
		Assert.Equal(Int32.MaxValue, new JLong(Int32.MaxValue).Value);
		Assert.Equal(Int64.MinValue, new JLong(Int64.MinValue).Value);
		Assert.Equal(Int64.MaxValue, new JLong(Int64.MaxValue).Value);
		Assert.Equal(Int16.MinValue, new JLong(Int16.MinValue).Value);
		Assert.Equal(Int16.MaxValue, new JLong(Int16.MaxValue).Value);

		Assert.Equal(SByte.MinValue, new JLong((Single)SByte.MinValue).Value);
		Assert.Equal(SByte.MaxValue, new JLong((Single)SByte.MaxValue).Value);
		Assert.Equal(0, new JLong((Single)'\u0000').Value);
		Assert.Equal(UInt16.MaxValue, new JLong((Single)'\uffff').Value);
		Assert.Equal(0, new JLong((Single)Double.MinValue).Value);
		Assert.Equal(0x7FFFFFFFFFFFFFFF, new JLong((Single)Double.MaxValue).Value);
		Assert.Equal(Int32.MinValue, new JLong((Single)Int32.MinValue).Value);
		Assert.Equal(0x80000000, new JLong((Single)Int32.MaxValue).Value);
		Assert.Equal(Int64.MinValue, new JLong((Single)Int64.MinValue).Value);
		Assert.Equal(Int64.MaxValue, new JLong((Single)Int64.MaxValue).Value);
		Assert.Equal(Int16.MinValue, new JLong((Single)Int16.MinValue).Value);
		Assert.Equal(Int16.MaxValue, new JLong((Single)Int16.MaxValue).Value);

		Assert.Equal(SByte.MinValue, new JLong((Double)SByte.MinValue).Value);
		Assert.Equal(SByte.MaxValue, new JLong((Double)SByte.MaxValue).Value);
		Assert.Equal(0, new JLong((Double)'\u0000').Value);
		Assert.Equal(UInt16.MaxValue, new JLong((Double)'\uffff').Value);
		Assert.Equal(0, new JLong((Double)Single.MinValue).Value);
		Assert.Equal(0x7FFFFFFFFFFFFFFF, new JLong((Double)Single.MaxValue).Value);
		Assert.Equal(Int32.MinValue, new JLong((Double)Int32.MinValue).Value);
		Assert.Equal(Int32.MaxValue, new JLong((Double)Int32.MaxValue).Value);
		Assert.Equal(Int64.MinValue, new JLong((Double)Int64.MinValue).Value);
		Assert.Equal(Int64.MaxValue, new JLong((Double)Int64.MaxValue).Value);
		Assert.Equal(Int16.MinValue, new JLong((Double)Int16.MinValue).Value);
		Assert.Equal(Int16.MaxValue, new JLong((Double)Int16.MaxValue).Value);
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