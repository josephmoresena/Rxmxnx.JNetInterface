namespace Rxmxnx.JNetInterface.Tests.Native;

[ExcludeFromCodeCoverage]
public sealed class JWeakTests : GlobalObjectTestsBase
{
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
		JWeakRef weakRef0 = GlobalObjectTestsBase.Fixture.Create<JWeakRef>();
		JWeakRef weakRef1 = GlobalObjectTestsBase.Fixture.Create<JWeakRef>();

		vm.InitializeThread(Arg.Any<CString?>()).Returns(thread);
		env.ReferenceFeature.Unload(Arg.Any<JWeak>()).Returns(true);
		env.IsValidationAvoidable(Arg.Any<JGlobalBase>()).Returns(true);

		DateTime currentDate = DateTime.Now;
		using JClassObject jClassClass = new(env);
		using JWeak jWeak0 = new(jClassClass, weakRef0);
		JWeak jWeak1 = new(jWeak0, weakRef1);
		DateTime latestDate = DateTime.Now;

		env.ReferenceFeature.Create<JWeak>(jClassClass).Returns(jWeak1);
		env.GetReferenceType(jWeak1).Returns(JReferenceType.WeakGlobalRefType);

		Assert.Equal(weakRef0, jWeak0.Reference);
		Assert.Equal(weakRef1, jWeak1.Reference);

		Assert.Equal(isProxy, jWeak0.IsProxy);
		Assert.Equal(isProxy, jWeak1.IsProxy);

		Assert.InRange(jWeak0.LastValidation, currentDate, latestDate);
		Assert.InRange(jWeak1.LastValidation, currentDate, latestDate);

		env.ClearReceivedCalls();

		Assert.Equal(jWeak1, jClassClass.Weak);
		env.ReferenceFeature.Received(1).Create<JWeak>(jClassClass);

		Assert.Equal(weakRef1.Value, jClassClass.Reference.Value);
		env.Received(1).IsValidationAvoidable(jWeak1);
		env.Received(0).GetReferenceType(jWeak1);

		env.IsValidationAvoidable(Arg.Any<JGlobalBase>()).Returns(false);

		Assert.Equal(weakRef1.Value, jClassClass.Reference.Value);

		env.ReferenceFeature.Received(1).Create<JWeak>(jClassClass);
		env.Received(2).IsValidationAvoidable(jWeak1);
		env.Received(1).GetReferenceType(jWeak1);

		env.IsValidationAvoidable(Arg.Any<JGlobalBase>()).Returns(true);

		Assert.False(jWeak0.HasObjects);
		Assert.False(jWeak1.HasObjects);

		Assert.Equal(typeof(ClassObjectMetadata), jWeak0.ObjectMetadata.GetType());
		Assert.True(jWeak0.ObjectSignature.AsSpan().SequenceEqual(jClassClass.ObjectSignature));

		vm.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();
		jWeak1.Dispose();

		vm.Received(1).InitializeThread(Arg.Any<CString?>());
		env.ReferenceFeature.Received(1).Unload(jWeak1);
		Assert.Equal(default, jWeak1.Reference);

		vm.ClearReceivedCalls();
		env.ReferenceFeature.ClearReceivedCalls();
		jWeak1.Dispose();
		vm.Received(1).InitializeThread(Arg.Any<CString?>());
		env.ReferenceFeature.Received(0).Unload(jWeak1);

		env.ClearReceivedCalls();

		Assert.Equal(default, jClassClass.Reference.Value);
		env.Received(0).IsValidationAvoidable(jWeak1);
		env.Received(0).GetReferenceType(jWeak1);

		Assert.Equal($"{jWeak0.Reference} {jWeak0.ObjectMetadata}", jWeak0.ToString());
		Assert.Equal($"{jWeak1.Reference} {jWeak1.ObjectMetadata}", jWeak1.ToString());
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
		JWeakRef weakRef = GlobalObjectTestsBase.Fixture.Create<JWeakRef>();
		String threadName = $"CheckAssignability-{Environment.CurrentManagedThreadId}";
		using JClassObject jClassClass = new(env);
		using JWeak jWeak = new(jClassClass, weakRef);

		vm.InitializeThread(Arg.Any<CString?>()).Returns(thread);
		jClassClass.SetAssignableTo<JClassObject>(true);
		jClassClass.SetAssignableTo<JStringObject>(false);

		env.ReferenceFeature.Create<JWeak>(jClassClass).Returns(jWeak);
		Assert.Equal(jWeak, jClassClass.Weak);

		Assert.True(jWeak.InstanceOf<JClassObject>());
		Assert.True(jWeak.InstanceOf<JLocalObject>());
		Assert.True(jWeak.InstanceOf<JSerializableObject>());
		Assert.True(jWeak.InstanceOf<JAnnotatedElementObject>());
		Assert.True(jWeak.InstanceOf<JGenericDeclarationObject>());
		Assert.True(jWeak.InstanceOf<JTypeObject>());
		Assert.False(jWeak.InstanceOf<JStringObject>());

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
		JWeakRef weakRef = !isDefault ? GlobalObjectTestsBase.Fixture.Create<JWeakRef>() : default;
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
	internal async Task FinalizerTestAsync(Boolean unload)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JStringLocalRef stringRef = GlobalObjectTestsBase.Fixture.Create<JStringLocalRef>();
		JWeakRef weakRef = GlobalObjectTestsBase.Fixture.Create<JWeakRef>();
		ConcurrentBag<JWeakRef> references = GlobalObjectTestsBase.ConfigureFinalizer<JWeakRef>(vm, thread, unload);

		using JClassObject jClassClass = new(env);
		using JClassObject jStringClass = new(jClassClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jString = new(jStringClass, stringRef, GlobalObjectTestsBase.Fixture.Create<String>());
		for (Int32 i = 0; i < 100; i++)
		{
			GC.Collect();
			_ = new JWeak(jString, weakRef);
			GC.Collect();
		}
		await GlobalObjectTestsBase.FinalizerCheckTestAsync(thread, references, weakRef);
	}
}