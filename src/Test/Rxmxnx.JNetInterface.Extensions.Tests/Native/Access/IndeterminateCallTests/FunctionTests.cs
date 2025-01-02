namespace Rxmxnx.JNetInterface.Tests.Native.Access.IndeterminateCallTests;

[ExcludeFromCodeCoverage]
public sealed class FunctionTests : IndeterminateCallTestsBase
{
	private static readonly MethodInfo objectTestInfo =
		typeof(FunctionTests).GetMethod(nameof(FunctionTests.ObjectTest),
		                                BindingFlags.Static | BindingFlags.NonPublic)!;
	private static readonly MethodInfo primitiveTestInfo =
		typeof(FunctionTests).GetMethod(nameof(FunctionTests.PrimitiveTest),
		                                BindingFlags.Static | BindingFlags.NonPublic)!;
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly JArgumentMetadata[] args =
	[
		JArgumentMetadata.Create<JClassObject>(), JArgumentMetadata.Create<JStringObject>(),
		JArgumentMetadata.Create<JInt>(),
	];

	[Fact]
	internal void BooleanTest() => FunctionTests.ParameterlessTest<JBoolean>();
	[Fact]
	internal void ByteTest() => FunctionTests.ParameterlessTest<JByte>();
	[Fact]
	internal void CharTest() => FunctionTests.ParameterlessTest<JChar>();
	[Fact]
	internal void DoubleTest() => FunctionTests.ParameterlessTest<JDouble>();
	[Fact]
	internal void FloatTest() => FunctionTests.ParameterlessTest<JFloat>();
	[Fact]
	internal void IntTest() => FunctionTests.ParameterlessTest<JInt>();
	[Fact]
	internal void LongTest() => FunctionTests.ParameterlessTest<JLong>();
	[Fact]
	internal void ShortTest() => FunctionTests.ParameterlessTest<JShort>();

	[Fact]
	internal void ObjectParameterlessTest() => FunctionTests.ParameterlessTest<JLocalObject>();
	[Fact]
	internal void ClassParameterlessTest() => FunctionTests.ParameterlessTest<JClassObject>();
	[Fact]
	internal void StringParameterlessTest() => FunctionTests.ParameterlessTest<JStringObject>();
	[Fact]
	internal void ThrowableParameterlessTest() => FunctionTests.ParameterlessTest<JThrowableObject>();
	[Fact]
	internal void EnumParameterlessTest() => FunctionTests.ParameterlessTest<JEnumObject>();
	[Fact]
	internal void NumberParameterlessTest() => FunctionTests.ParameterlessTest<JNumberObject>();
	[Fact]
	internal void AccessibleObjectParameterlessTest() => FunctionTests.ParameterlessTest<JAccessibleObject>();
	[Fact]
	internal void ExecutableParameterlessTest() => FunctionTests.ParameterlessTest<JExecutableObject>();
	[Fact]
	internal void ModifierParameterlessTest() => FunctionTests.ParameterlessTest<JModifierObject>();
	[Fact]
	internal void ProxyParameterlessTest() => FunctionTests.ParameterlessTest<JProxyObject>();

	[Fact]
	internal void ErrorParameterlessTest() => FunctionTests.ParameterlessTest<JErrorObject>();
	[Fact]
	internal void ExceptionParameterlessTest() => FunctionTests.ParameterlessTest<JExceptionObject>();
	[Fact]
	internal void RuntimeExceptionParameterlessTest() => FunctionTests.ParameterlessTest<JRuntimeExceptionObject>();
	[Fact]
	internal void BufferParameterlessTest() => FunctionTests.ParameterlessTest<JBufferObject>();

	[Fact]
	internal void FieldParameterlessTest() => FunctionTests.ParameterlessTest<JFieldObject>();
	[Fact]
	internal void MethodParameterlessTest() => FunctionTests.ParameterlessTest<JMethodObject>();
	[Fact]
	internal void ConstructorParameterlessTest() => FunctionTests.ParameterlessTest<JConstructorObject>();
	[Fact]
	internal void StackTraceElementParameterlessTest() => FunctionTests.ParameterlessTest<JStackTraceElementObject>();

	[Theory]
	[InlineData(typeof(JBoolean))]
	[InlineData(typeof(JByte))]
	[InlineData(typeof(JChar))]
	[InlineData(typeof(JDouble))]
	[InlineData(typeof(JFloat))]
	[InlineData(typeof(JInt))]
	[InlineData(typeof(JLong))]
	[InlineData(typeof(JShort))]
	[InlineData(typeof(JLocalObject))]
	[InlineData(typeof(JClassObject))]
	[InlineData(typeof(JStringObject))]
	[InlineData(typeof(JThrowableObject))]
	[InlineData(typeof(JEnumObject))]
	[InlineData(typeof(JNumberObject))]
	[InlineData(typeof(JAccessibleObject))]
	[InlineData(typeof(JExecutableObject))]
	[InlineData(typeof(JModifierObject))]
	[InlineData(typeof(JProxyObject))]
	[InlineData(typeof(JErrorObject))]
	[InlineData(typeof(JExceptionObject))]
	[InlineData(typeof(JRuntimeExceptionObject))]
	[InlineData(typeof(JBufferObject))]
	[InlineData(typeof(JFieldObject))]
	[InlineData(typeof(JMethodObject))]
	[InlineData(typeof(JConstructorObject))]
	[InlineData(typeof(JStackTraceElementObject))]
	internal void Test(Type typeofT)
	{
		MethodInfo methodInfo = typeofT.IsValueType ?
			FunctionTests.primitiveTestInfo.MakeGenericMethod(typeofT) :
			FunctionTests.objectTestInfo.MakeGenericMethod(typeofT);
		methodInfo.CreateDelegate<Action>()();
	}

	private static void ParameterlessTest<TDataType>() where TDataType : IDataType<TDataType>
	{
		JDataTypeMetadata typeMetadata = IDataType.GetMetadata<TDataType>();
		ReadOnlySpan<Byte> functionName = (CString)FunctionTests.fixture.Create<String>();
		JFunctionDefinition definition = new JFunctionDefinition<TDataType>.Parameterless(functionName);
		IndeterminateCall call = IndeterminateCall.CreateFunctionDefinition<TDataType>(functionName, []);
		IndeterminateCall callMetadata =
			IndeterminateCall.CreateFunctionDefinition(typeMetadata.ArgumentMetadata, functionName, []);
		IndeterminateCall operatorCall = definition;

		Assert.Equal(call.Definition, operatorCall.Definition);
		Assert.Equal(call.Definition.Name, operatorCall.Definition.Name);
		Assert.Equal(call.Definition.Descriptor, operatorCall.Definition.Descriptor);
		Assert.Equal(call.Definition.Hash, operatorCall.Definition.Hash);
		Assert.Equal(call.Definition, callMetadata.Definition);
		Assert.Equal(call.Definition.Name, callMetadata.Definition.Name);
		Assert.Equal(call.Definition.Descriptor, callMetadata.Definition.Descriptor);
		Assert.Equal(call.Definition.Hash, callMetadata.Definition.Hash);
		Assert.Equal(functionName, operatorCall.Definition.Name);
		Assert.Equal(CString.Concat("()"u8, typeMetadata.Signature), operatorCall.Definition.Descriptor);
		Assert.Equal(typeMetadata.Signature, operatorCall.ReturnType.AsSpan());
		Assert.Equal(call.ReturnType, operatorCall.ReturnType);

		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClass = new(env);

		IndeterminateResult result = new(0, typeMetadata.Signature);

		IndeterminateCallTestsBase.Compare(result, call.FunctionCall(jClass, []));
		IndeterminateCallTestsBase.Compare(result, call.FunctionCall(jClass, jClass, true, []));
		IndeterminateCallTestsBase.Compare(result, call.StaticFunctionCall(jClass, []));

		Assert.Null((IndeterminateCall?)default(JFunctionDefinition<TDataType>));
	}
	private static void ObjectTest<TDataType>() where TDataType : JLocalObject, IClassType<TDataType>
	{
		ReadOnlySpan<Byte> functionName = (CString)FunctionTests.fixture.Create<String>();
		JFunctionDefinition definition = JFunctionDefinition<TDataType>.Create(functionName, FunctionTests.args);
		IndeterminateCall call =
			IndeterminateCall.CreateFunctionDefinition<TDataType>(functionName, FunctionTests.args);
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<TDataType>();
		Boolean isAbstract = typeMetadata.Modifier == JTypeModifier.Abstract;
		IndeterminateCall nonGenericCall =
			IndeterminateCall.CreateFunctionDefinition(typeMetadata.ArgumentMetadata, functionName, FunctionTests.args);
		JObjectLocalRef localRef = FunctionTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = FunctionTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef stringRef = FunctionTests.fixture.Create<JStringLocalRef>();
		String textValue = FunctionTests.fixture.Create<String>();

		Assert.IsType<JFunctionDefinition<TDataType>>(call.Definition);
		Assert.IsType<JNonTypedFunctionDefinition>(nonGenericCall.Definition);

		Assert.Equal(call.Definition, nonGenericCall.Definition);
		Assert.Equal(call.Definition.Name, nonGenericCall.Definition.Name);
		Assert.Equal(call.Definition.Descriptor, nonGenericCall.Definition.Descriptor);
		Assert.Equal(call.Definition.Hash, nonGenericCall.Definition.Hash);

		if (typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/Class"u8)) return;

		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClassClass = new(env);
		using JClassObject jClass = new(jClassClass, typeMetadata, classRef);
		using JClassObject jStringClass = !typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/String"u8) ?
			new(jClassClass, IClassType.GetMetadata<JStringObject>(), FunctionTests.fixture.Create<JClassLocalRef>()) :
			jClass;
		using JClassObject jMethodClass = !typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/Method"u8) ?
			new(jClassClass, IClassType.GetMetadata<JMethodObject>(), FunctionTests.fixture.Create<JClassLocalRef>()) :
			jClass;
		using JStringObject jString = new(jStringClass, stringRef, textValue);
		using JMethodObject jMethod = new(jMethodClass, FunctionTests.fixture.Create<JObjectLocalRef>(), definition,
		                                  jClass);

		if (typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/String"u8)) return;

		using TDataType? instance =
			!isAbstract && !typeMetadata.ClassName.AsSpan().SequenceEqual(CommonNames.ClassObject) &&
			!typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/String"u8) ?
				Assert.IsType<TDataType>(
					typeMetadata.ParseInstance(typeMetadata.CreateInstance(jClass, localRef, true))) :
				default;
		IObject?[] parameters = [jClass, jString, JInt.NegativeOne,];

		Assert.Equal(definition, call.Definition);
		Assert.Equal(definition, nonGenericCall.Definition);
		Assert.Equal(typeMetadata.Signature, call.ReturnType);

		env.ClassFeature.GetClass<TDataType>().Returns(jClass);
		env.AccessFeature.CallFunction<JLocalObject>(Arg.Any<JLocalObject>(), Arg.Any<JClassObject>(),
		                                             (JFunctionDefinition)call.Definition, Arg.Any<Boolean>(),
		                                             Arg.Any<IObject?[]>()).Returns(instance);
		env.AccessFeature.CallFunction<JLocalObject>(Arg.Any<JMethodObject>(), Arg.Any<JLocalObject>(),
		                                             (JFunctionDefinition)call.Definition, Arg.Any<Boolean>(),
		                                             Arg.Any<IObject?[]>()).Returns(instance);
		env.AccessFeature
		   .CallStaticFunction<JLocalObject>(Arg.Any<JClassObject>(), (JFunctionDefinition)call.Definition,
		                                     Arg.Any<IObject?[]>()).Returns(instance);
		env.AccessFeature
		   .CallStaticFunction<JLocalObject>(Arg.Any<JMethodObject>(), (JFunctionDefinition)call.Definition,
		                                     Arg.Any<IObject?[]>()).Returns(instance);
		env.ReferenceFeature.Unload(Arg.Any<JLocalObject>()).Returns(true);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateResult result = new(instance, typeMetadata.Signature);

		Assert.Equal(localRef, instance?.LocalReference ?? localRef);
		call.MethodCall(jString, parameters);
		env.AccessFeature.Received(1).CallFunction<JLocalObject>(jString, jString.Class,
		                                                         (JFunctionDefinition)call.Definition, false,
		                                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));
		Assert.Equal(default, instance?.LocalReference ?? default);
		instance?.SetValue(localRef);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		Assert.Equal(localRef, instance?.LocalReference ?? localRef);
		call.MethodCall(jString, jClassClass, false, parameters);
		env.AccessFeature.Received(1).CallFunction<JLocalObject>(jString, jClassClass,
		                                                         (JFunctionDefinition)call.Definition, false,
		                                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));
		Assert.Equal(default, instance?.LocalReference ?? default);
		instance?.SetValue(localRef);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		Assert.Equal(localRef, instance?.LocalReference ?? localRef);
		call.MethodCall(jClassClass, jClassClass, true, parameters);
		env.AccessFeature.Received(1).CallFunction<JLocalObject>(jClassClass, jClassClass,
		                                                         (JFunctionDefinition)call.Definition, true,
		                                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));
		Assert.Equal(default, instance?.LocalReference ?? default);
		instance?.SetValue(localRef);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		Assert.Equal(localRef, instance?.LocalReference ?? localRef);
		call.StaticMethodCall(jStringClass, parameters);
		env.AccessFeature.Received(1).CallStaticFunction<JLocalObject>(jStringClass,
		                                                               (JFunctionDefinition)call.Definition,
		                                                               Arg.Is<IObject[]>(
			                                                               a => a.SequenceEqual(parameters)));
		Assert.Equal(default, instance?.LocalReference ?? default);
		instance?.SetValue(localRef);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		Assert.Equal(localRef, instance?.LocalReference ?? localRef);
		IndeterminateCall.ReflectedMethodCall(jMethod, jString, parameters);
		env.AccessFeature.Received(1).CallFunction<JLocalObject>(jMethod, jString,
		                                                         (JFunctionDefinition)jMethod.Definition, false,
		                                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));
		Assert.Equal(default, instance?.LocalReference ?? default);
		instance?.SetValue(localRef);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		Assert.Equal(localRef, instance?.LocalReference ?? localRef);
		IndeterminateCall.ReflectedMethodCall(jMethod, jString, true, parameters);
		env.AccessFeature.Received(1).CallFunction<JLocalObject>(jMethod, jString,
		                                                         (JFunctionDefinition)jMethod.Definition, true,
		                                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));
		Assert.Equal(default, instance?.LocalReference ?? default);
		instance?.SetValue(localRef);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		Assert.Equal(localRef, instance?.LocalReference ?? localRef);
		IndeterminateCall.ReflectedStaticMethodCall(jMethod, parameters);
		env.AccessFeature.Received(1).CallStaticFunction<JLocalObject>(jMethod, (JFunctionDefinition)jMethod.Definition,
		                                                               Arg.Is<IObject[]>(
			                                                               a => a.SequenceEqual(parameters)));
		Assert.Equal(default, instance?.LocalReference ?? default);
		instance?.SetValue(localRef);

		IndeterminateCallTestsBase.Compare(result, call.FunctionCall(jString, parameters));
		env.AccessFeature.Received(1).CallFunction<JLocalObject>(jString, jString.Class,
		                                                         (JFunctionDefinition)call.Definition, false,
		                                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCallTestsBase.Compare(result, call.FunctionCall(jString, jClassClass, false, parameters));
		env.AccessFeature.Received(1).CallFunction<JLocalObject>(jString, jClassClass,
		                                                         (JFunctionDefinition)call.Definition, false,
		                                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCallTestsBase.Compare(result, call.FunctionCall(jClassClass, jClassClass, true, parameters));
		env.AccessFeature.Received(1).CallFunction<JLocalObject>(jClassClass, jClassClass,
		                                                         (JFunctionDefinition)call.Definition, true,
		                                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCallTestsBase.Compare(result, call.StaticFunctionCall(jStringClass, parameters));
		env.AccessFeature.Received(1).CallStaticFunction<JLocalObject>(jStringClass,
		                                                               (JFunctionDefinition)call.Definition,
		                                                               Arg.Is<IObject[]>(
			                                                               a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCallTestsBase.Compare(result,
		                                   IndeterminateCall.ReflectedFunctionCall(jMethod, jString, parameters));
		env.AccessFeature.Received(1).CallFunction<JLocalObject>(jMethod, jString,
		                                                         (JFunctionDefinition)jMethod.Definition, false,
		                                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCallTestsBase.Compare(result,
		                                   IndeterminateCall.ReflectedFunctionCall(jMethod, jString, true, parameters));
		env.AccessFeature.Received(1).CallFunction<JLocalObject>(jMethod, jString,
		                                                         (JFunctionDefinition)jMethod.Definition, true,
		                                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCallTestsBase.Compare(result, IndeterminateCall.ReflectedStaticFunctionCall(jMethod, parameters));
		env.AccessFeature.Received(1).CallStaticFunction<JLocalObject>(jMethod, (JFunctionDefinition)jMethod.Definition,
		                                                               Arg.Is<IObject[]>(
			                                                               a => a.SequenceEqual(parameters)));
	}
	private static void PrimitiveTest<TDataType>() where TDataType : unmanaged, IPrimitiveType<TDataType>
	{
		ReadOnlySpan<Byte> functionName = (CString)FunctionTests.fixture.Create<String>();
		JFunctionDefinition definition = JFunctionDefinition<TDataType>.Create(functionName, FunctionTests.args);
		IndeterminateCall call =
			IndeterminateCall.CreateFunctionDefinition<TDataType>(functionName, FunctionTests.args);
		JPrimitiveTypeMetadata typeMetadata = IPrimitiveType.GetMetadata<TDataType>();
		IndeterminateCall nonGenericCall =
			IndeterminateCall.CreateFunctionDefinition(typeMetadata.ArgumentMetadata, functionName, FunctionTests.args);
		TDataType[] primitiveArray = PrimitiveArrayTests.CreatePrimitiveArray<TDataType>(1);
		JClassLocalRef classRef = FunctionTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef stringRef = FunctionTests.fixture.Create<JStringLocalRef>();
		String textValue = FunctionTests.fixture.Create<String>();

		Assert.IsType<JFunctionDefinition<TDataType>>(call.Definition);
		Assert.IsType<JFunctionDefinition<TDataType>>(nonGenericCall.Definition);

		Assert.Equal(call.Definition, nonGenericCall.Definition);
		Assert.Equal(call.Definition.Name, nonGenericCall.Definition.Name);
		Assert.Equal(call.Definition.Descriptor, nonGenericCall.Definition.Descriptor);
		Assert.Equal(call.Definition.Hash, nonGenericCall.Definition.Hash);

		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClassClass = new(env);
		using JClassObject jClass = new(jClassClass, typeMetadata, classRef);
		using JClassObject jStringClass = !typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/String"u8) ?
			new(jClassClass, IClassType.GetMetadata<JStringObject>(), FunctionTests.fixture.Create<JClassLocalRef>()) :
			jClass;
		using JClassObject jMethodClass = !typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/Method"u8) ?
			new(jClassClass, IClassType.GetMetadata<JMethodObject>(), FunctionTests.fixture.Create<JClassLocalRef>()) :
			jClass;
		using JStringObject jString = new(jStringClass, stringRef, textValue);
		using JMethodObject jMethod = new(jMethodClass, FunctionTests.fixture.Create<JObjectLocalRef>(), definition,
		                                  jClass);

		IObject?[] parameters = [jClass, jString, JInt.NegativeOne,];

		Assert.Equal(definition, call.Definition);
		Assert.Equal(definition, nonGenericCall.Definition);
		Assert.Equal(typeMetadata.Signature, call.ReturnType);

		env.AccessFeature.When(a => a.CallPrimitiveFunction(Arg.Any<IFixedMemory>(), Arg.Any<JLocalObject>(),
		                                                    Arg.Any<JClassObject>(),
		                                                    (JFunctionDefinition)call.Definition, Arg.Any<Boolean>(),
		                                                    Arg.Any<IObject?[]>())).Do(c =>
		{
			IFixedMemory mem = (IFixedMemory)c[0];
			primitiveArray.AsSpan().AsBytes().CopyTo(mem.Bytes);
		});
		env.AccessFeature.When(a => a.CallStaticPrimitiveFunction(Arg.Any<IFixedMemory>(), Arg.Any<JClassObject>(),
		                                                          (JFunctionDefinition)call.Definition,
		                                                          Arg.Any<IObject?[]>())).Do(c =>
		{
			IFixedMemory mem = (IFixedMemory)c[0];
			primitiveArray.AsSpan().AsBytes().CopyTo(mem.Bytes);
		});
		env.AccessFeature.CallFunction<TDataType>(Arg.Any<JMethodObject>(), Arg.Any<JLocalObject>(),
		                                          (JFunctionDefinition)call.Definition, Arg.Any<Boolean>(),
		                                          Arg.Any<IObject?[]>()).Returns(primitiveArray[0]);
		env.AccessFeature
		   .CallStaticFunction<TDataType>(Arg.Any<JMethodObject>(), (JFunctionDefinition)call.Definition,
		                                  Arg.Any<IObject?[]>()).Returns(primitiveArray[0]);

		Span<Byte> bytes = stackalloc Byte[sizeof(Int64)];
		primitiveArray.AsSpan().AsBytes().CopyTo(bytes);

		call.MethodCall(jString, parameters);
		env.AccessFeature.Received(1).CallPrimitiveFunction(Arg.Any<IFixedMemory>(), jString, jString.Class,
		                                                    (JFunctionDefinition)call.Definition, false,
		                                                    Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		call.MethodCall(jString, jClassClass, false, parameters);
		env.AccessFeature.Received(1).CallPrimitiveFunction(Arg.Any<IFixedMemory>(), jString, jClassClass,
		                                                    (JFunctionDefinition)call.Definition, false,
		                                                    Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		call.MethodCall(jClassClass, jClassClass, true, parameters);
		env.AccessFeature.Received(1).CallPrimitiveFunction(Arg.Any<IFixedMemory>(), jClassClass, jClassClass,
		                                                    (JFunctionDefinition)call.Definition, true,
		                                                    Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		call.StaticMethodCall(jStringClass, parameters);
		env.AccessFeature.Received(1).CallStaticPrimitiveFunction(Arg.Any<IFixedMemory>(), jStringClass,
		                                                          (JFunctionDefinition)call.Definition,
		                                                          Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCall.ReflectedMethodCall(jMethod, jString, parameters);
		env.AccessFeature.Received(1).CallFunction<TDataType>(jMethod, jString, (JFunctionDefinition)jMethod.Definition,
		                                                      false,
		                                                      Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCall.ReflectedMethodCall(jMethod, jString, true, parameters);
		env.AccessFeature.Received(1).CallFunction<TDataType>(jMethod, jString, (JFunctionDefinition)jMethod.Definition,
		                                                      true,
		                                                      Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCall.ReflectedStaticMethodCall(jMethod, parameters);
		env.AccessFeature.Received(1).CallStaticFunction<TDataType>(jMethod, (JFunctionDefinition)jMethod.Definition,
		                                                            Arg.Is<IObject[]>(
			                                                            a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateResult result = new(MemoryMarshal.Cast<Byte, Int64>(bytes)[0], typeMetadata.Signature);

		IndeterminateCallTestsBase.Compare(result, call.FunctionCall(jString, parameters));
		env.AccessFeature.Received(1).CallPrimitiveFunction(Arg.Any<IFixedMemory>(), jString, jString.Class,
		                                                    (JFunctionDefinition)call.Definition, false,
		                                                    Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCallTestsBase.Compare(result, call.FunctionCall(jString, jClassClass, false, parameters));
		env.AccessFeature.Received(1).CallPrimitiveFunction(Arg.Any<IFixedMemory>(), jString, jClassClass,
		                                                    (JFunctionDefinition)call.Definition, false,
		                                                    Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCallTestsBase.Compare(result, call.FunctionCall(jClassClass, jClassClass, true, parameters));
		env.AccessFeature.Received(1).CallPrimitiveFunction(Arg.Any<IFixedMemory>(), jClassClass, jClassClass,
		                                                    (JFunctionDefinition)call.Definition, true,
		                                                    Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCallTestsBase.Compare(result, call.StaticFunctionCall(jStringClass, parameters));
		env.AccessFeature.Received(1).CallStaticPrimitiveFunction(Arg.Any<IFixedMemory>(), jStringClass,
		                                                          (JFunctionDefinition)call.Definition,
		                                                          Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCallTestsBase.Compare(result,
		                                   IndeterminateCall.ReflectedFunctionCall(jMethod, jString, true, parameters));
		env.AccessFeature.Received(1).CallFunction<TDataType>(jMethod, jString, (JFunctionDefinition)jMethod.Definition,
		                                                      true,
		                                                      Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCallTestsBase.Compare(result, IndeterminateCall.ReflectedStaticFunctionCall(jMethod, parameters));
		env.AccessFeature.Received(1).CallStaticFunction<TDataType>(jMethod, (JFunctionDefinition)jMethod.Definition,
		                                                            Arg.Is<IObject[]>(
			                                                            a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCallTestsBase.Compare(result, call.FunctionCall(jString, parameters));
		env.AccessFeature.Received(1).CallPrimitiveFunction(Arg.Any<IFixedMemory>(), jString, jString.Class,
		                                                    (JFunctionDefinition)call.Definition, false,
		                                                    Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCallTestsBase.Compare(result, call.FunctionCall(jString, jClassClass, false, parameters));
		env.AccessFeature.Received(1).CallPrimitiveFunction(Arg.Any<IFixedMemory>(), jString, jClassClass,
		                                                    (JFunctionDefinition)call.Definition, false,
		                                                    Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCallTestsBase.Compare(result, call.FunctionCall(jClassClass, jClassClass, true, parameters));
		env.AccessFeature.Received(1).CallPrimitiveFunction(Arg.Any<IFixedMemory>(), jClassClass, jClassClass,
		                                                    (JFunctionDefinition)call.Definition, true,
		                                                    Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCallTestsBase.Compare(result, call.StaticFunctionCall(jStringClass, parameters));
		env.AccessFeature.Received(1).CallStaticPrimitiveFunction(Arg.Any<IFixedMemory>(), jStringClass,
		                                                          (JFunctionDefinition)call.Definition,
		                                                          Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCallTestsBase.Compare(result,
		                                   IndeterminateCall.ReflectedFunctionCall(jMethod, jString, parameters));
		env.AccessFeature.Received(1).CallFunction<TDataType>(jMethod, jString, (JFunctionDefinition)jMethod.Definition,
		                                                      false,
		                                                      Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCallTestsBase.Compare(result,
		                                   IndeterminateCall.ReflectedFunctionCall(jMethod, jString, true, parameters));
		env.AccessFeature.Received(1).CallFunction<TDataType>(jMethod, jString, (JFunctionDefinition)jMethod.Definition,
		                                                      true,
		                                                      Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCallTestsBase.Compare(result, IndeterminateCall.ReflectedStaticFunctionCall(jMethod, parameters));
		env.AccessFeature.Received(1).CallStaticFunction<TDataType>(jMethod, (JFunctionDefinition)jMethod.Definition,
		                                                            Arg.Is<IObject[]>(
			                                                            a => a.SequenceEqual(parameters)));
	}
}