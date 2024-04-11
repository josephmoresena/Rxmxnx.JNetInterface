namespace Rxmxnx.JNetInterface.Tests.Native.Access;

[ExcludeFromCodeCoverage]
public sealed class JFieldDefinitionTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void ObjectTest() => JFieldDefinitionTests.ObjectFieldTest<JLocalObject>();
	[Fact]
	internal void ClassTest() => JFieldDefinitionTests.ObjectFieldTest<JClassObject>();
	[Fact]
	internal void StringTest() => JFieldDefinitionTests.ObjectFieldTest<JStringObject>();
	[Fact]
	internal void ThrowableTest() => JFieldDefinitionTests.ObjectFieldTest<JThrowableObject>();
	[Fact]
	internal void EnumTest() => JFieldDefinitionTests.ObjectFieldTest<JEnumObject>();
	[Fact]
	internal void NumberTest() => JFieldDefinitionTests.ObjectFieldTest<JNumberObject>();
	[Fact]
	internal void AccessibleObjectTest() => JFieldDefinitionTests.ObjectFieldTest<JAccessibleObject>();
	[Fact]
	internal void ExecutableTest() => JFieldDefinitionTests.ObjectFieldTest<JExecutableObject>();
	[Fact]
	internal void ModifierTest() => JFieldDefinitionTests.ObjectFieldTest<JModifierObject>();
	[Fact]
	internal void ProxyTest() => JFieldDefinitionTests.ObjectFieldTest<JProxyObject>();

	[Fact]
	internal void ErrorTest() => JFieldDefinitionTests.ObjectFieldTest<JErrorObject>();
	[Fact]
	internal void ExceptionTest() => JFieldDefinitionTests.ObjectFieldTest<JExceptionObject>();
	[Fact]
	internal void RuntimeExceptionTest() => JFieldDefinitionTests.ObjectFieldTest<JRuntimeExceptionObject>();
	[Fact]
	internal void BufferTest() => JFieldDefinitionTests.ObjectFieldTest<JBufferObject>();

	[Fact]
	internal void FieldTest() => JFieldDefinitionTests.ObjectFieldTest<JFieldObject>();
	[Fact]
	internal void MethodTest() => JFieldDefinitionTests.ObjectFieldTest<JMethodObject>();
	[Fact]
	internal void ConstructorTest() => JFieldDefinitionTests.ObjectFieldTest<JConstructorObject>();
	[Fact]
	internal void StackTraceElementTest() => JFieldDefinitionTests.ObjectFieldTest<JStackTraceElementObject>();

	[Fact]
	internal void DirectBufferTest() => JFieldDefinitionTests.ObjectFieldTest<JDirectBufferObject>();
	[Fact]
	internal void CharSequenceTest() => JFieldDefinitionTests.ObjectFieldTest<JCharSequenceObject>();
	[Fact]
	internal void CloneableTest() => JFieldDefinitionTests.ObjectFieldTest<JCloneableObject>();
	[Fact]
	internal void ComparableTest() => JFieldDefinitionTests.ObjectFieldTest<JComparableObject>();
	[Fact]
	internal void AnnotatedElementTest() => JFieldDefinitionTests.ObjectFieldTest<JAnnotatedElementObject>();
	[Fact]
	internal void GenericDeclarationTest() => JFieldDefinitionTests.ObjectFieldTest<JGenericDeclarationObject>();
	[Fact]
	internal void MemberTest() => JFieldDefinitionTests.ObjectFieldTest<JMemberObject>();
	[Fact]
	internal void TypeTest() => JFieldDefinitionTests.ObjectFieldTest<JTypeObject>();
	[Fact]
	internal void AnnotationTest() => JFieldDefinitionTests.ObjectFieldTest<JAnnotationObject>();
	[Fact]
	internal void SerializableTest() => JFieldDefinitionTests.ObjectFieldTest<JSerializableObject>();

	private static void ObjectFieldTest<TDataType>() where TDataType : JReferenceObject, IReferenceType<TDataType>
	{
		JFieldDefinitionTests.Test<TDataType>();
		JFieldDefinitionTests.Test<JArrayObject<TDataType>>();
		JFieldDefinitionTests.NonTypedTest<TDataType>();
	}

	private static void Test<TDataType>() where TDataType : JReferenceObject, IReferenceType<TDataType>
	{
		JReferenceTypeMetadata typeMetadata = IReferenceType.GetMetadata<TDataType>();
		String fieldName = JFieldDefinitionTests.fixture.Create<String>();
		CStringSequence seq = new(fieldName, TDataType.Argument.Signature.ToString());
		JFieldDefinition<TDataType> fieldDefinition = new((CString)fieldName);
		JObjectLocalRef localRef0 = JFieldDefinitionTests.fixture.Create<JObjectLocalRef>();
		JObjectLocalRef localRef1 = JFieldDefinitionTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef0 = JFieldDefinitionTests.fixture.Create<JClassLocalRef>();
		JClassLocalRef classRef1 = JFieldDefinitionTests.fixture.Create<JClassLocalRef>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClassClass = new(env);
		using JClassObject jClass = new(jClassClass, typeMetadata, classRef0);
		using JClassObject jFieldClass = new(jClass, IDataType.GetMetadata<JFieldObject>(), classRef1);
		using TDataType? instance =
			typeMetadata.Signature[0] != UnicodeObjectSignatures.ArraySignaturePrefixChar &&
			!typeMetadata.ClassName.AsSpan().SequenceEqual(UnicodeClassNames.ClassObject) &&
			!typeMetadata.ClassName.AsSpan().SequenceEqual(UnicodeClassNames.StringObject()) ?
				Assert.IsType<TDataType>(
					typeMetadata.ParseInstance(typeMetadata.CreateInstance(jClass, localRef0, true))) :
				default;
		using JFieldObject jField = new(jFieldClass, localRef1, fieldDefinition, jClassClass);

		env.AccessFeature.GetField<TDataType>((JLocalObject)jField, Arg.Any<JClassObject>(), fieldDefinition)
		   .Returns(instance);
		env.AccessFeature.GetField<TDataType>(jField, (JLocalObject)jClass, fieldDefinition).Returns(instance);
		env.AccessFeature.GetStaticField<TDataType>(jClass, fieldDefinition).Returns(instance);
		env.AccessFeature.GetStaticField<TDataType>(jField, fieldDefinition).Returns(instance);
		env.AccessFeature.GetReflectedField(fieldDefinition, jClassClass, Arg.Any<Boolean>()).Returns(jField);

		Assert.Equal(seq.ToString(), fieldDefinition.Information.ToString());
		Assert.Equal(fieldDefinition.Information.GetHashCode(), fieldDefinition.GetHashCode());

		Assert.False(fieldDefinition.Equals(default));
		Assert.True(fieldDefinition.Equals((Object)fieldDefinition));
		Assert.True(fieldDefinition.Equals((Object)new JFieldDefinition<TDataType>(fieldDefinition)));
		Assert.True(fieldDefinition.Equals(
			            (Object)new JNonTypedFieldDefinition((CString)fieldName, TDataType.Argument.Signature)));
		Assert.Equal(jField, fieldDefinition.GetReflected(jClassClass));
		Assert.Equal(jField, fieldDefinition.GetStaticReflected(jClassClass));

		Assert.Equal($"{{ Field: {fieldName} Descriptor: {TDataType.Argument.Signature} }}",
		             fieldDefinition.ToString());

		Assert.Equal(instance, fieldDefinition.Get(jField));
		Assert.Equal(instance, fieldDefinition.Get(jField, jClassClass));
		Assert.Equal(instance, fieldDefinition.GetReflected(jField, jClass));
		Assert.Equal(instance, fieldDefinition.StaticGet(jClass));
		Assert.Equal(instance, fieldDefinition.StaticGetReflected(jField));

		env.AccessFeature.Received(1).GetField<TDataType>((JLocalObject)jField, jFieldClass, fieldDefinition);
		env.AccessFeature.Received(1).GetField<TDataType>((JLocalObject)jField, jClassClass, fieldDefinition);
		env.AccessFeature.Received(1).GetField<TDataType>(jField, (JLocalObject)jClass, fieldDefinition);
		env.AccessFeature.Received(1).GetStaticField<TDataType>(jClass, fieldDefinition);
		env.AccessFeature.Received(1).GetStaticField<TDataType>(jField, fieldDefinition);

		env.AccessFeature.Received(1).GetReflectedField(fieldDefinition, jClassClass, true);
		env.AccessFeature.Received(1).GetReflectedField(fieldDefinition, jClassClass, false);

		fieldDefinition.Set(jField, instance);
		fieldDefinition.Set(jField, instance, jClassClass);
		fieldDefinition.SetReflected(jField, jClass, instance);
		fieldDefinition.StaticSet(jClass, instance);
		fieldDefinition.StaticSetReflected(jField, instance);

		env.AccessFeature.Received(1).SetField((JLocalObject)jField, jFieldClass, fieldDefinition, instance);
		env.AccessFeature.Received(1).SetField((JLocalObject)jField, jClassClass, fieldDefinition, instance);
		env.AccessFeature.Received(1).SetField(jField, (JLocalObject)jClass, fieldDefinition, instance);
		env.AccessFeature.Received(1).SetStaticField(jClass, fieldDefinition, instance);
		env.AccessFeature.Received(1).SetStaticField(jField, fieldDefinition, instance);
	}
	private static void NonTypedTest<TDataType>() where TDataType : JReferenceObject, IReferenceType<TDataType>
	{
		String fieldName = JFieldDefinitionTests.fixture.Create<String>();
		CStringSequence seq = new(fieldName, TDataType.Argument.Signature.ToString());
		JNonTypedFieldDefinition fieldDefinition = new((CString)fieldName, TDataType.Argument.Signature);
		JObjectLocalRef localRef = JFieldDefinitionTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef0 = JFieldDefinitionTests.fixture.Create<JClassLocalRef>();
		JClassLocalRef classRef1 = JFieldDefinitionTests.fixture.Create<JClassLocalRef>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClassClass = new(env);
		using JClassObject jClass = new(jClassClass, IDataType.GetMetadata<TDataType>(), classRef0);
		using JClassObject jFieldClass = new(jClassClass, IDataType.GetMetadata<JFieldObject>(), classRef1);
		using JFieldObject jField = new(jFieldClass, localRef, fieldDefinition, jClassClass);

		Assert.Equal(seq.ToString(), fieldDefinition.Information.ToString());
		Assert.Equal(fieldDefinition.Information.GetHashCode(), fieldDefinition.GetHashCode());

		Assert.False(fieldDefinition.Equals(default));
		Assert.True(fieldDefinition.Equals((Object)fieldDefinition));
		Assert.True(fieldDefinition.Equals(
			            (Object)new JNonTypedFieldDefinition((CString)fieldName, TDataType.Argument.Signature)));
		Assert.True(fieldDefinition.Equals((Object)new JFieldDefinition<TDataType>(fieldDefinition)));

		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(fieldDefinition.Get(jField));
		env.AccessFeature.Received(1).GetField<JLocalObject>((JLocalObject)jField, jFieldClass, fieldDefinition);

		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(fieldDefinition.Get(jField, jClass));
		env.AccessFeature.Received(1).GetField<JLocalObject>((JLocalObject)jField, jClass, fieldDefinition);

		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(fieldDefinition.GetReflected(jField, jClass));
		env.AccessFeature.Received(1).GetField<JLocalObject>(jField, (JLocalObject)jClass, fieldDefinition);

		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(fieldDefinition.StaticGet(jClassClass));
		env.AccessFeature.Received(1).GetStaticField<JLocalObject>(jClassClass, fieldDefinition);

		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(fieldDefinition.StaticGetReflected(jField));
		env.AccessFeature.Received(1).GetStaticField<JLocalObject>(jField, fieldDefinition);

		env.AccessFeature.ClearReceivedCalls();
		fieldDefinition.Set(jField, default);
		env.AccessFeature.Received(1)
		   .SetField<JLocalObject>((JLocalObject)jField, jFieldClass, fieldDefinition, default);

		env.AccessFeature.ClearReceivedCalls();
		fieldDefinition.Set(jField, default, jClass);
		env.AccessFeature.Received(1).SetField<JLocalObject>((JLocalObject)jField, jClass, fieldDefinition, default);

		env.AccessFeature.ClearReceivedCalls();
		fieldDefinition.SetReflected(jField, jClass, default);
		env.AccessFeature.Received(1).SetField<JLocalObject>(jField, (JLocalObject)jClass, fieldDefinition, default);

		env.AccessFeature.ClearReceivedCalls();
		fieldDefinition.StaticSet(jClassClass, default);
		env.AccessFeature.Received(1).SetStaticField<JLocalObject>(jClassClass, fieldDefinition, default);

		env.AccessFeature.ClearReceivedCalls();
		fieldDefinition.StaticSetReflected(jField, default);
		env.AccessFeature.Received(1).SetStaticField<JLocalObject>(jField, fieldDefinition, default);
	}
}