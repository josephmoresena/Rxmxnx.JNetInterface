namespace Rxmxnx.JNetInterface.Tests.Native.Access.IndeterminateCallTests;

/// <summary>
/// Covers the generic <see cref="IndeterminateCall"/> argument overloads.
/// A value argument changes only when the call receives it by reference.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class FunctionalArgumentTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void MethodCallDistinguishesValueAndReferenceArguments()
	{
		FunctionalArgumentTests.WithInstance((env, instance, jClass) =>
		{
			IndeterminateCall call = IndeterminateCall.CreateMethodDefinition(
				(CString)FunctionalArgumentTests.fixture.Create<String>());
			FunctionalArgumentTests.AssertValueAndReference(args => call.MethodCall(instance, args),
			                                                (ref args) => call.MethodCall(instance, ref args));
			FunctionalArgumentTests.AssertValueAndReference(args => call.MethodCall(instance, jClass, true, args),
			                                                (ref args) => call.MethodCall(
				                                                instance, jClass, true, ref args));
			FunctionalArgumentTests.AssertValueAndReference(args => call.StaticMethodCall(jClass, args),
			                                                (ref args) => call.StaticMethodCall(jClass, ref args));
			call.MethodCall(instance, (ClassArgument?)null);
			env.AccessFeature.Received().CallMethod(instance, instance.Class, (JMethodDefinition)call.Definition, false,
			                                        Arg.Any<IObject?[]>());
		});
	}
	[Fact]
	internal void FunctionCallDistinguishesValueAndReferenceArguments()
	{
		FunctionalArgumentTests.WithInstance((env, instance, jClass) =>
		{
			IndeterminateCall primitive = IndeterminateCall.CreateFunctionDefinition<JInt>(
				(CString)FunctionalArgumentTests.fixture.Create<String>());
			IndeterminateCall reference = IndeterminateCall.CreateFunctionDefinition<JStringObject>(
				(CString)FunctionalArgumentTests.fixture.Create<String>());
			JInt primitiveResult = 11;
			env.AccessFeature.CallFunction<JInt>(Arg.Any<JLocalObject>(), Arg.Any<JClassObject>(),
			                                     Arg.Any<JFunctionDefinition>(), Arg.Any<Boolean>(),
			                                     Arg.Any<IObject?[]>()).Returns(primitiveResult);
			env.AccessFeature.CallStaticFunction<JInt>(Arg.Any<JClassObject>(), Arg.Any<JFunctionDefinition>(),
			                                           Arg.Any<IObject?[]>()).Returns(primitiveResult);
			env.AccessFeature.CallFunction<JLocalObject>(Arg.Any<JLocalObject>(), Arg.Any<JClassObject>(),
			                                             Arg.Any<JFunctionDefinition>(), Arg.Any<Boolean>(),
			                                             Arg.Any<IObject?[]>()).Returns(instance);
			env.AccessFeature.CallStaticFunction<JLocalObject>(Arg.Any<JClassObject>(), Arg.Any<JFunctionDefinition>(),
			                                                   Arg.Any<IObject?[]>()).Returns(instance);

			FunctionalArgumentTests.AssertValueAndReference(args =>
			{
				IndeterminateResult result = primitive.FunctionCall(instance, args);
				Assert.Equal(primitiveResult, result.IntValue);
			}, (ref args) =>
			{
				IndeterminateResult result = primitive.FunctionCall(instance, ref args);
				Assert.Equal(primitiveResult, result.IntValue);
			});
			FunctionalArgumentTests.AssertValueAndReference(args =>
			{
				IndeterminateResult result = primitive.FunctionCall(instance, jClass, true, args);
				Assert.Equal(primitiveResult, result.IntValue);
			}, (ref args) =>
			{
				IndeterminateResult result = primitive.FunctionCall(instance, jClass, true, ref args);
				Assert.Equal(primitiveResult, result.IntValue);
			});
			FunctionalArgumentTests.AssertValueAndReference(args =>
			{
				IndeterminateResult result = primitive.StaticFunctionCall(jClass, args);
				Assert.Equal(primitiveResult, result.IntValue);
			}, (ref args) =>
			{
				IndeterminateResult result = primitive.StaticFunctionCall(jClass, ref args);
				Assert.Equal(primitiveResult, result.IntValue);
			});
			FunctionalArgumentTests.AssertValueAndReference(args =>
			{
				IndeterminateResult result = reference.StaticFunctionCall(jClass, args);
				Assert.Same(instance, result.Object);
			}, (ref args) =>
			{
				IndeterminateResult result = reference.StaticFunctionCall(jClass, ref args);
				Assert.Same(instance, result.Object);
			});
		});
	}
	[Fact]
	internal void ConstructorCallDistinguishesValueAndReferenceArguments()
	{
		FunctionalArgumentTests.WithInstance((env, instance, jClass) =>
		{
			IndeterminateCall call = IndeterminateCall.CreateConstructorDefinition();
			env.AccessFeature
			   .CallConstructor<JLocalObject>(jClass, Arg.Any<JConstructorDefinition>(), Arg.Any<IObject?[]>())
			   .Returns(instance);
			FunctionalArgumentTests.AssertValueAndReference(args =>
			{
				JLocalObject created = call.NewCall(jClass, args);
				Assert.Same(instance, created);
			}, (ref args) =>
			{
				JLocalObject created = call.NewCall(jClass, ref args);
				Assert.Same(instance, created);
			});

			ClassArgument classArgument = new();
			Assert.Same(instance, call.NewCall(jClass, classArgument));
			Assert.Equal(1, classArgument.Configurations);
		});
	}

	private static void AssertValueAndReference(Action<CountingArgument> byValue, ByReference byReference)
	{
		CountingArgument value = new() { Configurations = 1, };
		byValue(value);
		Assert.Equal(1, value.Configurations);

		CountingArgument reference = new() { Configurations = 1, };
		byReference(ref reference);
		Assert.Equal(2, reference.Configurations);
	}
	private static void WithInstance(Action<EnvironmentProxy, JStringObject, JClassObject> test)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject root = new(env);
		using JClassObject jClass = new(root, IClassType.GetMetadata<JStringObject>(),
		                                FunctionalArgumentTests.fixture.Create<JClassLocalRef>());
		using JStringObject instance = new(jClass, FunctionalArgumentTests.fixture.Create<JStringLocalRef>(),
		                                   "argument");
		test(env, instance, jClass);
	}

	private delegate void ByReference(ref CountingArgument args);

	/// <summary>
	/// Value argument whose <see cref="Configure{TSlot}"/> records that the call reached it.
	/// </summary>
	private struct CountingArgument : ICallArgument
	{
		public Int32 Configurations { get; set; }
		public void Configure<TSlot>(TSlot slot) where TSlot : IParameterSlot => this.Configurations++;
	}

	/// <summary>
	/// Reference argument. Passing it by value still reaches the same instance.
	/// </summary>
	private sealed class ClassArgument : ICallArgument
	{
		public Int32 Configurations { get; private set; }
		public void Configure<TSlot>(TSlot slot) where TSlot : IParameterSlot => this.Configurations++;
	}
}