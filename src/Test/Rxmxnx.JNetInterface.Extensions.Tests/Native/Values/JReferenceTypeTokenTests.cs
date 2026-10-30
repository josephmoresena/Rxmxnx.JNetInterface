namespace Rxmxnx.JNetInterface.Tests.Native.Values;

/// <summary>
/// Covers <see cref="JReferenceTypeToken"/> conversions and
/// <see cref="JReferenceTypeToken.Create{TReference}(JObjectLocalRef)"/>.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class JReferenceTypeTokenTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void ImplicitOperatorsKeepTheReferenceAndNativeType()
	{
		JObjectLocalRef localRef = JReferenceTypeTokenTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = new(localRef);
		JStringLocalRef stringRef = new(localRef);
		JThrowableLocalRef throwableRef = new(localRef);
		JArrayLocalRef arrayRef = JReferenceTypeTokenTests.fixture.Create<JArrayLocalRef>();
		JObjectArrayLocalRef objectArrayRef = new(arrayRef);
		JBooleanArrayLocalRef booleanArrayRef = new(arrayRef);
		JByteArrayLocalRef byteArrayRef = new(arrayRef);
		JCharArrayLocalRef charArrayRef = new(arrayRef);
		JDoubleArrayLocalRef doubleArrayRef = new(arrayRef);
		JFloatArrayLocalRef floatArrayRef = new(arrayRef);
		JIntArrayLocalRef intArrayRef = new(arrayRef);
		JLongArrayLocalRef longArrayRef = new(arrayRef);
		JShortArrayLocalRef shortArrayRef = new(arrayRef);

		JReferenceTypeTokenTests.AssertWithoutMetadata(localRef, localRef, JNativeType.JObject);
		JReferenceTypeTokenTests.AssertWithMetadata<JClassObject>(classRef, classRef.Value, JNativeType.JClass);
		JReferenceTypeTokenTests.AssertWithMetadata<JStringObject>(stringRef, stringRef.Value, JNativeType.JString);
		JReferenceTypeTokenTests.AssertWithMetadata<JThrowableObject>(throwableRef, throwableRef.Value,
		                                                              JNativeType.JThrowable);
		JReferenceTypeTokenTests.AssertWithoutMetadata(arrayRef, arrayRef.Value, JNativeType.JArray);
		JReferenceTypeTokenTests.AssertWithoutMetadata(objectArrayRef, objectArrayRef.Value, JNativeType.JObjectArray);
		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JBoolean>>(booleanArrayRef,
		                                                                    booleanArrayRef.Value,
		                                                                    JNativeType.JBooleanArray);
		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JByte>>(byteArrayRef,
		                                                                 byteArrayRef.Value, JNativeType.JByteArray);
		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JChar>>(charArrayRef,
		                                                                 charArrayRef.Value, JNativeType.JCharArray);
		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JDouble>>(doubleArrayRef,
		                                                                   doubleArrayRef.Value,
		                                                                   JNativeType.JDoubleArray);
		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JFloat>>(floatArrayRef,
		                                                                  floatArrayRef.Value, JNativeType.JFloatArray);
		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JInt>>(intArrayRef, intArrayRef.Value,
		                                                                JNativeType.JIntArray);
		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JLong>>(longArrayRef,
		                                                                 longArrayRef.Value, JNativeType.JLongArray);
		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JShort>>(shortArrayRef,
		                                                                  shortArrayRef.Value, JNativeType.JShortArray);
	}
	[Fact]
	internal void CreateUsesTheRequestedReferenceType()
	{
		JObjectLocalRef localRef = JReferenceTypeTokenTests.fixture.Create<JObjectLocalRef>();
		JThrowableLocalRef throwableRef = new(localRef);
		JArrayLocalRef arrayRef = JReferenceTypeTokenTests.fixture.Create<JArrayLocalRef>();
		JObjectArrayLocalRef objectArrayRef = new(arrayRef);

		JReferenceTypeTokenTests.AssertWithMetadata<JClassObject>(JReferenceTypeToken.Create<JClassObject>(localRef),
		                                                          localRef, JNativeType.JClass);
		JReferenceTypeTokenTests.AssertWithMetadata<JStringObject>(JReferenceTypeToken.Create<JStringObject>(localRef),
		                                                           localRef, JNativeType.JString);
		JReferenceTypeTokenTests.AssertWithMetadata<JThrowableObject>(
			JReferenceTypeToken.Create<JThrowableObject>(localRef), localRef, JNativeType.JThrowable);
		JReferenceTypeTokenTests.AssertWithMetadata<JMethodObject>(JReferenceTypeToken.Create<JMethodObject>(localRef),
		                                                           localRef, JNativeType.JObject);
		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JBoolean>>(
			JReferenceTypeToken.Create<JArrayObject<JBoolean>>(localRef), localRef, JNativeType.JBooleanArray);
		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JByte>>(
			JReferenceTypeToken.Create<JArrayObject<JByte>>(localRef), localRef, JNativeType.JByteArray);
		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JChar>>(
			JReferenceTypeToken.Create<JArrayObject<JChar>>(localRef), localRef, JNativeType.JCharArray);
		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JDouble>>(
			JReferenceTypeToken.Create<JArrayObject<JDouble>>(localRef), localRef, JNativeType.JDoubleArray);
		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JFloat>>(
			JReferenceTypeToken.Create<JArrayObject<JFloat>>(localRef), localRef, JNativeType.JFloatArray);
		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JInt>>(
			JReferenceTypeToken.Create<JArrayObject<JInt>>(localRef), localRef, JNativeType.JIntArray);
		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JLong>>(
			JReferenceTypeToken.Create<JArrayObject<JLong>>(localRef), localRef, JNativeType.JLongArray);
		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JShort>>(
			JReferenceTypeToken.Create<JArrayObject<JShort>>(localRef), localRef, JNativeType.JShortArray);
		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JStringObject>>(
			JReferenceTypeToken.Create<JArrayObject<JStringObject>>(localRef), localRef, JNativeType.JObjectArray);

		JReferenceTypeTokenTests.AssertWithMetadata<JThrowableObject>(
			JReferenceTypeToken.Create<JThrowableObject>(throwableRef), throwableRef.Value, JNativeType.JThrowable);

		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JBoolean>>(
			JReferenceTypeToken.Create<JBoolean>(arrayRef), arrayRef.Value, JNativeType.JBooleanArray);
		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JStringObject>>(
			JReferenceTypeToken.Create<JStringObject>(arrayRef), arrayRef.Value, JNativeType.JObjectArray);
		JReferenceTypeTokenTests.AssertWithMetadata<JArrayObject<JStringObject>>(
			JReferenceTypeToken.Create<JStringObject>(objectArrayRef), objectArrayRef.Value, JNativeType.JObjectArray);
	}

	private static void AssertWithoutMetadata(JReferenceTypeToken token, JObjectLocalRef reference,
		JNativeType nativeType)
	{
		Assert.Equal(reference, token.Reference);
		Assert.Equal(nativeType, token.NativeType);
		Assert.Null(token.GetMetadata());
	}
	private static void AssertWithMetadata<TReference>(JReferenceTypeToken token, JObjectLocalRef reference,
		JNativeType nativeType) where TReference : JReferenceObject, IReferenceType<TReference>
	{
		Assert.Equal(reference, token.Reference);
		Assert.Equal(nativeType, token.NativeType);
		Assert.Same(IReferenceType.GetMetadata<TReference>(), token.GetMetadata());
	}
}