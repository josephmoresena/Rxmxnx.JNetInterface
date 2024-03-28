namespace Rxmxnx.JNetInterface.Tests.Primitives;

[ExcludeFromCodeCoverage]
public sealed class CharTest : PrimitiveTestBase
{
	[Fact]
	internal void Test()
	{
		PrimitiveTestBase.SpanParseableTest<JChar, Char>();
		Char value = PrimitiveTestBase.Fixture.Create<Char>();
		JChar primitive = value;
		PrimitiveTestBase.IntegerTest<JChar, Char>();
		PrimitiveTestBase.NumericOperationsTest<JChar, Char>();
		PrimitiveTestBase.NumericTypeTest<JChar, Char>();
		PrimitiveTestBase.SpanFormattableTest<JChar, Char>(primitive);
		Assert.IsType<JPrimitiveObject<JChar>>((JObject)primitive);
		foreach (Char newValue in PrimitiveTestBase.Fixture.CreateMany<Char>(10))
			CharTest.EqualityTest(primitive, newValue);
	}
	[Fact]
	internal void MetadataTest()
	{
		JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<JChar>();

		Assert.Equal(ClassNames.CharPrimitive, metadata.ClassName.ToString());
		Assert.True(UnicodeClassNames.CharPrimitive().SequenceEqual(metadata.ClassName));
		Assert.Equal(PrimitiveSignatures.CharSignature, metadata.Signature.ToString());
		Assert.Equal(UnicodePrimitiveSignatures.CharSignatureChar, metadata.Signature[0]);

		Assert.Equal(ClassNames.CharacterObject, metadata.WrapperClassName.ToString());
		Assert.True(UnicodeClassNames.CharacterObject().SequenceEqual(metadata.WrapperClassName));

		using IFixedPointer.IDisposable fPtr = (metadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, metadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
	private static void EqualityTest(JChar primitive0, JChar primitive1)
	{
		Boolean equals = primitive0.Value == primitive1.Value;
		Assert.Equal(equals, primitive0 == primitive1);
		Assert.Equal(equals, primitive0 == primitive1.Value);
		Assert.Equal(!equals, primitive0 != primitive1);
		Assert.Equal(!equals, primitive0 != primitive1.Value);

		Assert.Equal(equals, primitive1.Value == primitive0);
		Assert.Equal(!equals, primitive1.Value != primitive0);
		Assert.Equal(primitive1.Value, ((JChar)(Int16)primitive1.Value).Value);
	}
}