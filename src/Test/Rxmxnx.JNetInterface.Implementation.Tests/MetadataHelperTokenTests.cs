namespace Rxmxnx.JNetInterface.Tests;

/// <summary>
/// Covers <see cref="MetadataHelper.GetExactMetadata(JReferenceTypeToken)"/>.
/// A token with metadata is returned as-is. A token without metadata is resolved from <see cref="JNativeType"/>.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class MetadataHelperTokenTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void TokenWithMetadataIsRegisteredAndReturned()
	{
		JObjectLocalRef localRef = MetadataHelperTokenTests.fixture.Create<JObjectLocalRef>();
		JArrayLocalRef arrayRef = MetadataHelperTokenTests.fixture.Create<JArrayLocalRef>();

		MetadataHelperTokenTests.AssertSameMetadata<JClassObject>(JReferenceTypeToken.Create<JClassObject>(localRef));
		MetadataHelperTokenTests.AssertSameMetadata<JStringObject>(new JStringLocalRef(localRef));
		MetadataHelperTokenTests.AssertSameMetadata<JThrowableObject>(new JThrowableLocalRef(localRef));
		MetadataHelperTokenTests.AssertSameMetadata<JArrayObject<JBoolean>>(new JBooleanArrayLocalRef(arrayRef));
		MetadataHelperTokenTests.AssertSameMetadata<JArrayObject<JInt>>(JReferenceTypeToken.Create<JInt>(arrayRef));
		MetadataHelperTokenTests.AssertSameMetadata<JArrayObject<JStringObject>>(
			JReferenceTypeToken.Create<JStringObject>(new JObjectArrayLocalRef(arrayRef)));
	}
	[Fact]
	internal void TokenWithoutMetadataUsesTheNativeType()
	{
		MetadataHelperTokenTests.AssertNativeType<JClassObject>(JNativeType.JClass);
		MetadataHelperTokenTests.AssertNativeType<JStringObject>(JNativeType.JString);
		MetadataHelperTokenTests.AssertNativeType<JArrayObject<JBoolean>>(JNativeType.JBooleanArray);
		MetadataHelperTokenTests.AssertNativeType<JArrayObject<JByte>>(JNativeType.JByteArray);
		MetadataHelperTokenTests.AssertNativeType<JArrayObject<JChar>>(JNativeType.JCharArray);
		MetadataHelperTokenTests.AssertNativeType<JArrayObject<JDouble>>(JNativeType.JDoubleArray);
		MetadataHelperTokenTests.AssertNativeType<JArrayObject<JFloat>>(JNativeType.JFloatArray);
		MetadataHelperTokenTests.AssertNativeType<JArrayObject<JInt>>(JNativeType.JIntArray);
		MetadataHelperTokenTests.AssertNativeType<JArrayObject<JLong>>(JNativeType.JLongArray);
		MetadataHelperTokenTests.AssertNativeType<JArrayObject<JShort>>(JNativeType.JShortArray);

		Assert.Null(MetadataHelper.GetExactMetadata(MetadataHelperTokenTests.WithoutMetadata(JNativeType.JObject)));
		Assert.Null(MetadataHelper.GetExactMetadata(MetadataHelperTokenTests.WithoutMetadata(JNativeType.JArray)));
		Assert.Null(
			MetadataHelper.GetExactMetadata(MetadataHelperTokenTests.WithoutMetadata(JNativeType.JObjectArray)));
		Assert.Null(MetadataHelper.GetExactMetadata(MetadataHelperTokenTests.WithoutMetadata(JNativeType.JThrowable)));
		Assert.Null(MetadataHelper.GetExactMetadata(MetadataHelperTokenTests.fixture.Create<JObjectLocalRef>()));
	}

	private static void AssertSameMetadata<TReference>(JReferenceTypeToken token)
		where TReference : JReferenceObject, IReferenceType<TReference>
	{
		JReferenceTypeMetadata expected = IReferenceType.GetMetadata<TReference>();
		Assert.Same(expected, MetadataHelper.GetExactMetadata(token));
		Assert.Same(expected, MetadataHelper.GetExactMetadata(token));
	}
	private static void AssertNativeType<TReference>(JNativeType nativeType)
		where TReference : JReferenceObject, IReferenceType<TReference>
		=> Assert.Same(IReferenceType.GetMetadata<TReference>(),
		               MetadataHelper.GetExactMetadata(MetadataHelperTokenTests.WithoutMetadata(nativeType)));
	private static JReferenceTypeToken WithoutMetadata(JNativeType nativeType)
	{
		Object box = default(JReferenceTypeToken);
		typeof(JReferenceTypeToken).GetProperty(nameof(JReferenceTypeToken.NativeType))!.SetValue(box, nativeType);
		return (JReferenceTypeToken)box;
	}
}