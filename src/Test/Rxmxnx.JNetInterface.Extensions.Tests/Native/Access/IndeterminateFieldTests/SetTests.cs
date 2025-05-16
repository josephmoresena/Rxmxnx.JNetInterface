namespace Rxmxnx.JNetInterface.Tests.Native.Access.IndeterminateFieldTests;

[ExcludeFromCodeCoverage]
[SuppressMessage("csharpsquid", "S2699")]
public sealed class SetTests : IndeterminateAccessTestsBase
{
	private static readonly MethodInfo objectTestInfo =
		typeof(SetTests).GetMethod(nameof(SetTests.ObjectTest), BindingFlags.Static | BindingFlags.NonPublic)!;
	private static readonly MethodInfo referenceObjectTestInfo =
		typeof(SetTests).GetMethod(nameof(SetTests.ReferenceObjectTest), BindingFlags.Static | BindingFlags.NonPublic)!;
	private static readonly MethodInfo primitiveTestInfo =
		typeof(SetTests).GetMethod(nameof(SetTests.PrimitiveTest), BindingFlags.Static | BindingFlags.NonPublic)!;
	private static readonly MethodInfo primitiveFieldWrapperTestInfo =
		typeof(SetTests).GetMethod(nameof(SetTests.PrimitiveFieldWrapperTest),
		                           BindingFlags.Static | BindingFlags.NonPublic)!;
	private static readonly MethodInfo wrapperFieldPrimitiveTestInfo =
		typeof(SetTests).GetMethod(nameof(SetTests.WrapperFieldPrimitiveTest),
		                           BindingFlags.Static | BindingFlags.NonPublic)!;
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void BooleanTest() => SetTests.Test<JBoolean>();
	[Fact]
	internal void ByteTest() => SetTests.Test<JByte>();
	[Fact]
	internal void CharTest() => SetTests.Test<JChar>();
	[Fact]
	internal void DoubleTest() => SetTests.Test<JDouble>();
	[Fact]
	internal void FloatTest() => SetTests.Test<JFloat>();
	[Fact]
	internal void IntTest() => SetTests.Test<JInt>();
	[Fact]
	internal void LongTest() => SetTests.Test<JLong>();
	[Fact]
	internal void ShortTest() => SetTests.Test<JShort>();

	[Fact]
	internal void JavaObjectTest() => SetTests.Test<JLocalObject>();
	[Fact]
	internal void ClassTest() => SetTests.Test<JClassObject>();
	[Fact]
	internal void StringTest() => SetTests.Test<JStringObject>();
	[Fact]
	internal void ThrowableTest() => SetTests.Test<JThrowableObject>();
	[Fact]
	internal void EnumTest() => SetTests.Test<JEnumObject>();
	[Fact]
	internal void NumberTest() => SetTests.Test<JNumberObject>();
	[Fact]
	internal void AccessibleObjectTest() => SetTests.Test<JAccessibleObject>();
	[Fact]
	internal void ExecutableTest() => SetTests.Test<JExecutableObject>();
	[Fact]
	internal void ModifierTest() => SetTests.Test<JModifierObject>();
	[Fact]
	internal void ProxyTest() => SetTests.Test<JProxyObject>();

	[Fact]
	internal void ErrorTest() => SetTests.Test<JErrorObject>();
	[Fact]
	internal void ExceptionTest() => SetTests.Test<JExceptionObject>();
	[Fact]
	internal void RuntimeExceptionTest() => SetTests.Test<JRuntimeExceptionObject>();
	[Fact]
	internal void BufferTest() => SetTests.Test<JBufferObject>();

	[Fact]
	internal void FieldTest() => SetTests.Test<JFieldObject>();
	[Fact]
	internal void MethodTest() => SetTests.Test<JMethodObject>();
	[Fact]
	internal void ConstructorTest() => SetTests.Test<JConstructorObject>();
	[Fact]
	internal void StackTraceElementTest() => SetTests.Test<JStackTraceElementObject>();

	[Fact]
	internal void BooleanObjectTest() => SetTests.Test<JBooleanObject>();
	[Fact]
	internal void ByteObjectTest() => SetTests.Test<JByteObject>();
	[Fact]
	internal void CharacterObjectTest() => SetTests.Test<JCharacterObject>();
	[Fact]
	internal void DoubleObjectTest() => SetTests.Test<JDoubleObject>();
	[Fact]
	internal void FloatObjectTest() => SetTests.Test<JFloatObject>();
	[Fact]
	internal void IntegerObjectTest() => SetTests.Test<JIntegerObject>();
	[Fact]
	internal void LongObjectTest() => SetTests.Test<JLongObject>();
	[Fact]
	internal void ShortObjectTest() => SetTests.Test<JShortObject>();

	private static void Test<TDataType>() where TDataType : IDataType<TDataType>
	{
		Type typeofT = typeof(TDataType);
		MethodInfo methodInfo = typeofT.IsValueType ?
			SetTests.primitiveTestInfo.MakeGenericMethod(typeofT) :
			SetTests.objectTestInfo.MakeGenericMethod(typeofT);
		Action action = methodInfo.CreateDelegate<Action>();

		if (typeof(TDataType).GetInterfaces()
		                     .FirstOrDefault(i => i.IsGenericType &&
			                                     i.GetGenericTypeDefinition() == typeof(IPrimitiveWrapperType<,>))
		                     ?.GetGenericArguments() is { } wrapperTypes)
		{
			action += SetTests.primitiveFieldWrapperTestInfo.MakeGenericMethod([..wrapperTypes.Reverse(),])
			                  .CreateDelegate<Action>();
			action += SetTests.wrapperFieldPrimitiveTestInfo.MakeGenericMethod(wrapperTypes).CreateDelegate<Action>();
		}
		action();
	}
	private static void ObjectTest<TObject>() where TObject : JReferenceObject, IReferenceType<TObject>, ILocalObject
	{
		ReadOnlySpan<Byte> fieldName = (CString)SetTests.fixture.Create<String>();
		JReferenceTypeMetadata typeMetadata = IReferenceType.GetMetadata<TObject>();
		Boolean isAbstract = typeMetadata.Modifier == JTypeModifier.Abstract;
		JObjectLocalRef localRef = SetTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = SetTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef stringRef = SetTests.fixture.Create<JStringLocalRef>();
		String textValue = SetTests.fixture.Create<String>();

		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClassClass = new(env);
		using JClassObject jClass = new(jClassClass, typeMetadata, classRef);
		using JClassObject jArrayClass = new(jClassClass, typeMetadata.GetArrayMetadata()!,
		                                     SetTests.fixture.Create<JClassLocalRef>());
		using JClassObject jStringClass = !typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/String"u8) ?
			new(jClassClass, IClassType.GetMetadata<JStringObject>(), SetTests.fixture.Create<JClassLocalRef>()) :
			jClass;
		using JClassObject jFieldClass = !typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/Field"u8) ?
			new(jClassClass, IClassType.GetMetadata<JFieldObject>(), SetTests.fixture.Create<JClassLocalRef>()) :
			jClass;
		using JStringObject jString = new(jStringClass, stringRef, textValue);

		using TObject? instance =
			!isAbstract && !typeMetadata.ClassName.AsSpan().SequenceEqual(CommonNames.ClassObject) &&
			!typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/String"u8) ?
				Assert.IsType<TObject>(
					typeMetadata.ParseInstance(typeMetadata.CreateInstance(jClass, localRef, true))) :
				default;

		SetTests.ReferenceObjectTest<TObject>(env, fieldName, jClass, jString, jFieldClass, instance);
		foreach (JInterfaceTypeMetadata interfaceTypeMetadata in typeMetadata.Interfaces)
		{
			ReferenceObjectTestDelegate test = SetTests.referenceObjectTestInfo
			                                           .MakeGenericMethod(interfaceTypeMetadata.Type)
			                                           .CreateDelegate<ReferenceObjectTestDelegate>();
			using JClassObject jInterfaceClass =
				new(jClassClass, interfaceTypeMetadata, SetTests.fixture.Create<JClassLocalRef>());
			test(env, fieldName, jInterfaceClass, jString, jFieldClass, instance);
		}

		using JArrayObject<TObject> jArray = new(jArrayClass, SetTests.fixture.Create<JArrayLocalRef>(), 0);
		SetTests.ReferenceObjectTest<JArrayObject<TObject>>(env, fieldName, jArrayClass, jString, jFieldClass,
		                                                    jArrayClass);

		if (instance is null) return;

		using JGlobal jGlobal = new(env.VirtualMachine, new(jClass), SetTests.fixture.Create<JGlobalRef>());

		env.ClassFeature.GetObjectClass(jGlobal.ObjectMetadata).Returns(jClass);
		env.ClassFeature.GetTypeMetadata(jClass).Returns(typeMetadata);
		env.IsValidationAvoidable(Arg.Any<JGlobalBase>()).Returns(true);
		env.GetReferenceType(jGlobal).Returns(JReferenceType.GlobalRefType);
		SetTests.ReferenceObjectTest<TObject>(env, fieldName, jClass, jString, jFieldClass, jGlobal);
	}
	private static void ReferenceObjectTest<TObject>(EnvironmentProxy env, ReadOnlySpan<Byte> fieldName,
		JClassObject jClass, JStringObject jString, JClassObject jFieldClass, JReferenceObject? instance)
		where TObject : JReferenceObject, IReferenceType<TObject>, ILocalObject
	{
		JReferenceTypeMetadata typeMetadata = IReferenceType.GetMetadata<TObject>();
		JClassObject jClassClass = jClass.Class;
		JClassObject jStringClass = jString.Class;
		JLocalObject? localInstance = instance is ILocalObject jl ?
			jl.CastTo<JLocalObject>() :
			(instance as JGlobalBase)?.AsLocal<JLocalObject>(env);

		JFieldDefinition definition = new JFieldDefinition<TObject>(fieldName);
		IndeterminateField field = IndeterminateField.Create<TObject>(fieldName);
		IndeterminateField nonGenericField = IndeterminateField.Create(typeMetadata.ArgumentMetadata, fieldName);

		Assert.IsType<JFieldDefinition<TObject>>(field.Definition);
		Assert.IsType<JNonTypedFieldDefinition>(nonGenericField.Definition);

		Assert.Equal(field.Definition, nonGenericField.Definition);
		Assert.Equal(field.Definition.Name, nonGenericField.Definition.Name);
		Assert.Equal(field.Definition.Descriptor, nonGenericField.Definition.Descriptor);
		Assert.Equal(field.Definition.Hash, nonGenericField.Definition.Hash);

		Assert.Equal(definition, field.Definition);
		Assert.Equal(definition, nonGenericField.Definition);
		Assert.Equal(typeMetadata.Signature, field.FieldType);

		env.ClassFeature.GetClass<TObject>().Returns(jClass);
		env.ReferenceFeature.Unload(Arg.Any<JLocalObject>()).Returns(true);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		field.Set(jString, instance);
		env.AccessFeature.Received(1).SetField(jString, jString.Class, field.Definition, localInstance);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		field.Set(jString, jClassClass, instance);
		env.AccessFeature.Received(1).SetField(jString, jClassClass, field.Definition, localInstance);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		field.StaticSet(jStringClass, instance);
		env.AccessFeature.Received(1).SetStaticField(jStringClass, field.Definition, localInstance);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		using JFieldObject jField = new(jFieldClass, SetTests.fixture.Create<JObjectLocalRef>(), definition, jClass);

		IndeterminateField.ReflectedSet(jField, jString, instance);
		env.AccessFeature.Received(1).SetField(jField, jString, field.Definition, localInstance);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateField.ReflectedStaticSet(jField, instance);
		env.AccessFeature.Received(1).SetStaticField(jField, field.Definition, localInstance);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
	}
	private static unsafe void PrimitiveTest<TPrimitive>() where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		ReadOnlySpan<Byte> fieldName = (CString)SetTests.fixture.Create<String>();
		JFieldDefinition definition = new JFieldDefinition<TPrimitive>(fieldName);
		IndeterminateField field = IndeterminateField.Create<TPrimitive>(fieldName);
		JPrimitiveTypeMetadata typeMetadata = IPrimitiveType.GetMetadata<TPrimitive>();
		IndeterminateField nonGenericField = IndeterminateField.Create(typeMetadata.ArgumentMetadata, fieldName);
		TPrimitive[] primitiveArray = SetTests.fixture.CreatePrimitiveArray<TPrimitive>(1);
		JObject primitiveObject = primitiveArray[0];
		JStringLocalRef stringRef = SetTests.fixture.Create<JStringLocalRef>();
		String textValue = SetTests.fixture.Create<String>();

		Assert.IsType<JFieldDefinition<TPrimitive>>(field.Definition);
		Assert.IsType<JFieldDefinition<TPrimitive>>(nonGenericField.Definition);

		Assert.Equal(field.Definition, nonGenericField.Definition);
		Assert.Equal(field.Definition.Name, nonGenericField.Definition.Name);
		Assert.Equal(field.Definition.Descriptor, nonGenericField.Definition.Descriptor);
		Assert.Equal(field.Definition.Hash, nonGenericField.Definition.Hash);

		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClassClass = new(env);
		using JClassObject jStringClass = new(jClassClass, IClassType.GetMetadata<JStringObject>(),
		                                      SetTests.fixture.Create<JClassLocalRef>());
		using JClassObject jFieldClass = new(jClassClass, IClassType.GetMetadata<JFieldObject>(),
		                                     SetTests.fixture.Create<JClassLocalRef>());
		using JStringObject jString = new(jStringClass, stringRef, textValue);
		using JFieldObject jField = new(jFieldClass, SetTests.fixture.Create<JObjectLocalRef>(), definition,
		                                jClassClass);

		Assert.Equal(definition, field.Definition);
		Assert.Equal(definition, nonGenericField.Definition);
		Assert.Equal(typeMetadata.Signature, field.FieldType);

		env.AccessFeature.When(a => a.SetPrimitiveField(Arg.Any<JLocalObject>(), Arg.Any<JClassObject>(),
		                                                field.Definition, Arg.Any<IReadOnlyFixedMemory>())).Do(c =>
		{
			IReadOnlyFixedMemory mem = (IReadOnlyFixedMemory)c[3];
			Assert.True(primitiveArray.AsSpan().AsBytes().SequenceEqual(mem.Bytes[..sizeof(TPrimitive)]));
		});
		env.AccessFeature
		   .When(a => a.SetPrimitiveStaticField(Arg.Any<JClassObject>(), field.Definition,
		                                        Arg.Any<IReadOnlyFixedMemory>())).Do(c =>
		   {
			   IReadOnlyFixedMemory mem = (IReadOnlyFixedMemory)c[2];
			   Assert.True(primitiveArray.AsSpan().AsBytes().SequenceEqual(mem.Bytes[..sizeof(TPrimitive)]));
		   });

		field.Set(jString, primitiveArray[0]);
		env.AccessFeature.Received(1).SetPrimitiveField(jString, jString.Class,
		                                                field.Definition, Arg.Any<IReadOnlyFixedMemory>());

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		field.Set(jString, jClassClass, primitiveArray[0]);
		env.AccessFeature.Received(1).SetPrimitiveField(jString, jClassClass,
		                                                field.Definition, Arg.Any<IReadOnlyFixedMemory>());

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		field.StaticSet(jStringClass, primitiveArray[0]);
		env.AccessFeature.Received(1)
		   .SetPrimitiveStaticField(jStringClass, field.Definition, Arg.Any<IReadOnlyFixedMemory>());

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateField.ReflectedSet(jField, jString, primitiveArray[0]);
		env.AccessFeature.Received(1).SetField(jField, jString, jField.Definition, primitiveArray[0]);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateField.ReflectedStaticSet(jField, primitiveArray[0]);
		env.AccessFeature.Received(1).SetStaticField(jField, jField.Definition, primitiveArray[0]);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		field.Set(jString, primitiveObject);
		env.AccessFeature.Received(1).SetPrimitiveField(jString, jString.Class,
		                                                field.Definition, Arg.Any<IReadOnlyFixedMemory>());

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		field.Set(jString, jClassClass, primitiveObject);
		env.AccessFeature.Received(1).SetPrimitiveField(jString, jClassClass,
		                                                field.Definition, Arg.Any<IReadOnlyFixedMemory>());

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		field.StaticSet(jStringClass, primitiveObject);
		env.AccessFeature.Received(1)
		   .SetPrimitiveStaticField(jStringClass, field.Definition, Arg.Any<IReadOnlyFixedMemory>());

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateField.ReflectedSet(jField, jString, primitiveObject);
		env.AccessFeature.Received(1).SetField(jField, jString, jField.Definition, primitiveArray[0]);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateField.ReflectedStaticSet(jField, primitiveObject);
		env.AccessFeature.Received(1).SetStaticField(jField, jField.Definition, primitiveArray[0]);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		Assert.ThrowsAny<InvalidCastException>(() => field.Set(jString, jClassClass));
		env.AccessFeature.Received(0).SetPrimitiveField(jString, jString.Class,
		                                                field.Definition, Arg.Any<IReadOnlyFixedMemory>());

		Assert.ThrowsAny<InvalidCastException>(() => field.Set(jString, jClassClass, jClassClass));
		env.AccessFeature.Received(0).SetPrimitiveField(jString, jClassClass,
		                                                field.Definition, Arg.Any<IReadOnlyFixedMemory>());

		Assert.ThrowsAny<InvalidCastException>(() => field.StaticSet(jStringClass, jClassClass));
		env.AccessFeature.Received(0)
		   .SetPrimitiveStaticField(jStringClass, field.Definition, Arg.Any<IReadOnlyFixedMemory>());

		Assert.ThrowsAny<InvalidCastException>(() => IndeterminateField.ReflectedSet(jField, jString, jClassClass));
		env.AccessFeature.Received(0).SetField(jField, jString, jField.Definition, Arg.Any<TPrimitive>());

		Assert.ThrowsAny<InvalidCastException>(() => IndeterminateField.ReflectedStaticSet(jField, jClassClass));
		env.AccessFeature.Received(0).SetStaticField(jField, jField.Definition, Arg.Any<TPrimitive>());
	}
	private static unsafe void PrimitiveFieldWrapperTest<TPrimitive, TWrapper>()
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		where TWrapper : JLocalObject, IPrimitiveWrapperType<TWrapper, TPrimitive>
	{
		ReadOnlySpan<Byte> fieldName = (CString)SetTests.fixture.Create<String>();
		JFieldDefinition definition = new JFieldDefinition<TPrimitive>(fieldName);
		IndeterminateField field = IndeterminateField.Create<TPrimitive>(fieldName);
		JPrimitiveTypeMetadata primitiveTypeMetadata = IPrimitiveType.GetMetadata<TPrimitive>();
		JClassTypeMetadata wrapperTypeMetadata = IClassType.GetMetadata<TWrapper>();
		IndeterminateField nonGenericField =
			IndeterminateField.Create(primitiveTypeMetadata.ArgumentMetadata, fieldName);
		TPrimitive[] primitiveArray = SetTests.fixture.CreatePrimitiveArray<TPrimitive>(1);
		JObjectLocalRef localRef = SetTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = SetTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef stringRef = SetTests.fixture.Create<JStringLocalRef>();
		String textValue = SetTests.fixture.Create<String>();

		Assert.IsType<JFieldDefinition<TPrimitive>>(field.Definition);
		Assert.IsType<JFieldDefinition<TPrimitive>>(nonGenericField.Definition);

		Assert.Equal(field.Definition, nonGenericField.Definition);
		Assert.Equal(field.Definition.Name, nonGenericField.Definition.Name);
		Assert.Equal(field.Definition.Descriptor, nonGenericField.Definition.Descriptor);
		Assert.Equal(field.Definition.Hash, nonGenericField.Definition.Hash);

		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClassClass = new(env);
		using JClassObject jStringClass = new(jClassClass, IClassType.GetMetadata<JStringObject>(),
		                                      SetTests.fixture.Create<JClassLocalRef>());
		using JClassObject jWrapperClass = new(jClassClass, wrapperTypeMetadata, classRef);
		using JClassObject jFieldClass = new(jClassClass, IClassType.GetMetadata<JFieldObject>(),
		                                     SetTests.fixture.Create<JClassLocalRef>());
		using JStringObject jString = new(jStringClass, stringRef, textValue);
		using JFieldObject jField = new(jFieldClass, SetTests.fixture.Create<JObjectLocalRef>(), definition,
		                                jClassClass);

		using TWrapper instance = Assert.IsType<TWrapper>(
			wrapperTypeMetadata.ParseInstance(wrapperTypeMetadata.CreateInstance(jWrapperClass, localRef, true)));

		Assert.Equal(definition, field.Definition);
		Assert.Equal(definition, nonGenericField.Definition);
		Assert.Equal(primitiveTypeMetadata.Signature, field.FieldType);

		env.AccessFeature.When(a => a.SetPrimitiveField(Arg.Any<JLocalObject>(), Arg.Any<JClassObject>(),
		                                                field.Definition, Arg.Any<IReadOnlyFixedMemory>())).Do(c =>
		{
			IReadOnlyFixedMemory mem = (IReadOnlyFixedMemory)c[3];
			Assert.True(primitiveArray.AsSpan().AsBytes().SequenceEqual(mem.Bytes[..sizeof(TPrimitive)]));
		});
		env.AccessFeature
		   .When(a => a.SetPrimitiveStaticField(Arg.Any<JClassObject>(), field.Definition,
		                                        Arg.Any<IReadOnlyFixedMemory>())).Do(c =>
		   {
			   IReadOnlyFixedMemory mem = (IReadOnlyFixedMemory)c[2];
			   Assert.True(primitiveArray.AsSpan().AsBytes().SequenceEqual(mem.Bytes[..sizeof(TPrimitive)]));
		   });
		env.FunctionSet.GetPrimitiveValue<JByte>(Arg.Any<JNumberObject>())
		   .Returns(c => (JByte)IPrimitiveType.GetMetadata<JByte>().CreateInstance(primitiveArray.AsSpan().AsBytes()));
		env.FunctionSet.GetPrimitiveValue<JDouble>(Arg.Any<JNumberObject>())
		   .Returns(c => (JDouble)IPrimitiveType.GetMetadata<JDouble>()
		                                        .CreateInstance(primitiveArray.AsSpan().AsBytes()));
		env.FunctionSet.GetPrimitiveValue<JFloat>(Arg.Any<JNumberObject>())
		   .Returns(c => (JFloat)IPrimitiveType.GetMetadata<JFloat>()
		                                       .CreateInstance(primitiveArray.AsSpan().AsBytes()));
		env.FunctionSet.GetPrimitiveValue<JInt>(Arg.Any<JNumberObject>())
		   .Returns(c => (JInt)IPrimitiveType.GetMetadata<JInt>().CreateInstance(primitiveArray.AsSpan().AsBytes()));
		env.FunctionSet.GetPrimitiveValue<JLong>(Arg.Any<JNumberObject>())
		   .Returns(c => (JLong)IPrimitiveType.GetMetadata<JLong>().CreateInstance(primitiveArray.AsSpan().AsBytes()));
		env.FunctionSet.GetPrimitiveValue<JShort>(Arg.Any<JNumberObject>())
		   .Returns(c => (JShort)IPrimitiveType.GetMetadata<JShort>()
		                                       .CreateInstance(primitiveArray.AsSpan().AsBytes()));
		env.AccessFeature
		   .CallFunction<JBoolean>(Arg.Any<JLocalObject>(), Arg.Any<JClassObject>(),
		                           NativeFunctionSetImpl.BooleanValueDefinition, false, [])
		   .Returns(c => (JBoolean)IPrimitiveType.GetMetadata<JBoolean>()
		                                         .CreateInstance(primitiveArray.AsSpan().AsBytes()));
		env.AccessFeature
		   .CallFunction<JChar>(Arg.Any<JLocalObject>(), Arg.Any<JClassObject>(),
		                        NativeFunctionSetImpl.CharValueDefinition, false, [])
		   .Returns(c => (JChar)IPrimitiveType.GetMetadata<JChar>().CreateInstance(primitiveArray.AsSpan().AsBytes()));

		field.Set(jString, instance);
		env.AccessFeature.Received(1).SetPrimitiveField(jString, jString.Class,
		                                                field.Definition, Arg.Any<IReadOnlyFixedMemory>());

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		field.Set(jString, jClassClass, instance);
		env.AccessFeature.Received(1).SetPrimitiveField(jString, jClassClass,
		                                                field.Definition, Arg.Any<IReadOnlyFixedMemory>());

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		field.StaticSet(jStringClass, instance);
		env.AccessFeature.Received(1)
		   .SetPrimitiveStaticField(jStringClass, field.Definition, Arg.Any<IReadOnlyFixedMemory>());

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateField.ReflectedSet(jField, jString, instance);
		env.AccessFeature.Received(1).SetField(jField, jString, jField.Definition, primitiveArray[0]);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateField.ReflectedStaticSet(jField, instance);
		env.AccessFeature.Received(1).SetStaticField(jField, jField.Definition, primitiveArray[0]);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		using JLocalObject jLocalInstance = new(instance);
		using JWeak jWeak = new(instance, SetTests.fixture.Create<JWeakRef>());

		env.IsValidationAvoidable(Arg.Any<JGlobalBase>()).Returns(true);
		env.GetReferenceType(jWeak).Returns(JReferenceType.WeakGlobalRefType);
		env.ClassFeature.GetObjectClass(jWeak.ObjectMetadata).Returns(jWrapperClass);
		env.ClassFeature.GetTypeMetadata(jWrapperClass).Returns(wrapperTypeMetadata);
		env.ReferenceFeature.Create<JWeak>(jLocalInstance).Returns(jWeak);
		env.ClassFeature.IsInstanceOf<JNumberObject>(jLocalInstance).Returns(instance is JNumberObject);
		env.ClassFeature.IsInstanceOf<JBooleanObject>(jLocalInstance).Returns(instance is JBooleanObject);
		env.ClassFeature.IsInstanceOf<JCharacterObject>(jLocalInstance).Returns(instance is JCharacterObject);
		env.ClassFeature.IsInstanceOf<JNumberObject>(jWeak).Returns(instance is JNumberObject);
		env.ClassFeature.IsInstanceOf<JBooleanObject>(jWeak).Returns(instance is JBooleanObject);
		env.ClassFeature.IsInstanceOf<JCharacterObject>(jWeak).Returns(instance is JCharacterObject);
		env.WithFrame(Arg.Any<Int32>(), Arg.Any<IndeterminateField.InfoObjectQuery>(),
		              Arg.Any<Func<IndeterminateField.InfoObjectQuery, IndeterminateField.InfoObjectResult>>())
		   .Returns(c => (c[2] as Func<IndeterminateField.InfoObjectQuery, IndeterminateField.InfoObjectResult>)!(
			            (IndeterminateField.InfoObjectQuery)c[1]));

		field.Set(jString, jLocalInstance);
		env.ReferenceFeature.Received(1).Create<JWeak>(jLocalInstance);
		env.AccessFeature.Received(1).SetPrimitiveField(jString, jString.Class,
		                                                field.Definition, Arg.Any<IReadOnlyFixedMemory>());

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();
		jLocalInstance.Lifetime.UnloadGlobal(jWeak);

		field.Set(jString, jClassClass, jLocalInstance);
		env.ReferenceFeature.Received(1).Create<JWeak>(jLocalInstance);
		env.AccessFeature.Received(1).SetPrimitiveField(jString, jClassClass,
		                                                field.Definition, Arg.Any<IReadOnlyFixedMemory>());

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();
		jLocalInstance.Lifetime.UnloadGlobal(jWeak);

		field.StaticSet(jStringClass, jLocalInstance);
		env.ReferenceFeature.Received(1).Create<JWeak>(jLocalInstance);
		env.AccessFeature.Received(1)
		   .SetPrimitiveStaticField(jStringClass, field.Definition, Arg.Any<IReadOnlyFixedMemory>());

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();
		jLocalInstance.Lifetime.UnloadGlobal(jWeak);

		IndeterminateField.ReflectedSet(jField, jString, jLocalInstance);
		env.ReferenceFeature.Received(1).Create<JWeak>(jLocalInstance);
		env.AccessFeature.Received(1).SetField(jField, jString, jField.Definition, primitiveArray[0]);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();
		jLocalInstance.Lifetime.UnloadGlobal(jWeak);

		IndeterminateField.ReflectedStaticSet(jField, jLocalInstance);
		env.ReferenceFeature.Received(1).Create<JWeak>(jLocalInstance);
		env.AccessFeature.Received(1).SetStaticField(jField, jField.Definition, primitiveArray[0]);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();

		if (instance is not JNumberObject) return;

		using JClassObject jFakeNumberClass = new(jClassClass, IClassType.GetMetadata<JFakeNumber>(),
		                                          SetTests.fixture.Create<JClassLocalRef>());
		using JNumberObject jFakeNumber = (JNumberObject)IClassType.GetMetadata<JFakeNumber>()
		                                                           .CreateInstance(jFakeNumberClass,
			                                                           SetTests.fixture.Create<JObjectLocalRef>(),
			                                                           true);

		field.Set(jString, jFakeNumber);
		env.AccessFeature.Received(1).SetPrimitiveField(jString, jString.Class,
		                                                field.Definition, Arg.Any<IReadOnlyFixedMemory>());

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		field.Set(jString, jClassClass, jFakeNumber);
		env.AccessFeature.Received(1).SetPrimitiveField(jString, jClassClass,
		                                                field.Definition, Arg.Any<IReadOnlyFixedMemory>());

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		field.StaticSet(jStringClass, jFakeNumber);
		env.AccessFeature.Received(1)
		   .SetPrimitiveStaticField(jStringClass, field.Definition, Arg.Any<IReadOnlyFixedMemory>());

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateField.ReflectedSet(jField, jString, jFakeNumber);
		env.AccessFeature.Received(1).SetField(jField, jString, jField.Definition, primitiveArray[0]);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateField.ReflectedStaticSet(jField, jFakeNumber);
		env.AccessFeature.Received(1).SetStaticField(jField, jField.Definition, primitiveArray[0]);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
	}

	private static void WrapperFieldPrimitiveTest<TWrapper, TPrimitive>()
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		where TWrapper : JLocalObject, IPrimitiveWrapperType<TWrapper, TPrimitive>
	{
		ReadOnlySpan<Byte> fieldName = (CString)SetTests.fixture.Create<String>();
		JReferenceTypeMetadata wrapperTypeMetadata = IReferenceType.GetMetadata<TWrapper>();
		TPrimitive[] primitiveArray = SetTests.fixture.CreatePrimitiveArray<TPrimitive>(1);
		JObject primitiveObject = primitiveArray[0];
		JObjectLocalRef localRef = SetTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = SetTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef stringRef = SetTests.fixture.Create<JStringLocalRef>();
		String textValue = SetTests.fixture.Create<String>();

		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClassClass = new(env);
		using JClassObject jWrapperClass = new(jClassClass, wrapperTypeMetadata, classRef);
		using JClassObject jArrayClass = new(jClassClass, wrapperTypeMetadata.GetArrayMetadata()!,
		                                     SetTests.fixture.Create<JClassLocalRef>());
		using JClassObject jStringClass = !wrapperTypeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/String"u8) ?
			new(jClassClass, IClassType.GetMetadata<JStringObject>(), SetTests.fixture.Create<JClassLocalRef>()) :
			jWrapperClass;
		using JClassObject jFieldClass = !wrapperTypeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/Field"u8) ?
			new(jClassClass, IClassType.GetMetadata<JFieldObject>(), SetTests.fixture.Create<JClassLocalRef>()) :
			jWrapperClass;
		using JStringObject jString = new(jStringClass, stringRef, textValue);

		using TWrapper instance = Assert.IsType<TWrapper>(
			wrapperTypeMetadata.ParseInstance(wrapperTypeMetadata.CreateInstance(jWrapperClass, localRef, true)));

		env.FunctionSet.GetPrimitiveValue<JByte>(Arg.Any<JNumberObject>())
		   .Returns(c => (JByte)IPrimitiveType.GetMetadata<JByte>().CreateInstance(primitiveArray.AsSpan().AsBytes()));
		env.FunctionSet.GetPrimitiveValue<JDouble>(Arg.Any<JNumberObject>())
		   .Returns(c => (JDouble)IPrimitiveType.GetMetadata<JDouble>()
		                                        .CreateInstance(primitiveArray.AsSpan().AsBytes()));
		env.FunctionSet.GetPrimitiveValue<JFloat>(Arg.Any<JNumberObject>())
		   .Returns(c => (JFloat)IPrimitiveType.GetMetadata<JFloat>()
		                                       .CreateInstance(primitiveArray.AsSpan().AsBytes()));
		env.FunctionSet.GetPrimitiveValue<JInt>(Arg.Any<JNumberObject>())
		   .Returns(c => (JInt)IPrimitiveType.GetMetadata<JInt>().CreateInstance(primitiveArray.AsSpan().AsBytes()));
		env.FunctionSet.GetPrimitiveValue<JLong>(Arg.Any<JNumberObject>())
		   .Returns(c => (JLong)IPrimitiveType.GetMetadata<JLong>().CreateInstance(primitiveArray.AsSpan().AsBytes()));
		env.FunctionSet.GetPrimitiveValue<JShort>(Arg.Any<JNumberObject>())
		   .Returns(c => (JShort)IPrimitiveType.GetMetadata<JShort>()
		                                       .CreateInstance(primitiveArray.AsSpan().AsBytes()));
		env.AccessFeature
		   .CallFunction<JBoolean>(Arg.Any<JLocalObject>(), Arg.Any<JClassObject>(),
		                           NativeFunctionSetImpl.BooleanValueDefinition, false, [])
		   .Returns(c => (JBoolean)IPrimitiveType.GetMetadata<JBoolean>()
		                                         .CreateInstance(primitiveArray.AsSpan().AsBytes()));
		env.AccessFeature
		   .CallFunction<JChar>(Arg.Any<JLocalObject>(), Arg.Any<JClassObject>(),
		                        NativeFunctionSetImpl.CharValueDefinition, false, [])
		   .Returns(c => (JChar)IPrimitiveType.GetMetadata<JChar>().CreateInstance(primitiveArray.AsSpan().AsBytes()));
		env.ReferenceFeature.CreateWrapper(primitiveArray[0]).Returns(instance);

		JFieldDefinition definition = new JFieldDefinition<TWrapper>(fieldName);
		IndeterminateField field = IndeterminateField.Create<TWrapper>(fieldName);
		IndeterminateField nonGenericField = IndeterminateField.Create(wrapperTypeMetadata.ArgumentMetadata, fieldName);

		Assert.IsType<JFieldDefinition<TWrapper>>(field.Definition);
		Assert.IsType<JNonTypedFieldDefinition>(nonGenericField.Definition);

		Assert.Equal(field.Definition, nonGenericField.Definition);
		Assert.Equal(field.Definition.Name, nonGenericField.Definition.Name);
		Assert.Equal(field.Definition.Descriptor, nonGenericField.Definition.Descriptor);
		Assert.Equal(field.Definition.Hash, nonGenericField.Definition.Hash);

		Assert.Equal(definition, field.Definition);
		Assert.Equal(definition, nonGenericField.Definition);
		Assert.Equal(wrapperTypeMetadata.Signature, field.FieldType);

		env.ClassFeature.GetClass<TWrapper>().Returns(jWrapperClass);
		env.ReferenceFeature.Unload(Arg.Any<JLocalObject>()).Returns(true);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		field.Set(jString, primitiveArray[0]);
		env.ReferenceFeature.Received(1).CreateWrapper(primitiveArray[0]);
		env.AccessFeature.Received(1).SetField(jString, jString.Class, field.Definition, instance);

		Assert.True(instance.IsDefault);
		instance.SetValue(localRef);
		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();

		field.Set(jString, jClassClass, primitiveArray[0]);
		env.ReferenceFeature.Received(1).CreateWrapper(primitiveArray[0]);
		env.AccessFeature.Received(1).SetField(jString, jClassClass, field.Definition, instance);

		Assert.True(instance.IsDefault);
		instance.SetValue(localRef);
		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();

		field.StaticSet(jStringClass, primitiveArray[0]);
		env.ReferenceFeature.Received(1).CreateWrapper(primitiveArray[0]);
		env.AccessFeature.Received(1).SetStaticField(jStringClass, field.Definition, instance);

		Assert.True(instance.IsDefault);
		instance.SetValue(localRef);
		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();

		using JFieldObject jField = new(jFieldClass, SetTests.fixture.Create<JObjectLocalRef>(), definition,
		                                jWrapperClass);

		IndeterminateField.ReflectedSet(jField, jString, primitiveArray[0]);
		env.ReferenceFeature.Received(1).CreateWrapper(primitiveArray[0]);
		env.AccessFeature.Received(1).SetField(jField, jString, field.Definition, instance);

		Assert.True(instance.IsDefault);
		instance.SetValue(localRef);
		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();

		IndeterminateField.ReflectedStaticSet(jField, primitiveArray[0]);
		env.ReferenceFeature.Received(1).CreateWrapper(primitiveArray[0]);
		env.AccessFeature.Received(1).SetStaticField(jField, field.Definition, instance);

		Assert.True(instance.IsDefault);
		instance.SetValue(localRef);
		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();

		field.Set(jString, primitiveObject);
		env.ReferenceFeature.Received(1).CreateWrapper(primitiveArray[0]);
		env.AccessFeature.Received(1).SetField(jString, jString.Class, field.Definition, instance);

		Assert.True(instance.IsDefault);
		instance.SetValue(localRef);
		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();

		field.Set(jString, jClassClass, primitiveObject);
		env.ReferenceFeature.Received(1).CreateWrapper(primitiveArray[0]);
		env.AccessFeature.Received(1).SetField(jString, jClassClass, field.Definition, instance);

		Assert.True(instance.IsDefault);
		instance.SetValue(localRef);
		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();

		field.StaticSet(jStringClass, primitiveObject);
		env.ReferenceFeature.Received(1).CreateWrapper(primitiveArray[0]);
		env.AccessFeature.Received(1).SetStaticField(jStringClass, field.Definition, instance);

		Assert.True(instance.IsDefault);
		instance.SetValue(localRef);
		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();

		IndeterminateField.ReflectedSet(jField, jString, primitiveObject);
		env.ReferenceFeature.Received(1).CreateWrapper(primitiveArray[0]);
		env.AccessFeature.Received(1).SetField(jField, jString, field.Definition, instance);

		Assert.True(instance.IsDefault);
		instance.SetValue(localRef);
		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();

		IndeterminateField.ReflectedStaticSet(jField, primitiveObject);
		env.ReferenceFeature.Received(1).CreateWrapper(primitiveArray[0]);
		env.AccessFeature.Received(1).SetStaticField(jField, field.Definition, instance);

		Assert.True(instance.IsDefault);
	}

	private delegate void ReferenceObjectTestDelegate(EnvironmentProxy env, ReadOnlySpan<Byte> fieldName,
		JClassObject jClass, JStringObject jString, JClassObject jFieldClass, JReferenceObject? instance);

	private sealed class JFakeNumber : JNumberObject, IClassType<JFakeNumber>
	{
		private static readonly JClassTypeMetadata<JFakeNumber> typeMetadata = TypeMetadataBuilder<JNumberObject>
		                                                                       .Create<JFakeNumber>(
			                                                                       "com/fake/FakeNumber"u8,
			                                                                       JTypeModifier.Final).Build();

		static JClassTypeMetadata<JFakeNumber> IClassType<JFakeNumber>.Metadata => JFakeNumber.typeMetadata;

		private JFakeNumber(IReferenceType.ClassInitializer initializer) : base(initializer) { }
		private JFakeNumber(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
		private JFakeNumber(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

		static JFakeNumber IClassType<JFakeNumber>.Create(IReferenceType.ClassInitializer initializer)
			=> new(initializer);
		static JFakeNumber IClassType<JFakeNumber>.Create(IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
		static JFakeNumber IClassType<JFakeNumber>.Create(IReferenceType.GlobalInitializer initializer)
			=> new(initializer);
	}
}