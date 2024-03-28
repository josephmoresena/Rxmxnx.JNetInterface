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