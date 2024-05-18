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
	[Fact]
	private void ConstructorsTest()
	{
		Assert.Equal(0xFF80, new JChar(SByte.MinValue).Value);
		Assert.Equal(0x7F, new JChar(SByte.MaxValue).Value);
		Assert.Equal('\u0000', new JChar('\u0000').Value);
		Assert.Equal('\uffff', new JChar('\uffff').Value);
		Assert.Equal(0, new JChar(Double.MinValue).Value);
		Assert.Equal(0xFFFF, new JChar(Double.MaxValue).Value);
		Assert.Equal(0, new JChar(Single.MinValue).Value);
		Assert.Equal(0xFFFF, new JChar(Single.MaxValue).Value);
		Assert.Equal(0, new JChar(Int32.MinValue).Value);
		Assert.Equal(0xFFFF, new JChar(Int32.MaxValue).Value);
		Assert.Equal(0, new JChar(Int64.MinValue).Value);
		Assert.Equal(0xFFFF, new JChar(Int64.MaxValue).Value);
		Assert.Equal(0x8000, new JChar(Int16.MinValue).Value);
		Assert.Equal(0x7FFF, new JChar(Int16.MaxValue).Value);

		Assert.Equal(0xFF80, new JChar((Single)SByte.MinValue).Value);
		Assert.Equal(0x7F, new JChar((Single)SByte.MaxValue).Value);
		Assert.Equal('\u0000', new JChar((Single)'\u0000').Value);
		Assert.Equal('\uffff', new JChar((Single)'\uffff').Value);
		Assert.Equal(0, new JChar((Single)Double.MinValue).Value);
		Assert.Equal(0xFFFF, new JChar((Single)Double.MaxValue).Value);
		Assert.Equal(0, new JChar((Single)Int32.MinValue).Value);
		Assert.Equal(0xFFFF, new JChar((Single)Int32.MaxValue).Value);
		Assert.Equal(0, new JChar((Single)Int64.MinValue).Value);
		Assert.Equal(0xFFFF, new JChar((Single)Int64.MaxValue).Value);
		Assert.Equal(0x8000, new JChar((Single)Int16.MinValue).Value);
		Assert.Equal(0x7FFF, new JChar((Single)Int16.MaxValue).Value);

		Assert.Equal(0xFF80, new JChar((Double)SByte.MinValue).Value);
		Assert.Equal(0x7F, new JChar((Double)SByte.MaxValue).Value);
		Assert.Equal('\u0000', new JChar((Double)'\u0000').Value);
		Assert.Equal('\uffff', new JChar((Double)'\uffff').Value);
		Assert.Equal(0, new JChar((Double)Single.MinValue).Value);
		Assert.Equal(0xFFFF, new JChar((Double)Single.MaxValue).Value);
		Assert.Equal(0, new JChar((Double)Int32.MinValue).Value);
		Assert.Equal(0xFFFF, new JChar((Double)Int32.MaxValue).Value);
		Assert.Equal(0, new JChar((Double)Int64.MinValue).Value);
		Assert.Equal(0xFFFF, new JChar((Double)Int64.MaxValue).Value);
		Assert.Equal(0x8000, new JChar((Double)Int16.MinValue).Value);
		Assert.Equal(0x7FFF, new JChar((Double)Int16.MaxValue).Value);
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