namespace Rxmxnx.JNetInterface.Tests.Native.Access;

[ExcludeFromCodeCoverage]
public sealed class JMethodDefinitionTests
{
	private const String VoidParameterlessDescriptor = "()V";

	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly JArgumentMetadata[] args =
		[JArgumentMetadata.Create<JClassObject>(), JArgumentMetadata.Create<JStringObject>(),];
	private static readonly String methodDescriptor =
		$"({String.Concat(JMethodDefinitionTests.args.Select(a => a.Signature))})V";

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	private void SimpleTest(Boolean nonVirtual)
	{
		String methodName = JMethodDefinitionTests.fixture.Create<String>();
		CStringSequence seq = new(methodName, JMethodDefinitionTests.VoidParameterlessDescriptor);
		JMethodDefinition.Parameterless methodDefinition = new((CString)methodName);
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JMethodDefinitionTests.fixture.Create<JObjectLocalRef>();
		JStringLocalRef stringRef = JMethodDefinitionTests.fixture.Create<JStringLocalRef>();
		JClassLocalRef classRef0 = JMethodDefinitionTests.fixture.Create<JClassLocalRef>();
		JClassLocalRef classRef1 = JMethodDefinitionTests.fixture.Create<JClassLocalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jStringClass = new(jClass, IDataType.GetMetadata<JStringObject>(), classRef0);
		using JClassObject jMethodClass = new(jClass, IDataType.GetMetadata<JMethodObject>(), classRef1);
		using JStringObject jString = new(jStringClass, stringRef, JMethodDefinitionTests.fixture.Create<String>());
		using JMethodObject jMethod = new(jMethodClass, localRef, methodDefinition, jStringClass);

		env.AccessFeature.GetReflectedMethod(methodDefinition, jStringClass, Arg.Any<Boolean>()).Returns(jMethod);

		Assert.Equal(seq.ToString(), methodDefinition.Information.ToString());
		Assert.Equal(methodDefinition.Information.GetHashCode(), methodDefinition.GetHashCode());

		Assert.False(methodDefinition.Equals(default));
		Assert.True(methodDefinition.Equals((Object)methodDefinition));
		Assert.True(methodDefinition.Equals((Object)new JMethodDefinition.Parameterless((CString)methodName)));
		Assert.Equal(0, methodDefinition.Count);
		Assert.Equal(0, methodDefinition.ReferenceCount);
		Assert.Equal(0, methodDefinition.Size);
		Assert.Empty(methodDefinition.Sizes);
		Assert.Equal(jMethod, methodDefinition.GetReflected(jStringClass));
		Assert.Equal(jMethod, methodDefinition.GetStaticReflected(jStringClass));

		Assert.Equal($"{{ Method: {methodName} Descriptor: {JMethodDefinitionTests.VoidParameterlessDescriptor} }}",
		             methodDefinition.ToString());

		JMethodDefinition.Invoke(methodDefinition, jString, nonVirtual: nonVirtual);
		JMethodDefinition.Invoke(methodDefinition, jString, jClass, nonVirtual);
		JMethodDefinition.StaticInvoke(methodDefinition, jClass);

		env.AccessFeature.Received(1).CallMethod(jString, jStringClass, methodDefinition, nonVirtual,
		                                         Arg.Is<IObject?[]>(i => i.Length == 0));
		env.AccessFeature.Received(1).CallMethod(jString, jClass, methodDefinition, nonVirtual,
		                                         Arg.Is<IObject?[]>(i => i.Length == 0));
		env.AccessFeature.Received(1)
		   .CallStaticMethod(jClass, methodDefinition, Arg.Is<IObject?[]>(i => i.Length == 0));

		env.AccessFeature.Received(1).GetReflectedMethod(methodDefinition, jStringClass, true);
		env.AccessFeature.Received(1).GetReflectedMethod(methodDefinition, jStringClass, false);
	}
	[Fact]
	internal void Test()
	{
		String methodName = JMethodDefinitionTests.fixture.Create<String>();
		CStringSequence seq = new(methodName, JMethodDefinitionTests.methodDescriptor);
		JFakeMethodDefinition methodDefinition = new((CString)methodName, JMethodDefinitionTests.args);
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef0 = JMethodDefinitionTests.fixture.Create<JObjectLocalRef>();
		JObjectLocalRef localRef1 = JMethodDefinitionTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef0 = JMethodDefinitionTests.fixture.Create<JClassLocalRef>();
		JClassLocalRef classRef1 = JMethodDefinitionTests.fixture.Create<JClassLocalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jStringClass = new(jClass, IDataType.GetMetadata<JStringObject>(), classRef0);
		using JClassObject jMethodClass = new(jClass, IDataType.GetMetadata<JMethodObject>(), classRef1);
		using JLocalObject jLocal = new(jStringClass, localRef0);
		using JMethodObject jMethod = new(jMethodClass, localRef1, methodDefinition, jClass);
		IObject?[] parameters = [jClass, jStringClass, jLocal,];

		Assert.Equal(seq.ToString(), methodDefinition.Information.ToString());
		Assert.Equal(methodDefinition.Information.GetHashCode(), methodDefinition.GetHashCode());

		Assert.False(methodDefinition.Equals(default));
		Assert.True(methodDefinition.Equals((Object)methodDefinition));
		Assert.True(
			methodDefinition.Equals(
				(Object)new JFakeMethodDefinition((CString)methodName, JMethodDefinitionTests.args)));
		Assert.Equal(JMethodDefinitionTests.args.Length, methodDefinition.Count);
		Assert.Equal(JMethodDefinitionTests.args.Count(a => a.Signature.Length > 1), methodDefinition.ReferenceCount);
		Assert.Equal(JMethodDefinitionTests.args.Select(a => a.Size), methodDefinition.Sizes);
		Assert.Equal(JMethodDefinitionTests.args.Select(a => a.Size).Sum(), methodDefinition.Size);

		methodDefinition.Invoke(jLocal);
		env.AccessFeature.Received(1).CallMethod(jLocal, jLocal.Class, methodDefinition, false,
		                                         Arg.Is<IObject?[]>(a => JMethodDefinitionTests.IsEmptyArgs(a)));

		methodDefinition.Invoke(jLocal, jClass);
		env.AccessFeature.Received(1).CallMethod(jLocal, jClass, methodDefinition, false,
		                                         Arg.Is<IObject?[]>(a => JMethodDefinitionTests.IsEmptyArgs(a)));

		methodDefinition.Invoke(jLocal, parameters);
		env.AccessFeature.Received(1).CallMethod(jLocal, jLocal.Class, methodDefinition, false, parameters);

		methodDefinition.Invoke(jLocal, jClass, parameters);
		env.AccessFeature.Received(1).CallMethod(jLocal, jClass, methodDefinition, false, parameters);

		methodDefinition.InvokeNonVirtual(jLocal, jClass);
		env.AccessFeature.Received(1).CallMethod(jLocal, jClass, methodDefinition, true,
		                                         Arg.Is<IObject?[]>(a => JMethodDefinitionTests.IsEmptyArgs(a)));

		methodDefinition.InvokeNonVirtual(jLocal, jClass, parameters);
		env.AccessFeature.Received(1).CallMethod(jLocal, jClass, methodDefinition, true, parameters);

		methodDefinition.StaticInvoke(jStringClass);
		env.AccessFeature.Received(1).CallStaticMethod(jStringClass, methodDefinition,
		                                               Arg.Is<IObject?[]>(a => JMethodDefinitionTests.IsEmptyArgs(a)));

		methodDefinition.StaticInvoke(jClass, parameters);
		env.AccessFeature.Received(1).CallStaticMethod(jClass, methodDefinition, parameters);

		methodDefinition.InvokeReflected(jMethod, jLocal);
		env.AccessFeature.Received(1).CallMethod(jMethod, jLocal, methodDefinition, false,
		                                         Arg.Is<IObject?[]>(a => JMethodDefinitionTests.IsEmptyArgs(a)));

		methodDefinition.InvokeReflected(jMethod, jLocal, parameters);
		env.AccessFeature.Received(1).CallMethod(jMethod, jLocal, methodDefinition, false, parameters);

		methodDefinition.InvokeNonVirtualReflected(jMethod, jLocal);
		env.AccessFeature.Received(1).CallMethod(jMethod, jLocal, methodDefinition, true,
		                                         Arg.Is<IObject?[]>(a => JMethodDefinitionTests.IsEmptyArgs(a)));

		methodDefinition.InvokeNonVirtualReflected(jMethod, jLocal, parameters);
		env.AccessFeature.Received(1).CallMethod(jMethod, jLocal, methodDefinition, true, parameters);

		methodDefinition.InvokeStaticReflected(jMethod);
		env.AccessFeature.Received(1).CallStaticMethod(jMethod, methodDefinition,
		                                               Arg.Is<IObject?[]>(a => JMethodDefinitionTests.IsEmptyArgs(a)));

		methodDefinition.InvokeStaticReflected(jMethod, parameters);
		env.AccessFeature.Received(1).CallStaticMethod(jMethod, methodDefinition, parameters);
	}

	private static Boolean IsEmptyArgs(IReadOnlyCollection<IObject?> cArgs)
		=> cArgs.Count == JMethodDefinitionTests.args.Length && cArgs.All(o => o is null);

	private class JFakeMethodDefinition(ReadOnlySpan<Byte> methodName, params JArgumentMetadata[] metadata)
		: JMethodDefinition(methodName, metadata)
	{
		public new void Invoke(JLocalObject jLocal) => base.Invoke(jLocal);
		public new void Invoke(JLocalObject jLocal, JClassObject jClass) => base.Invoke(jLocal, jClass);
		public new void Invoke(JLocalObject jLocal, IObject?[] args) => base.Invoke(jLocal, args);
		public new void Invoke(JLocalObject jLocal, JClassObject jClass, IObject?[] args)
			=> base.Invoke(jLocal, jClass, args);
		public new void InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass)
			=> base.InvokeNonVirtual(jLocal, jClass);
		public new void InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass, IObject?[] args)
			=> base.InvokeNonVirtual(jLocal, jClass, args);
		public new void StaticInvoke(JClassObject jClass) => base.StaticInvoke(jClass);
		public new void StaticInvoke(JClassObject jClass, IObject?[] args) => base.StaticInvoke(jClass, args);
		public new void InvokeReflected(JMethodObject jMethod, JLocalObject jLocal)
			=> base.InvokeReflected(jMethod, jLocal);
		public new void InvokeReflected(JMethodObject jMethod, JLocalObject jLocal, IObject?[] args)
			=> base.InvokeReflected(jMethod, jLocal, args);
		public new void InvokeNonVirtualReflected(JMethodObject jMethod, JLocalObject jLocal)
			=> base.InvokeNonVirtualReflected(jMethod, jLocal);
		public new void InvokeNonVirtualReflected(JMethodObject jMethod, JLocalObject jLocal, IObject?[] args)
			=> base.InvokeNonVirtualReflected(jMethod, jLocal, args);
		public new void InvokeStaticReflected(JMethodObject jMethod) => base.InvokeStaticReflected(jMethod);
		public new void InvokeStaticReflected(JMethodObject jMethod, IObject?[] args)
			=> base.InvokeStaticReflected(jMethod, args);
	}
}