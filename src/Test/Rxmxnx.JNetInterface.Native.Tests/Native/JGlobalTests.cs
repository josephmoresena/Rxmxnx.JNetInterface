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

		DateTime currentDate = DateTime.Now;
		using JClassObject jClassClass = new(env);
		using JGlobal jGlobal0 = new(jClassClass, globalRef0);
		using JGlobal jGlobal1 = new(vm, jGlobal0.ObjectMetadata, globalRef0);
		JGlobal jGlobal2 = new(jGlobal1, globalRef1);
		DateTime latestDate = DateTime.Now;

		env.ReferenceFeature.Create<JGlobal>(jClassClass).Returns(jGlobal2);
		env.GetReferenceType(jGlobal2).Returns(JReferenceType.GlobalRefType);

		Assert.Equal(globalRef0, jGlobal0.Reference);
		Assert.Equal(globalRef0, jGlobal1.Reference);
		Assert.Equal(globalRef1, jGlobal2.Reference);

		Assert.Equal(isProxy, jGlobal0.IsProxy);
		Assert.Equal(isProxy, jGlobal1.IsProxy);
		Assert.Equal(isProxy, jGlobal2.IsProxy);

		Assert.Equal(jGlobal0, jGlobal0.GetCacheable(env));
		Assert.Equal(jGlobal1, jGlobal1.GetCacheable(env));
		Assert.Equal(jGlobal2, jGlobal2.GetCacheable(env));

		Assert.InRange(jGlobal0.LastValidation, currentDate, latestDate);
		Assert.InRange(jGlobal1.LastValidation, currentDate, latestDate);
		Assert.InRange(jGlobal2.LastValidation, currentDate, latestDate);

		env.ClearReceivedCalls();

		Assert.Equal(jGlobal2, jClassClass.Global);
		env.ReferenceFeature.Received(1).Create<JGlobal>(jClassClass);

		Assert.Equal(globalRef1.Value, jClassClass.Reference.Value);
		env.Received(1).IsValidationAvoidable(jGlobal2);
		env.Received(0).GetReferenceType(jGlobal2);

		env.IsValidationAvoidable(Arg.Any<JGlobalBase>()).Returns(false);

		Assert.Equal(globalRef1.Value, jClassClass.Reference.Value);

		env.ReferenceFeature.Received(1).Create<JGlobal>(jClassClass);
		env.Received(2).IsValidationAvoidable(jGlobal2);
		env.Received(1).GetReferenceType(jGlobal2);

		env.IsValidationAvoidable(Arg.Any<JGlobalBase>()).Returns(true);

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

		env.ClearReceivedCalls();

		Assert.Equal(default, jClassClass.Reference.Value);
		env.Received(0).IsValidationAvoidable(jGlobal2);
		env.Received(0).GetReferenceType(jGlobal2);

		jGlobal0.SetValue(default);
	}

	[Theory]
	[InlineData(null)]
	[InlineData(true)]
	[InlineData(false)]
	internal void InstanceOfTest(Boolean? jniSecure)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JGlobalRef globalRef = JGlobalTests.fixture.Create<JGlobalRef>();
		String threadName = $"CheckAssignability-{Environment.CurrentManagedThreadId}";
		using JClassObject jClassClass = new(env);
		using JGlobal jGlobal = new(jClassClass, globalRef);

		vm.InitializeThread(Arg.Any<CString?>()).Returns(thread);
		jClassClass.SetAssignableTo<JClassObject>(true);
		jClassClass.SetAssignableTo<JStringObject>(false);

		env.ReferenceFeature.Create<JGlobal>(jClassClass).Returns(jGlobal);
		Assert.Equal(jGlobal, jClassClass.Global);

		Assert.True(jGlobal.InstanceOf<JClassObject>());
		Assert.True(jGlobal.InstanceOf<JLocalObject>());
		Assert.True(jGlobal.InstanceOf<JSerializableObject>());
		Assert.True(jGlobal.InstanceOf<JAnnotatedElementObject>());
		Assert.True(jGlobal.InstanceOf<JGenericDeclarationObject>());
		Assert.True(jGlobal.InstanceOf<JTypeObject>());
		Assert.False(jGlobal.InstanceOf<JStringObject>());

		env.ClassFeature.IsInstanceOf(jGlobal, jClassClass).Returns(true);
		jGlobal.InstanceOf(jClassClass);
		env.ClassFeature.Received(1).IsInstanceOf(jGlobal, jClassClass);

		env.JniSecure().Returns(jniSecure.GetValueOrDefault());
		vm.GetEnvironment().Returns(jniSecure.HasValue ? env : default);
		vm.WhenForAnyArgs(v => v.InitializeThread(Arg.Any<CString?>())).Do(c =>
		{
			if (!c[0]!.ToString()!.StartsWith("CheckAssignability-")) return;
			if (!jniSecure.GetValueOrDefault())
				Assert.NotEqual(threadName, c[0].ToString());
			else
				Assert.Equal(threadName, c[0].ToString());
		});
		Assert.False(jGlobal.InstanceOf<JArrayObject<JLocalObject>>());
		vm.Received(1).GetEnvironment();
		env.Received(jniSecure.HasValue ? 1 : 0).JniSecure();
		vm.Received(1).InitializeThread(Arg.Any<CString?>());
		env.ClassFeature.Received(1).IsInstanceOf<JArrayObject<JLocalObject>>(jGlobal);
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void GetSynchronizerTest(Boolean isDefault)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JGlobalRef globalRef = !isDefault ? JGlobalTests.fixture.Create<JGlobalRef>() : default;
		using IDisposable synchronizer = Substitute.For<IDisposable>();
		using JClassObject jClassClass = new(env);
		using JGlobal jGlobal = new(jClassClass, globalRef);

		vm.InitializeThread(Arg.Any<CString?>()).Returns(thread);
		env.ReferenceFeature.GetSynchronizer(Arg.Any<JGlobal>()).Returns(synchronizer);

		if (isDefault)
			Assert.Null(jGlobal.Synchronize());
		else
			Assert.Equal(synchronizer, jGlobal.Synchronize());

		env.ReferenceFeature.Received(!isDefault ? 1 : 0).GetSynchronizer(Arg.Any<JGlobal>());
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
		thread.ReferenceFeature.WhenForAnyArgs(r => r.Unload(Arg.Any<JGlobal>())).Do(c =>
		{
			try
			{
				references.Add((c[0] as JGlobal)!.Reference);
			}
			catch (Exception)
			{
				// ignored
			}
		});

		using JClassObject jClassClass = new(env);
		using JClassObject jStringClass = new(jClassClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jString = new(jStringClass, stringRef, JGlobalTests.fixture.Create<String>());
		for (Int32 i = 0; i < 100; i++)
		{
			_ = new JGlobal(jString, globalRef);
			GC.Collect();
		}
		GC.Collect();
		thread.ReferenceFeature.Received().Unload(Arg.Any<JGlobal>());
		Assert.All(references, g => Assert.Equal(globalRef, g));
	}
}