// ReSharper disable AccessToDisposedClosure

namespace Rxmxnx.JNetInterface.Tests.Native.Access;

/// <summary>
/// Covers <see cref="JNonTypedFunctionDefinition"/> generic arguments.
/// A value argument changes only when the call receives it by reference.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class JNonTypedFunctionDefinitionTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void InvokeDistinguishesValueReferenceClassAndNullArguments()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JNonTypedFunctionDefinitionTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = JNonTypedFunctionDefinitionTests.fixture.Create<JClassLocalRef>();
		using JClassObject root = new(env);
		using JClassObject jClass = new(root, IDataType.GetMetadata<JStringObject>(), classRef);
		using JStringObject instance = new(jClass, JNonTypedFunctionDefinitionTests.fixture.Create<JStringLocalRef>(),
		                                   "result");
		using JClassObject jMethodClass = new(root, IDataType.GetMetadata<JMethodObject>(),
		                                      JNonTypedFunctionDefinitionTests.fixture.Create<JClassLocalRef>());
		JNonTypedFunctionDefinition definition = new((CString)JNonTypedFunctionDefinitionTests.fixture.Create<String>(),
		                                             IDataType.GetMetadata<JLocalObject>().Signature);
		using JMethodObject jMethod = new(jMethodClass, localRef, definition, jClass);

		env.AccessFeature.CallFunction<JLocalObject>(Arg.Any<JLocalObject>(), Arg.Any<JClassObject>(), definition,
		                                             Arg.Any<Boolean>(), Arg.Any<IObject?[]>()).Returns(instance);
		env.AccessFeature.CallFunction<JLocalObject>(Arg.Any<JMethodObject>(), Arg.Any<JLocalObject>(), definition,
		                                             Arg.Any<Boolean>(), Arg.Any<IObject?[]>()).Returns(instance);
		env.AccessFeature.CallStaticFunction<JLocalObject>(Arg.Any<JClassObject>(), definition, Arg.Any<IObject?[]>())
		   .Returns(instance);
		env.AccessFeature.CallStaticFunction<JLocalObject>(Arg.Any<JMethodObject>(), definition, Arg.Any<IObject?[]>())
		   .Returns(instance);

		JNonTypedFunctionDefinitionTests.AssertValueAndReference(args => definition.Invoke(instance, args),
		                                                         (ref args) => definition.Invoke(instance, ref args),
		                                                         instance);
		JNonTypedFunctionDefinitionTests.AssertValueAndReference(args => definition.Invoke(instance, jClass, args),
		                                                         (ref args) => definition.Invoke(
			                                                         instance, jClass, ref args), instance);
		JNonTypedFunctionDefinitionTests.AssertValueAndReference(
			args => definition.InvokeNonVirtual(instance, jClass, args),
			(ref args) => definition.InvokeNonVirtual(instance, jClass, ref args), instance);
		JNonTypedFunctionDefinitionTests.AssertValueAndReference(args => definition.StaticInvoke(jClass, args),
		                                                         (ref args) => definition.StaticInvoke(
			                                                         jClass, ref args), instance);
		JNonTypedFunctionDefinitionTests.AssertValueAndReference(
			args => definition.InvokeReflected(jMethod, instance, args),
			(ref args) => definition.InvokeReflected(jMethod, instance, ref args), instance);
		JNonTypedFunctionDefinitionTests.AssertValueAndReference(
			args => definition.InvokeNonVirtualReflected(jMethod, instance, args),
			(ref args) => definition.InvokeNonVirtualReflected(jMethod, instance, ref args), instance);
		JNonTypedFunctionDefinitionTests.AssertValueAndReference(
			args => definition.InvokeStaticReflected(jMethod, args),
			(ref args) => definition.InvokeStaticReflected(jMethod, ref args), instance);

		JNonTypedFunctionDefinitionTests.AssertClassAndNull(classArgument => definition.Invoke(instance, classArgument),
		                                                    () => definition.Invoke(instance, (ClassArgument?)null),
		                                                    instance);
		JNonTypedFunctionDefinitionTests.AssertClassAndNull(
			classArgument => definition.Invoke(instance, jClass, classArgument),
			() => definition.Invoke(instance, jClass, (ClassArgument?)null), instance);
		JNonTypedFunctionDefinitionTests.AssertClassAndNull(
			classArgument => definition.InvokeNonVirtual(instance, jClass, classArgument),
			() => definition.InvokeNonVirtual(instance, jClass, (ClassArgument?)null), instance);
		JNonTypedFunctionDefinitionTests.AssertClassAndNull(
			classArgument => definition.StaticInvoke(jClass, classArgument),
			() => definition.StaticInvoke(jClass, (ClassArgument?)null), instance);
		JNonTypedFunctionDefinitionTests.AssertClassAndNull(
			classArgument => definition.InvokeReflected(jMethod, instance, classArgument),
			() => definition.InvokeReflected(jMethod, instance, (ClassArgument?)null), instance);
		JNonTypedFunctionDefinitionTests.AssertClassAndNull(
			classArgument => definition.InvokeNonVirtualReflected(jMethod, instance, classArgument),
			() => definition.InvokeNonVirtualReflected(jMethod, instance, (ClassArgument?)null), instance);
		JNonTypedFunctionDefinitionTests.AssertClassAndNull(
			classArgument => definition.InvokeStaticReflected(jMethod, classArgument),
			() => definition.InvokeStaticReflected(jMethod, (ClassArgument?)null), instance);
	}

	private static void AssertValueAndReference(Func<CountingArgument, JLocalObject?> byValue, ByReference byReference,
		JLocalObject expected)
	{
		CountingArgument value = new() { Configurations = 1, };
		Assert.Same(expected, byValue(value));
		Assert.Equal(1, value.Configurations);

		CountingArgument reference = new() { Configurations = 1, };
		Assert.Same(expected, byReference(ref reference));
		Assert.Equal(2, reference.Configurations);
	}
	private static void AssertClassAndNull(Func<ClassArgument, JLocalObject?> byClass, Func<JLocalObject?> byNull,
		JLocalObject expected)
	{
		ClassArgument classArgument = new();
		Assert.Same(expected, byClass(classArgument));
		Assert.Equal(1, classArgument.Configurations);
		Assert.Same(expected, byNull());
	}

	private delegate JLocalObject? ByReference(ref CountingArgument args);

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