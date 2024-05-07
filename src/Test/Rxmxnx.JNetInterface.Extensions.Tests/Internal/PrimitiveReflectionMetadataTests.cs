namespace Rxmxnx.JNetInterface.Tests.Internal;

[ExcludeFromCodeCoverage]
public sealed class PrimitiveReflectionMetadataTests
{
	[Fact]
	internal void BooleanTest() => PrimitiveReflectionMetadataTests.Test<JBoolean>();
	[Fact]
	internal void CharTest() => PrimitiveReflectionMetadataTests.Test<JChar>();
	[Fact]
	internal void DoubleTest() => PrimitiveReflectionMetadataTests.Test<JDouble>();
	[Fact]
	internal void FloatTest() => PrimitiveReflectionMetadataTests.Test<JFloat>();
	[Fact]
	internal void IntTest() => PrimitiveReflectionMetadataTests.Test<JInt>();
	[Fact]
	internal void LongTest() => PrimitiveReflectionMetadataTests.Test<JLong>();

	private static void Test<TPrimitive>() where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		JPrimitiveTypeMetadata primitiveTypeMetadata = IPrimitiveType.GetMetadata<TPrimitive>();
		CString className = primitiveTypeMetadata.ClassName;
		CString classSignature = CString.Concat("L"u8, className, ";"u8);
		CString arraySignature = CString.Concat("["u8, classSignature);
		CStringSequence fakeHash = new(className.AsSpan(), classSignature, arraySignature);
		PrimitiveReflectionMetadata<TPrimitive> reflectionMetadata = PrimitiveReflectionMetadata<TPrimitive>.Instance;

		Assert.Equal(primitiveTypeMetadata.ArgumentMetadata, reflectionMetadata.ArgumentMetadata);
		Assert.Equal(JFunctionDefinition<TPrimitive>.Create("functionName"u8),
		             reflectionMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.Equal(new JFieldDefinition<TPrimitive>("fieldName"u8),
		             reflectionMetadata.CreateFieldDefinition("fieldName"u8));

		Assert.Equal(fakeHash.ToString(), PrimitiveReflectionMetadata<TPrimitive>.FakeHash);
	}
}