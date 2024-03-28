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

		Assert.Equal(ClassNames.FloatPrimitive, metadata.ClassName.ToString());
		Assert.True(UnicodeClassNames.FloatPrimitive().SequenceEqual(metadata.ClassName));
		Assert.Equal(PrimitiveSignatures.FloatSignature, metadata.Signature.ToString());
		Assert.Equal(UnicodePrimitiveSignatures.FloatSignatureChar, metadata.Signature[0]);

		Assert.Equal(ClassNames.FloatObject, metadata.WrapperClassName.ToString());
		Assert.True(UnicodeClassNames.FloatObject().SequenceEqual(metadata.WrapperClassName));

		using IFixedPointer.IDisposable fPtr = (metadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, metadata.ClassName.AsSpan().GetUnsafeIntPtr());
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