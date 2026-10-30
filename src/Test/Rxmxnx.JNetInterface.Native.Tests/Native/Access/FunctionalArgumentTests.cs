// ReSharper disable UnusedMethodReturnValue.Local

// ReSharper disable RedundantExplicitParamsArrayCreation

namespace Rxmxnx.JNetInterface.Tests.Native.Access;

/// <summary>
/// Exercises the default <see cref="IAccessFeature"/> argument adapters. The real environment replaces them.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class FunctionalArgumentTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void DefaultFeatureAdaptsCallArguments()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JClassLocalRef classRef = FunctionalArgumentTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef stringRef = FunctionalArgumentTests.fixture.Create<JStringLocalRef>();
		JObjectLocalRef localRef = FunctionalArgumentTests.fixture.Create<JObjectLocalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jStringClass = new(jClass, IDataType.GetMetadata<JStringObject>(), classRef);
		using JStringObject jString = new(jStringClass, stringRef, FunctionalArgumentTests.fixture.Create<String>());
		OpenMethod method = new((CString)FunctionalArgumentTests.fixture.Create<String>());
		OpenFunction function = new((CString)FunctionalArgumentTests.fixture.Create<String>());
		OpenConstructor constructor = new();
		using JClassObject jMethodClass = new(jClass, IDataType.GetMetadata<JMethodObject>(),
		                                      FunctionalArgumentTests.fixture.Create<JClassLocalRef>());
		using JClassObject jConstructorClass = new(jClass, IDataType.GetMetadata<JConstructorObject>(),
		                                           FunctionalArgumentTests.fixture.Create<JClassLocalRef>());
		using JMethodObject jMethod = new(jMethodClass, localRef, method, jStringClass);
		using JConstructorObject jConstructor = new(jConstructorClass,
		                                            FunctionalArgumentTests.fixture.Create<JObjectLocalRef>(),
		                                            constructor, jStringClass);
		ValueArgument value = default;
		JLocalObject created = new(jStringClass, FunctionalArgumentTests.fixture.Create<JObjectLocalRef>());

		env.AccessFeature.CallConstructor<JLocalObject>(Arg.Any<JClassObject>(), constructor, Arg.Any<IObject?[]>())
		   .Returns(created);
		env.AccessFeature
		   .CallConstructor<JLocalObject>(Arg.Any<JConstructorObject>(), constructor, Arg.Any<IObject?[]>())
		   .Returns(created);

		method.InvokeValue(jString, ref value);
		method.InvokeClass(jString, default);
		method.InvokeReflectedValue(jMethod, jString, ref value);
		method.StaticInvokeValue(jStringClass, ref value);
		function.InvokeValue(jString, ref value);
		function.StaticInvokeClass(jStringClass, new());
		Assert.Same(created, constructor.NewValue(jStringClass, ref value));
		Assert.Same(created, constructor.NewReflectedClass(jConstructor, default));

		Span<Byte> bytes = stackalloc Byte[1];
		IAccessFeature feature = env.AccessFeature;
		feature.CallPrimitiveFunction(bytes, jString, jStringClass, function, false, ref value);
		feature.CallStaticPrimitiveFunction(bytes, jStringClass, function, ref value);

		env.AccessFeature.Received(2).CallMethod(jString, jStringClass, method, false, Arg.Any<IObject?[]>());
		env.AccessFeature.Received(1).CallMethod(jMethod, jString, method, false, Arg.Any<IObject?[]>());
		env.AccessFeature.Received(1).CallStaticMethod(jStringClass, method, Arg.Any<IObject?[]>());
		env.AccessFeature.Received(1).CallFunction<JStringObject>(jString, jStringClass, function, false,
		                                                          Arg.Any<IObject?[]>());
		env.AccessFeature.Received(1).CallStaticFunction<JStringObject>(jStringClass, function, Arg.Any<IObject?[]>());
		env.AccessFeature.Received(1).CallConstructor<JLocalObject>(
			Arg.Any<JClassObject>(), constructor, Arg.Any<IObject?[]>());
		env.AccessFeature.Received(1).CallConstructor<JLocalObject>(
			Arg.Any<JConstructorObject>(), constructor, Arg.Any<IObject?[]>());
		created.Dispose();
	}

	private readonly struct ValueArgument : ICallArgument
	{
		public void Configure<TSlot>(TSlot slot) where TSlot : IParameterSlot { }
	}

	private sealed class ClassArgument : ICallArgument
	{
		public void Configure<TSlot>(TSlot slot) where TSlot : IParameterSlot { }
	}

	private sealed class OpenMethod(ReadOnlySpan<Byte> methodName) : JMethodDefinition(methodName, [])
	{
		public void InvokeValue(JLocalObject jLocal, ref ValueArgument args) => this.Invoke(jLocal, ref args);
		public void InvokeClass(JLocalObject jLocal, ClassArgument? args) => this.Invoke(jLocal, args);
		public void InvokeReflectedValue(JMethodObject jMethod, JLocalObject jLocal, ref ValueArgument args)
			=> this.InvokeReflected(jMethod, jLocal, ref args);
		public void StaticInvokeValue(JClassObject jClass, ref ValueArgument args)
			=> this.StaticInvoke(jClass, ref args);
	}

	private sealed class OpenFunction(ReadOnlySpan<Byte> functionName)
		: JFunctionDefinition<JStringObject>(functionName, [])
	{
		public JStringObject? InvokeValue(JLocalObject jLocal, ref ValueArgument args) => this.Invoke(jLocal, ref args);
		public JStringObject? StaticInvokeClass(JClassObject jClass, ClassArgument? args)
			=> this.StaticInvoke(jClass, args);
	}

	private sealed class OpenConstructor() : JConstructorDefinition([])
	{
		public JLocalObject NewValue(JClassObject jClass, ref ValueArgument args) => this.New(jClass, ref args);
		public JLocalObject NewReflectedClass(JConstructorObject jConstructor, ClassArgument? args)
			=> this.NewReflected(jConstructor, args);
	}
}