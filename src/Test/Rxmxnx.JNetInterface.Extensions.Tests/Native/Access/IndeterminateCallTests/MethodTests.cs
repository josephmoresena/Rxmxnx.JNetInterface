using JMethodDefinition = Rxmxnx.JNetInterface.Native.Access.JMethodDefinition;

namespace Rxmxnx.JNetInterface.Tests.Native.Access.IndeterminateCallTests;

[ExcludeFromCodeCoverage]
public sealed class MethodTests : IndeterminateAccessTestsBase
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
		ReadOnlySpan<Byte> methodName = (CString)MethodTests.fixture.Create<String>();
		JMethodDefinition definition = new JMethodDefinition.Parameterless(methodName);
		IndeterminateCall call = IndeterminateCall.CreateMethodDefinition(methodName, []);
		IndeterminateCall operatorCall = definition;

		Assert.Equal(call.Definition, operatorCall.Definition);
		Assert.Equal(call.Definition.Name, operatorCall.Definition.Name);
		Assert.Equal(call.Definition.Descriptor, operatorCall.Definition.Descriptor);
		Assert.Equal(call.Definition.Hash, operatorCall.Definition.Hash);
		Assert.Equal(methodName, operatorCall.Definition.Name);
		Assert.Equal("()V"u8, operatorCall.Definition.Descriptor);
		Assert.Equal("V"u8, operatorCall.ReturnType.AsSpan());
		Assert.Equal(call.ReturnType, operatorCall.ReturnType);

		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClass = new(env);

		IndeterminateAccessTestsBase.EmptyCompare(call.FunctionCall(jClass, []));
		IndeterminateAccessTestsBase.EmptyCompare(call.FunctionCall(jClass, jClass, true, []));
		IndeterminateAccessTestsBase.EmptyCompare(call.StaticFunctionCall(jClass, []));

		Assert.Null((IndeterminateCall?)default(JMethodDefinition));
		Assert.Throws<InvalidOperationException>(() => call.NewCall(jClass, []));
		Assert.Throws<InvalidOperationException>(() => call.NewCall<JLocalObject>(env, []));
	}

	[Fact]
	internal void ObjectTest() => MethodTests.Test<JLocalObject>();
	[Fact]
	internal void ClassTest() => MethodTests.Test<JClassObject>();
	[Fact]
	internal void StringTest() => MethodTests.Test<JStringObject>();
	[Fact]
	internal void ThrowableTest() => MethodTests.Test<JThrowableObject>();
	[Fact]
	internal void EnumTest() => MethodTests.Test<JEnumObject>();
	[Fact]
	internal void NumberTest() => MethodTests.Test<JNumberObject>();
	[Fact]
	internal void AccessibleObjectTest() => MethodTests.Test<JAccessibleObject>();
	[Fact]
	internal void ExecutableTest() => MethodTests.Test<JExecutableObject>();
	[Fact]
	internal void ModifierTest() => MethodTests.Test<JModifierObject>();
	[Fact]
	internal void ProxyTest() => MethodTests.Test<JProxyObject>();

	[Fact]
	internal void ErrorTest() => MethodTests.Test<JErrorObject>();
	[Fact]
	internal void ExceptionTest() => MethodTests.Test<JExceptionObject>();
	[Fact]
	internal void RuntimeExceptionTest() => MethodTests.Test<JRuntimeExceptionObject>();
	[Fact]
	internal void BufferTest() => MethodTests.Test<JBufferObject>();

	[Fact]
	internal void FieldTest() => MethodTests.Test<JFieldObject>();
	[Fact]
	internal void MethodTest() => MethodTests.Test<JMethodObject>();
	[Fact]
	internal void ConstructorTest() => MethodTests.Test<JConstructorObject>();
	[Fact]
	internal void StackTraceElementTest() => MethodTests.Test<JStackTraceElementObject>();

	private static void Test<TDataType>() where TDataType : JLocalObject, IClassType<TDataType>
	{
		ReadOnlySpan<Byte> methodName = (CString)MethodTests.fixture.Create<String>();
		JMethodDefinition definition = JMethodDefinition.Create(methodName, MethodTests.args);
		IndeterminateCall call = IndeterminateCall.CreateMethodDefinition(methodName, MethodTests.args);
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<TDataType>();
		JObjectLocalRef localRef = MethodTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = MethodTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef stringRef = MethodTests.fixture.Create<JStringLocalRef>();
		String textValue = MethodTests.fixture.Create<String>();

		if (typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/Class"u8)) return;

		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClassClass = new(env);
		using JClassObject jClass = new(jClassClass, typeMetadata, classRef);
		using JClassObject jStringClass = !typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/String"u8) ?
			new(jClassClass, IClassType.GetMetadata<JStringObject>(), MethodTests.fixture.Create<JClassLocalRef>()) :
			jClass;
		using JClassObject jMethodClass = !typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/Method"u8) ?
			new(jClassClass, IClassType.GetMetadata<JMethodObject>(), MethodTests.fixture.Create<JClassLocalRef>()) :
			jClass;
		using JStringObject jString = new(jStringClass, stringRef, textValue);
		using JMethodObject jMethod = new(jMethodClass, MethodTests.fixture.Create<JObjectLocalRef>(), definition,
		                                  jClass);

		if (typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/String"u8)) return;

		using TDataType instance = Assert.IsType<TDataType>(
			typeMetadata.ParseInstance(typeMetadata.CreateInstance(jClass, localRef, true)));
		IObject?[] parameters = [jClass, jString, JInt.NegativeOne,];

		Assert.Equal(definition, call.Definition);
		Assert.Equal([CommonNames.VoidSignatureChar,], call.ReturnType);

		env.ClassFeature.GetClass<TDataType>().Returns(jClass);
		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		call.MethodCall(instance, parameters);
		env.AccessFeature.Received(1).CallMethod(instance, instance.Class, (JMethodDefinition)call.Definition, false,
		                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		call.MethodCall(instance, jClassClass, false, parameters);
		env.AccessFeature.Received(1).CallMethod(instance, jClassClass, (JMethodDefinition)call.Definition, false,
		                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		call.MethodCall(instance, instance.Class, true, parameters);
		env.AccessFeature.Received(1).CallMethod(instance, instance.Class, (JMethodDefinition)call.Definition, true,
		                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		call.StaticMethodCall(jMethodClass, parameters);
		env.AccessFeature.Received(1).CallStaticMethod(jMethodClass, (JMethodDefinition)call.Definition,
		                                               Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCall.ReflectedMethodCall(jMethod, instance, parameters);
		env.AccessFeature.Received(1).CallMethod(jMethod, instance, (JMethodDefinition)jMethod.Definition, false,
		                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCall.ReflectedMethodCall(jMethod, instance, true, parameters);
		env.AccessFeature.Received(1).CallMethod(jMethod, instance, (JMethodDefinition)jMethod.Definition, true,
		                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateCall.ReflectedStaticMethodCall(jMethod, parameters);
		env.AccessFeature.Received(1).CallStaticMethod(jMethod, (JMethodDefinition)jMethod.Definition,
		                                               Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateAccessTestsBase.EmptyCompare(call.FunctionCall(instance, parameters));
		env.AccessFeature.Received(1).CallMethod(instance, instance.Class, (JMethodDefinition)call.Definition, false,
		                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateAccessTestsBase.EmptyCompare(call.FunctionCall(instance, jClassClass, false, parameters));
		env.AccessFeature.Received(1).CallMethod(instance, jClassClass, (JMethodDefinition)call.Definition, false,
		                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateAccessTestsBase.EmptyCompare(call.FunctionCall(instance, instance.Class, true, parameters));
		env.AccessFeature.Received(1).CallMethod(instance, instance.Class, (JMethodDefinition)call.Definition, true,
		                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateAccessTestsBase.EmptyCompare(call.StaticFunctionCall(jMethodClass, parameters));
		env.AccessFeature.Received(1).CallStaticMethod(jMethodClass, (JMethodDefinition)call.Definition,
		                                               Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateAccessTestsBase.EmptyCompare(
			IndeterminateCall.ReflectedFunctionCall(jMethod, instance, parameters));
		env.AccessFeature.Received(1).CallMethod(jMethod, instance, (JMethodDefinition)jMethod.Definition, false,
		                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateAccessTestsBase.EmptyCompare(
			IndeterminateCall.ReflectedFunctionCall(jMethod, instance, true, parameters));
		env.AccessFeature.Received(1).CallMethod(jMethod, instance, (JMethodDefinition)jMethod.Definition, true,
		                                         Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();

		IndeterminateAccessTestsBase.EmptyCompare(IndeterminateCall.ReflectedStaticFunctionCall(jMethod, parameters));
		env.AccessFeature.Received(1).CallStaticMethod(jMethod, (JMethodDefinition)jMethod.Definition,
		                                               Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));
	}
}