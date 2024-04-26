namespace Rxmxnx.JNetInterface.Tests.Native;

[ExcludeFromCodeCoverage]
public sealed class JWeakTests
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
		JWeakRef weakRef0 = JWeakTests.fixture.Create<JWeakRef>();
		JWeakRef weakRef1 = JWeakTests.fixture.Create<JWeakRef>();

		vm.InitializeThread(Arg.Any<CString?>()).Returns(thread);
		env.ReferenceFeature.Unload(Arg.Any<JWeak>()).Returns(true);
		env.IsValidationAvoidable(Arg.Any<JGlobalBase>()).Returns(true);

		DateTime currentDate = DateTime.Now;
		using JClassObject jClassClass = new(env);
		using JWeak jWeak0 = new(jClassClass, weakRef0);
		JWeak jWeak2 = new(jWeak0, weakRef1);
		DateTime latestDate = DateTime.Now;

		env.ReferenceFeature.Create<JWeak>(jClassClass).Returns(jWeak2);
		env.GetReferenceType(jWeak2).Returns(JReferenceType.WeakGlobalRefType);

		Assert.Equal(weakRef0, jWeak0.Reference);
		Assert.Equal(weakRef1, jWeak2.Reference);

		Assert.Equal(isProxy, jWeak0.IsProxy);
		Assert.Equal(isProxy, jWeak2.IsProxy);

		Assert.InRange(jWeak0.LastValidation, currentDate, latestDate);
		Assert.InRange(jWeak2.LastValidation, currentDate, latestDate);

		env.ClearReceivedCalls();

		Assert.Equal(jWeak2, jClassClass.Weak);
		env.ReferenceFeature.Received(1).Create<JWeak>(jClassClass);

		Assert.Equal(weakRef1.Value, jClassClass.Reference.Value);
		env.Received(1).IsValidationAvoidable(jWeak2);
		env.Received(0).GetReferenceType(jWeak2);

		env.IsValidationAvoidable(Arg.Any<JGlobalBase>()).Returns(false);

		Assert.Equal(weakRef1.Value, jClassClass.Reference.Value);

		env.ReferenceFeature.Received(1).Create<JWeak>(jClassClass);
		env.Received(2).IsValidationAvoidable(jWeak2);
		env.Received(1).GetReferenceType(jWeak2);

		env.IsValidationAvoidable(Arg.Any<JGlobalBase>()).Returns(true);

		Assert.False(jWeak0.HasObjects);
		Assert.False(jWeak2.HasObjects);

		Assert.Equal(typeof(ClassObjectMetadata), jWeak0.MetadataType);
		Assert.True(jWeak0.ObjectSignature.AsSpan().SequenceEqual(jClassClass.ObjectSignature));

		vm.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();
		jWeak2.Dispose();

		vm.Received(1).InitializeThread(Arg.Any<CString?>());
		env.ReferenceFeature.Received(1).Unload(jWeak2);
		Assert.Equal(default, jWeak2.Reference);

		vm.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();
		jWeak2.Dispose();
		vm.Received(1).InitializeThread(Arg.Any<CString?>());
		env.ReferenceFeature.Received(0).Unload(jWeak2);

		env.ClearReceivedCalls();

		Assert.Equal(default, jClassClass.Reference.Value);
		env.Received(0).IsValidationAvoidable(jWeak2);
		env.Received(0).GetReferenceType(jWeak2);
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
		JWeakRef weakRef = JWeakTests.fixture.Create<JWeakRef>();
		String threadName = $"CheckAssignability-{Environment.CurrentManagedThreadId}";
		using JClassObject jClassClass = new(env);
		using JWeak jWeak = new(jClassClass, weakRef);

		vm.InitializeThread(Arg.Any<CString?>()).Returns(thread);
		jClassClass.SetAssignableTo<JClassObject>(true);

		env.ReferenceFeature.Create<JWeak>(jClassClass).Returns(jWeak);
		Assert.Equal(jWeak, jClassClass.Weak);

		Assert.True(jWeak.InstanceOf<JClassObject>());
		Assert.True(jWeak.InstanceOf<JLocalObject>());
		Assert.True(jWeak.InstanceOf<JSerializableObject>());
		Assert.True(jWeak.InstanceOf<JAnnotatedElementObject>());
		Assert.True(jWeak.InstanceOf<JGenericDeclarationObject>());
		Assert.True(jWeak.InstanceOf<JTypeObject>());

		env.ClassFeature.IsInstanceOf(jWeak, jClassClass).Returns(true);
		jWeak.InstanceOf(jClassClass);
		env.ClassFeature.Received(1).IsInstanceOf(jWeak, jClassClass);

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
		Assert.False(jWeak.InstanceOf<JArrayObject<JLocalObject>>());
		vm.Received(1).GetEnvironment();
		env.Received(jniSecure.HasValue ? 1 : 0).JniSecure();
		vm.Received(1).InitializeThread(Arg.Any<CString?>());
		env.ClassFeature.Received(1).IsInstanceOf<JArrayObject<JLocalObject>>(jWeak);
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void GetSynchronizerTest(Boolean isDefault)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JWeakRef weakRef = !isDefault ? JWeakTests.fixture.Create<JWeakRef>() : default;
		using IDisposable synchronizer = Substitute.For<IDisposable>();
		using JClassObject jClassClass = new(env);
		using JWeak jWeak = new(jClassClass, weakRef);

		vm.InitializeThread(Arg.Any<CString?>()).Returns(thread);
		env.ReferenceFeature.GetSynchronizer(Arg.Any<JWeak>()).Returns(synchronizer);

		if (isDefault)
			Assert.Null(jWeak.Synchronize());
		else
			Assert.Equal(synchronizer, jWeak.Synchronize());

		env.ReferenceFeature.Received(!isDefault ? 1 : 0).GetSynchronizer(Arg.Any<JWeak>());
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void FinalizerTest(Boolean unload)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JStringLocalRef stringRef = JWeakTests.fixture.Create<JStringLocalRef>();
		JWeakRef weakRef = JWeakTests.fixture.Create<JWeakRef>();
		ConcurrentBag<JWeakRef> references = [];

		vm.InitializeThread(Arg.Any<CString?>()).Returns(thread);
		thread.ReferenceFeature.Unload(Arg.Any<JWeak>()).Returns(unload);
		thread.ReferenceFeature.WhenForAnyArgs(r => r.Unload(Arg.Any<JWeak>()))
		      .Do(c => references.Add((c[0] as JWeak)!.Reference));

		using JClassObject jClassClass = new(env);
		using JClassObject jStringClass = new(jClassClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jString = new(jStringClass, stringRef, JWeakTests.fixture.Create<String>());
		for (Int32 i = 0; i < 100; i++)
		{
			_ = new JWeak(jString, weakRef);
			GC.Collect();
		}
		GC.Collect();
		thread.ReferenceFeature.Received().Unload(Arg.Any<JWeak>());
		Assert.All(references, g => Assert.Equal(weakRef, g));
	}
}