namespace Rxmxnx.JNetInterface.Tests.Native.Access.IndeterminateCallTests;

[ExcludeFromCodeCoverage]
public sealed class ConstructorTests : IndeterminateCallTestsBase
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly JArgumentMetadata[] args =
	[
		JArgumentMetadata.Create<JClassObject>(), JArgumentMetadata.Create<JStringObject>(),
		JArgumentMetadata.Create<JInt>(),
	];

	[Fact]
	internal void ParameterlessTest()
	{
		JConstructorDefinition definition = new JConstructorDefinition.Parameterless();
		IndeterminateCall call = IndeterminateCall.CreateConstructorDefinition([]);
		IndeterminateCall operatorCall = definition;
		IndeterminateCall methodCall = IndeterminateCall.CreateMethodDefinition(CommonNames.Constructor, []);
		IndeterminateCall functionCall =
			IndeterminateCall.CreateFunctionDefinition(default!, CommonNames.Constructor, []);
		IndeterminateCall functionCallGeneric =
			IndeterminateCall.CreateFunctionDefinition<JInt>(CommonNames.Constructor, []);

		Assert.Equal(call.Definition, operatorCall.Definition);
		Assert.Equal(call.Definition, methodCall.Definition);
		Assert.Equal(call.Definition, functionCall.Definition);
		Assert.Equal(call.Definition, functionCallGeneric.Definition);
		Assert.Equal(call.Definition.Name, operatorCall.Definition.Name);
		Assert.Equal(call.Definition.Descriptor, operatorCall.Definition.Descriptor);
		Assert.Equal(call.Definition.Hash, operatorCall.Definition.Hash);
		Assert.Equal(CommonNames.Constructor, operatorCall.Definition.Name);
		Assert.Equal("()V"u8, operatorCall.Definition.Descriptor);
		Assert.Equal("V"u8, operatorCall.ReturnType.AsSpan());
		Assert.Equal(call.ReturnType, operatorCall.ReturnType);
		Assert.True(Object.ReferenceEquals(JAccessibleObjectDefinition.ParameterlessConstructorHash,
		                                   operatorCall.Definition.Hash));

		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClass = new(env);

		IndeterminateCallTestsBase.EmptyCompare(call.FunctionCall(jClass, []));
		IndeterminateCallTestsBase.EmptyCompare(call.FunctionCall(jClass, jClass, true, []));
		IndeterminateCallTestsBase.EmptyCompare(call.FunctionCall(jClass, jClass, false, []));
		IndeterminateCallTestsBase.Compare(new(0, jClass.ClassSignature), call.StaticFunctionCall(jClass, []));

		call.StaticMethodCall(jClass, []);

		Assert.Null((IndeterminateCall?)default(JConstructorDefinition));
	}
	[Fact]
	internal void ObjectTest() => ConstructorTests.Test<JLocalObject>();
	[Fact]
	internal void ClassTest() => ConstructorTests.Test<JClassObject>();
	[Fact]
	internal void StringTest() => ConstructorTests.Test<JStringObject>();
	[Fact]
	internal void ThrowableTest() => ConstructorTests.Test<JThrowableObject>();
	[Fact]
	internal void EnumTest() => ConstructorTests.Test<JEnumObject>();
	[Fact]
	internal void NumberTest() => ConstructorTests.Test<JNumberObject>();
	[Fact]
	internal void AccessibleObjectTest() => ConstructorTests.Test<JAccessibleObject>();
	[Fact]
	internal void ExecutableTest() => ConstructorTests.Test<JExecutableObject>();
	[Fact]
	internal void ModifierTest() => ConstructorTests.Test<JModifierObject>();
	[Fact]
	internal void ProxyTest() => ConstructorTests.Test<JProxyObject>();

	[Fact]
	internal void ErrorTest() => ConstructorTests.Test<JErrorObject>();
	[Fact]
	internal void ExceptionTest() => ConstructorTests.Test<JExceptionObject>();
	[Fact]
	internal void RuntimeExceptionTest() => ConstructorTests.Test<JRuntimeExceptionObject>();
	[Fact]
	internal void BufferTest() => ConstructorTests.Test<JBufferObject>();

	[Fact]
	internal void FieldTest() => ConstructorTests.Test<JFieldObject>();
	[Fact]
	internal void MethodTest() => ConstructorTests.Test<JMethodObject>();
	[Fact]
	internal void ConstructorTest() => ConstructorTests.Test<JConstructorObject>();
	[Fact]
	internal void StackTraceElementTest() => ConstructorTests.Test<JStackTraceElementObject>();

	private static void Test<TDataType>() where TDataType : JLocalObject, IClassType<TDataType>
	{
		JConstructorDefinition definition = JConstructorDefinition.Create(ConstructorTests.args);
		IndeterminateCall call = IndeterminateCall.CreateConstructorDefinition(ConstructorTests.args);
		IndeterminateCall methodCall =
			IndeterminateCall.CreateMethodDefinition(CommonNames.Constructor, ConstructorTests.args);
		IndeterminateCall functionCall =
			IndeterminateCall.CreateFunctionDefinition(default!, CommonNames.Constructor, ConstructorTests.args);
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<TDataType>();
		Boolean isAbstract = typeMetadata.Modifier == JTypeModifier.Abstract;
		JObjectLocalRef localRef = ConstructorTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = ConstructorTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef stringRef = ConstructorTests.fixture.Create<JStringLocalRef>();
		String textValue = ConstructorTests.fixture.Create<String>();

		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClassClass = new(env);
		using JClassObject jClass = new(jClassClass, typeMetadata, classRef);
		using JClassObject jStringClass = !typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/String"u8) ?
			new(jClassClass, IClassType.GetMetadata<JStringObject>(),
			    ConstructorTests.fixture.Create<JClassLocalRef>()) :
			jClass;
		using JClassObject jConstructorClass =
			!typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/Constructor"u8) ?
				new(jClassClass, IClassType.GetMetadata<JConstructorObject>(),
				    ConstructorTests.fixture.Create<JClassLocalRef>()) :
				jClass;
		using JStringObject jString = new(jStringClass, stringRef, textValue);
		using JConstructorObject jConstructor = new(jConstructorClass,
		                                            ConstructorTests.fixture.Create<JObjectLocalRef>(), definition,
		                                            jClass);
		using TDataType? instance =
			!isAbstract && !typeMetadata.ClassName.AsSpan().SequenceEqual(CommonNames.ClassObject) &&
			!typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/String"u8) ?
				Assert.IsType<TDataType>(
					typeMetadata.ParseInstance(typeMetadata.CreateInstance(jClass, localRef, true))) :
				default;
		IObject?[] parameters = [jClass, jString, JInt.NegativeOne,];

		Assert.Equal(definition, call.Definition);
		Assert.Equal(definition, methodCall.Definition);
		Assert.Equal(definition, functionCall.Definition);
		Assert.Equal([CommonNames.VoidSignatureChar,], call.ReturnType);

		env.ReferenceFeature.Unload(Arg.Any<JLocalObject>()).Returns(true);
		env.ClassFeature.GetClass(jClass.GetInformation()).Returns(jClass);
		env.ClassFeature.GetClass<TDataType>().Returns(jClass);
		env.AccessFeature
		   .CallConstructor<JLocalObject>(jClass, (JConstructorDefinition)call.Definition, Arg.Any<IObject?[]>())
		   .Returns(instance);
		env.AccessFeature.CallConstructor<JLocalObject>(jConstructor, jConstructor.Definition, Arg.Any<IObject?[]>())
		   .Returns(instance);
		env.AccessFeature
		   .CallConstructor<TDataType>(jClass, (JConstructorDefinition)call.Definition, Arg.Any<IObject?[]>())
		   .Returns(instance);
		env.AccessFeature.CallConstructor<TDataType>(jConstructor, jConstructor.Definition, Arg.Any<IObject?[]>())
		   .Returns(instance);

		if (isAbstract) return;

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		Assert.Equal(instance, call.NewCall<TDataType>(env, parameters));
		env.AccessFeature.Received(1).CallConstructor<TDataType>(jClass, (JConstructorDefinition)call.Definition,
		                                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		Assert.Equal(instance, call.NewCall(jClass, parameters));
		env.AccessFeature.Received(1).CallConstructor<JLocalObject>(jClass, (JConstructorDefinition)call.Definition,
		                                                            Arg.Is<IObject[]>(
			                                                            a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		Assert.Equal(instance, call.StaticFunctionCall(jClass, parameters).Object);
		env.AccessFeature.Received(1).CallConstructor<JLocalObject>(jClass, (JConstructorDefinition)call.Definition,
		                                                            Arg.Is<IObject[]>(
			                                                            a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		Assert.Equal(localRef, instance?.LocalReference ?? localRef);
		call.StaticMethodCall(jClass, parameters);
		env.AccessFeature.Received(1).CallConstructor<JLocalObject>(jClass, (JConstructorDefinition)call.Definition,
		                                                            Arg.Is<IObject[]>(
			                                                            a => a.SequenceEqual(parameters)));
		Assert.Equal(default, instance?.LocalReference ?? default);
		instance?.SetValue(localRef);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		Assert.Equal(instance, IndeterminateCall.ReflectedNewCall<TDataType>(jConstructor, parameters));
		env.AccessFeature.Received(1).CallConstructor<TDataType>(jConstructor, jConstructor.Definition,
		                                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		Assert.Equal(instance, IndeterminateCall.ReflectedNewCall(jConstructor, parameters));
		env.AccessFeature.Received(1).CallConstructor<JLocalObject>(jConstructor, jConstructor.Definition,
		                                                            Arg.Is<IObject[]>(
			                                                            a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		Assert.Equal(instance, IndeterminateCall.ReflectedStaticFunctionCall(jConstructor, parameters).Object);
		env.AccessFeature.Received(1).CallConstructor<JLocalObject>(jConstructor, jConstructor.Definition,
		                                                            Arg.Is<IObject[]>(
			                                                            a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		Assert.Equal(localRef, instance?.LocalReference ?? localRef);
		IndeterminateCall.ReflectedStaticMethodCall(jConstructor, parameters);
		env.AccessFeature.Received(1).CallConstructor<JLocalObject>(jConstructor, jConstructor.Definition,
		                                                            Arg.Is<IObject[]>(
			                                                            a => a.SequenceEqual(parameters)));
		Assert.Equal(default, instance?.LocalReference ?? default);
		instance?.SetValue(localRef);
	}
}