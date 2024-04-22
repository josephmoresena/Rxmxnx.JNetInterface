namespace Rxmxnx.JNetInterface.Tests.Native;

[ExcludeFromCodeCoverage]
public sealed class JGlobalTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void SimpleTest(Boolean isProxy)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		env.NoProxy.Returns(!isProxy);
		vm.NoProxy.Returns(!isProxy);
		JGlobalRef globalRef0 = JGlobalTests.fixture.Create<JGlobalRef>();
		JGlobalRef globalRef1 = JGlobalTests.fixture.Create<JGlobalRef>();

		vm.InitializeThread(Arg.Any<CString?>()).Returns(thread);
		env.ReferenceFeature.Unload(Arg.Any<JGlobal>()).Returns(true);
		env.IsValidationAvoidable(Arg.Any<JGlobalBase>()).Returns(true);

		using JClassObject jClassClass = new(env);
		using JGlobal jGlobal0 = new(jClassClass, globalRef0);
		using JGlobal jGlobal1 = new(vm, jGlobal0.ObjectMetadata, globalRef0);
		JGlobal jGlobal2 = new(jGlobal1, globalRef1);

		Assert.Equal(globalRef0, jGlobal0.Reference);
		Assert.Equal(globalRef0, jGlobal1.Reference);
		Assert.Equal(globalRef1, jGlobal2.Reference);

		Assert.Equal(isProxy, jGlobal0.IsProxy);
		Assert.Equal(isProxy, jGlobal1.IsProxy);
		Assert.Equal(isProxy, jGlobal2.IsProxy);

		Assert.Equal(jGlobal0, jGlobal0.GetCacheable(env));
		Assert.Equal(jGlobal1, jGlobal1.GetCacheable(env));
		Assert.Equal(jGlobal2, jGlobal2.GetCacheable(env));

		jGlobal0.Synchronize(jGlobal1);
		jGlobal1.Synchronize(jGlobal2);
		jGlobal2.Synchronize(jGlobal0);

		Assert.Equal(jGlobal0, jGlobal0.GetCacheable(env));
		Assert.Equal(jGlobal1, jGlobal1.GetCacheable(env));
		Assert.Equal(jGlobal0, jGlobal2.GetCacheable(env));

		jGlobal1.SetValue(globalRef1);
		Assert.Equal(globalRef1, jGlobal1.Reference);

		Assert.False(jGlobal0.HasObjects);
		Assert.False(jGlobal1.HasObjects);
		Assert.False(jGlobal2.HasObjects);

		Assert.Equal(typeof(ClassObjectMetadata), jGlobal0.MetadataType);
		Assert.True(jGlobal0.ObjectSignature.AsSpan().SequenceEqual(jClassClass.ObjectSignature));

		vm.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();
		jGlobal2.Dispose();

		vm.Received(1).InitializeThread(Arg.Any<CString?>());
		env.ReferenceFeature.Received(1).Unload(jGlobal2);
		Assert.Equal(default, jGlobal2.Reference);

		vm.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();
		jGlobal2.Dispose();
		vm.Received(1).InitializeThread(Arg.Any<CString?>());
		env.ReferenceFeature.Received(0).Unload(jGlobal2);
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void FinalizerTest(Boolean unload)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JStringLocalRef stringRef = JGlobalTests.fixture.Create<JStringLocalRef>();
		JGlobalRef globalRef = JGlobalTests.fixture.Create<JGlobalRef>();
		ConcurrentBag<JGlobalRef> references = [];

		vm.InitializeThread(Arg.Any<CString?>()).Returns(thread);
		thread.ReferenceFeature.Unload(Arg.Any<JGlobal>()).Returns(unload);
		thread.ReferenceFeature.WhenForAnyArgs(r => r.Unload(Arg.Any<JGlobal>()))
		      .Do(c => references.Add((c[0] as JGlobal)!.Reference));

		using JClassObject jClassClass = new(env);
		using JClassObject jStringClass = new(jClassClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jString = new(jStringClass, stringRef, JGlobalTests.fixture.Create<String>());
		for (Int32 i = 0; i < 100; i++)
		{
			_ = new JGlobal(jString, globalRef);
			GC.Collect();
		}
		GC.Collect();
		thread.ReferenceFeature.Received().Unload(Arg.Is<JGlobal>(g => g.Reference == (unload ? default : globalRef)));
		Assert.All(references, g => Assert.Equal(globalRef, g));
	}
}