namespace Rxmxnx.JNetInterface.Tests.Native.Access.IndeterminateFieldTests;

[ExcludeFromCodeCoverage]
[SuppressMessage("csharpsquid", "S2699")]
public sealed class GetTests : IndeterminateAccessTestsBase
{
	private static readonly MethodInfo objectTestInfo =
		typeof(GetTests).GetMethod(nameof(GetTests.ObjectTest), BindingFlags.Static | BindingFlags.NonPublic)!;
	private static readonly MethodInfo primitiveTestInfo =
		typeof(GetTests).GetMethod(nameof(GetTests.PrimitiveTest), BindingFlags.Static | BindingFlags.NonPublic)!;
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void BooleanTest() => GetTests.Test<JBoolean>();
	[Fact]
	internal void ByteTest() => GetTests.Test<JByte>();
	[Fact]
	internal void CharTest() => GetTests.Test<JChar>();
	[Fact]
	internal void DoubleTest() => GetTests.Test<JDouble>();
	[Fact]
	internal void FloatTest() => GetTests.Test<JFloat>();
	[Fact]
	internal void IntTest() => GetTests.Test<JInt>();
	[Fact]
	internal void LongTest() => GetTests.Test<JLong>();
	[Fact]
	internal void ShortTest() => GetTests.Test<JShort>();

	[Fact]
	internal void JavaObjectTest() => GetTests.Test<JLocalObject>();
	[Fact]
	internal void ClassTest() => GetTests.Test<JClassObject>();
	[Fact]
	internal void StringTest() => GetTests.Test<JStringObject>();
	[Fact]
	internal void ThrowableTest() => GetTests.Test<JThrowableObject>();
	[Fact]
	internal void EnumTest() => GetTests.Test<JEnumObject>();
	[Fact]
	internal void NumberTest() => GetTests.Test<JNumberObject>();
	[Fact]
	internal void AccessibleObjectTest() => GetTests.Test<JAccessibleObject>();
	[Fact]
	internal void ExecutableTest() => GetTests.Test<JExecutableObject>();
	[Fact]
	internal void ModifierTest() => GetTests.Test<JModifierObject>();
	[Fact]
	internal void ProxyTest() => GetTests.Test<JProxyObject>();

	[Fact]
	internal void ErrorTest() => GetTests.Test<JErrorObject>();
	[Fact]
	internal void ExceptionTest() => GetTests.Test<JExceptionObject>();
	[Fact]
	internal void RuntimeExceptionTest() => GetTests.Test<JRuntimeExceptionObject>();
	[Fact]
	internal void BufferTest() => GetTests.Test<JBufferObject>();

	[Fact]
	internal void FieldTest() => GetTests.Test<JFieldObject>();
	[Fact]
	internal void MethodTest() => GetTests.Test<JMethodObject>();
	[Fact]
	internal void ConstructorTest() => GetTests.Test<JConstructorObject>();
	[Fact]
	internal void StackTraceElementTest() => GetTests.Test<JStackTraceElementObject>();

	[Fact]
	internal void BooleanObjectTest() => GetTests.Test<JBoolean>();
	[Fact]
	internal void ByteObjectTest() => GetTests.Test<JByteObject>();
	[Fact]
	internal void CharacterObjectTest() => GetTests.Test<JCharacterObject>();
	[Fact]
	internal void DoubleObjectTest() => GetTests.Test<JDoubleObject>();
	[Fact]
	internal void FloatObjectTest() => GetTests.Test<JFloatObject>();
	[Fact]
	internal void IntegerObjectTest() => GetTests.Test<JIntegerObject>();
	[Fact]
	internal void LongObjectTest() => GetTests.Test<JLongObject>();
	[Fact]
	internal void ShortObjectTest() => GetTests.Test<JShortObject>();

	private static void Test<TDataType>() where TDataType : IDataType<TDataType>
	{
		Type typeofT = typeof(TDataType);
		MethodInfo methodInfo = typeofT.IsValueType ?
			GetTests.primitiveTestInfo.MakeGenericMethod(typeofT) :
			GetTests.objectTestInfo.MakeGenericMethod(typeofT);
		Action action = methodInfo.CreateDelegate<Action>();
		action();
	}
	private static void ObjectTest<TObject>() where TObject : JReferenceObject, IReferenceType<TObject>, ILocalObject
	{
		ReadOnlySpan<Byte> fieldName = (CString)GetTests.fixture.Create<String>();
		JFieldDefinition definition = new JFieldDefinition<TObject>(fieldName);
		IndeterminateField field = IndeterminateField.Create<TObject>(fieldName);
		JReferenceTypeMetadata typeMetadata = IReferenceType.GetMetadata<TObject>();
		Boolean isAbstract = typeMetadata.Modifier == JTypeModifier.Abstract;
		IndeterminateField nonGenericField = IndeterminateField.Create(typeMetadata.ArgumentMetadata, fieldName);
		JObjectLocalRef localRef = GetTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = GetTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef stringRef = GetTests.fixture.Create<JStringLocalRef>();
		String textValue = GetTests.fixture.Create<String>();

		Assert.IsType<JFieldDefinition<TObject>>(field.Definition);
		Assert.IsType<JNonTypedFieldDefinition>(nonGenericField.Definition);

		Assert.Equal(field.Definition, nonGenericField.Definition);
		Assert.Equal(field.Definition.Name, nonGenericField.Definition.Name);
		Assert.Equal(field.Definition.Descriptor, nonGenericField.Definition.Descriptor);
		Assert.Equal(field.Definition.Hash, nonGenericField.Definition.Hash);

		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClassClass = new(env);
		using JClassObject jClass = new(jClassClass, typeMetadata, classRef);
		using JClassObject jStringClass = !typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/String"u8) ?
			new(jClassClass, IClassType.GetMetadata<JStringObject>(), GetTests.fixture.Create<JClassLocalRef>()) :
			jClass;
		using JClassObject jFieldClass = !typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/Field"u8) ?
			new(jClassClass, IClassType.GetMetadata<JFieldObject>(), GetTests.fixture.Create<JClassLocalRef>()) :
			jClass;
		using JStringObject jString = new(jStringClass, stringRef, textValue);
		using JFieldObject jField = new(jFieldClass, GetTests.fixture.Create<JObjectLocalRef>(), definition, jClass);

		using TObject? instance =
			!isAbstract && !typeMetadata.ClassName.AsSpan().SequenceEqual(CommonNames.ClassObject) &&
			!typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/String"u8) ?
				Assert.IsType<TObject>(
					typeMetadata.ParseInstance(typeMetadata.CreateInstance(jClass, localRef, true))) :
				default;

		Assert.Equal(definition, field.Definition);
		Assert.Equal(definition, nonGenericField.Definition);
		Assert.Equal(typeMetadata.Signature, field.FieldType);

		env.ClassFeature.GetClass<TObject>().Returns(jClass);
		env.AccessFeature.GetField<JLocalObject>(Arg.Any<JLocalObject>(), Arg.Any<JClassObject>(), field.Definition)
		   .Returns(instance?.CastTo<JLocalObject>());
		env.AccessFeature.GetField<JLocalObject>(Arg.Any<JFieldObject>(), Arg.Any<JLocalObject>(), field.Definition)
		   .Returns(instance?.CastTo<JLocalObject>());
		env.AccessFeature.GetStaticField<JLocalObject>(Arg.Any<JClassObject>(), field.Definition)
		   .Returns(instance?.CastTo<JLocalObject>());
		env.AccessFeature.GetStaticField<JLocalObject>(Arg.Any<JFieldObject>(), field.Definition)
		   .Returns(instance?.CastTo<JLocalObject>());
		env.ReferenceFeature.Unload(Arg.Any<JLocalObject>()).Returns(true);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateResult result = new(instance?.CastTo<JLocalObject>(), typeMetadata.Signature);

		IndeterminateAccessTestsBase.Compare(result, field.Get(jString));
		env.AccessFeature.Received(1).GetField<JLocalObject>(jString, jString.Class, field.Definition);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateAccessTestsBase.Compare(result, field.Get(jString, jClassClass));
		env.AccessFeature.Received(1).GetField<JLocalObject>(jString, jClassClass, field.Definition);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateAccessTestsBase.Compare(result, field.StaticGet(jStringClass));
		env.AccessFeature.Received(1).GetStaticField<JLocalObject>(jStringClass, field.Definition);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateAccessTestsBase.Compare(result, IndeterminateField.ReflectedGet(jField, jString));
		env.AccessFeature.Received(1).GetField<JLocalObject>(jField, jString, field.Definition);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateAccessTestsBase.Compare(result, IndeterminateField.ReflectedStaticGet(jField));
		env.AccessFeature.Received(1).GetStaticField<JLocalObject>(jField, field.Definition);
	}
	private static void PrimitiveTest<TPrimitive>() where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		ReadOnlySpan<Byte> fieldName = (CString)GetTests.fixture.Create<String>();
		JFieldDefinition definition = new JFieldDefinition<TPrimitive>(fieldName);
		IndeterminateField field = IndeterminateField.Create<TPrimitive>(fieldName);
		JPrimitiveTypeMetadata typeMetadata = IPrimitiveType.GetMetadata<TPrimitive>();
		IndeterminateField nonGenericField = IndeterminateField.Create(typeMetadata.ArgumentMetadata, fieldName);
		TPrimitive[] primitiveArray = GetTests.fixture.CreatePrimitiveArray<TPrimitive>(1);
		JStringLocalRef stringRef = GetTests.fixture.Create<JStringLocalRef>();
		String textValue = GetTests.fixture.Create<String>();

		Assert.IsType<JFieldDefinition<TPrimitive>>(field.Definition);
		Assert.IsType<JFieldDefinition<TPrimitive>>(nonGenericField.Definition);

		Assert.Equal(field.Definition, nonGenericField.Definition);
		Assert.Equal(field.Definition.Name, nonGenericField.Definition.Name);
		Assert.Equal(field.Definition.Descriptor, nonGenericField.Definition.Descriptor);
		Assert.Equal(field.Definition.Hash, nonGenericField.Definition.Hash);

		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClassClass = new(env);
		using JClassObject jStringClass = new(jClassClass, IClassType.GetMetadata<JStringObject>(),
		                                      GetTests.fixture.Create<JClassLocalRef>());
		using JClassObject jFieldClass = new(jClassClass, IClassType.GetMetadata<JFieldObject>(),
		                                     GetTests.fixture.Create<JClassLocalRef>());
		using JStringObject jString = new(jStringClass, stringRef, textValue);
		using JFieldObject jField = new(jFieldClass, GetTests.fixture.Create<JObjectLocalRef>(), definition,
		                                jClassClass);

		Assert.Equal(definition, field.Definition);
		Assert.Equal(definition, nonGenericField.Definition);
		Assert.Equal(typeMetadata.Signature, field.FieldType);

		env.AccessFeature.When(a => a.GetPrimitiveField(Arg.Any<IFixedMemory>(), Arg.Any<JLocalObject>(),
		                                                Arg.Any<JClassObject>(), field.Definition)).Do(c =>
		{
			IFixedMemory mem = (IFixedMemory)c[0];
			primitiveArray.AsSpan().AsBytes().CopyTo(mem.Bytes);
		});
		env.AccessFeature
		   .When(a => a.GetPrimitiveStaticField(Arg.Any<IFixedMemory>(), Arg.Any<JClassObject>(), field.Definition))
		   .Do(c =>
		   {
			   IFixedMemory mem = (IFixedMemory)c[0];
			   primitiveArray.AsSpan().AsBytes().CopyTo(mem.Bytes);
		   });
		env.AccessFeature.GetField<TPrimitive>(Arg.Any<JFieldObject>(), Arg.Any<JLocalObject>(), field.Definition)
		   .Returns(primitiveArray[0]);
		env.AccessFeature.GetStaticField<TPrimitive>(Arg.Any<JFieldObject>(), field.Definition)
		   .Returns(primitiveArray[0]);

		Span<Byte> bytes = stackalloc Byte[sizeof(Int64)];
		primitiveArray.AsSpan().AsBytes().CopyTo(bytes);

		IndeterminateResult result = new(MemoryMarshal.Cast<Byte, JValue.PrimitiveValue>(bytes)[0],
		                                 typeMetadata.Signature);
		IndeterminateAccessTestsBase.Compare(result, field.Get(jString));
		env.AccessFeature.Received(1).GetPrimitiveField(Arg.Any<IFixedMemory>(), jString, jString.Class,
		                                                field.Definition);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateAccessTestsBase.Compare(result, field.Get(jString, jClassClass));
		env.AccessFeature.Received(1).GetPrimitiveField(Arg.Any<IFixedMemory>(), jString, jClassClass,
		                                                field.Definition);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateAccessTestsBase.Compare(result, field.StaticGet(jStringClass));
		env.AccessFeature.Received(1).GetPrimitiveStaticField(Arg.Any<IFixedMemory>(), jStringClass, field.Definition);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateAccessTestsBase.Compare(result, IndeterminateField.ReflectedGet(jField, jString));
		env.AccessFeature.Received(1).GetField<TPrimitive>(jField, jString, jField.Definition);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateAccessTestsBase.Compare(result, IndeterminateField.ReflectedStaticGet(jField));
		env.AccessFeature.Received(1).GetStaticField<TPrimitive>(jField, jField.Definition);
	}
}