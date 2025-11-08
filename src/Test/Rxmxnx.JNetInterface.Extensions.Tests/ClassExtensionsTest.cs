namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
#pragma warning disable CA1859
public sealed class ClassExtensionsTest
{
	[Fact]
	internal void GetRuntimeTypeMetadataVoidTest()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JDataTypeMetadata typeMetadata = JPrimitiveTypeMetadata.VoidMetadata;
		using JClassObject jClassClass = new(env);
		using JClassObject jClass = new(jClassClass, typeMetadata);

		Assert.Equal(jClass.GetRuntimeTypeMetadata(), typeMetadata);
		env.ClassFeature.Received(0).GetTypeMetadata(jClass);
	}
	[Fact]
	internal void BooleanTest() => ClassExtensionsTest.Test<JBoolean>();
	[Fact]
	internal void ByteTest() => ClassExtensionsTest.Test<JByte>();
	[Fact]
	internal void CharTest() => ClassExtensionsTest.Test<JChar>();
	[Fact]
	internal void DoubleTest() => ClassExtensionsTest.Test<JDouble>();
	[Fact]
	internal void FloatTest() => ClassExtensionsTest.Test<JFloat>();
	[Fact]
	internal void IntTest() => ClassExtensionsTest.Test<JInt>();
	[Fact]
	internal void LongTest() => ClassExtensionsTest.Test<JLong>();
	[Fact]
	internal void ShortTest() => ClassExtensionsTest.Test<JShort>();
	[Fact]
	internal void ObjectTest() => ClassExtensionsTest.Test<JLocalObject>();
	[Fact]
	internal void ClassTest() => ClassExtensionsTest.Test<JClassObject>();
	[Fact]
	internal void ThrowableTest() => ClassExtensionsTest.Test<JThrowableObject>();
	[Fact]
	internal void SystemTest() => ClassExtensionsTest.Test<JSystemObject>();
	[Fact]
	internal void StackTraceElementTest() => ClassExtensionsTest.Test<JStackTraceElementObject>();
	[Fact]
	internal void StringTest() => ClassExtensionsTest.Test<JStringObject>();
	[Fact]
	internal void EnumTest() => ClassExtensionsTest.Test<JEnumObject>();
	[Fact]
	internal void AnnotationTest() => ClassExtensionsTest.Test<JAnnotationObject>();
	[Fact]
	internal void WrapperClassesTest()
	{
		ClassExtensionsTest.Test<JBooleanObject>();
		ClassExtensionsTest.Test<JByteObject>();
		ClassExtensionsTest.Test<JCharacterObject>();
		ClassExtensionsTest.Test<JDoubleObject>();
		ClassExtensionsTest.Test<JFloatObject>();
		ClassExtensionsTest.Test<JIntegerObject>();
		ClassExtensionsTest.Test<JLongObject>();
		ClassExtensionsTest.Test<JShortObject>();
		ClassExtensionsTest.Test<JVoidObject>();
	}

	private static void Test<TDataType>() where TDataType : IDataType<TDataType>
	{
		ClassExtensionsTest.GetRuntimeTypeMetadataTest<TDataType>();
		ClassExtensionsTest.GetRuntimeTypeMetadataTest<JArrayObject<TDataType>>();
	}

	private static void GetRuntimeTypeMetadataTest<TDataType>() where TDataType : IDataType<TDataType>
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JDataTypeMetadata typeMetadata = IDataType.GetMetadata<TDataType>();
		using JClassObject jClassClass = new(env);
		using JClassObject jClass =
			!JLocalObject.IsClassType<TDataType>() ? new(jClassClass, typeMetadata) : jClassClass;

		env.ClassFeature.GetTypeMetadata(jClass).Returns(typeMetadata as JReferenceTypeMetadata);
		Assert.Equal(jClass.GetRuntimeTypeMetadata(), typeMetadata);

		env.ClassFeature.Received(typeMetadata is not JPrimitiveTypeMetadata ? 1 : 0).GetTypeMetadata(jClass);
	}
}