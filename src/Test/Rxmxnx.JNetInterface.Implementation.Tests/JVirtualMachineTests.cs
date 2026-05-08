namespace Rxmxnx.JNetInterface.Tests;

using EnvProxy = Proxies.EnvironmentProxy;

[ExcludeFromCodeCoverage]
public sealed partial class JVirtualMachineTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void AndroidApiLevelTest() => Assert.Null(JVirtualMachine.AndroidApiLevel);

	[Fact]
	internal async Task RegisterTest()
	{
		Assert.False(JVirtualMachine.Register<JClassObject>());
		Assert.True(JVirtualMachine.Register<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JClassObject>>>>>());
		Task registerTask = Task.WhenAll(JVirtualMachineTests.RegisterTestObject(),
		                                 JVirtualMachineTests.RegisterTestObject(),
		                                 JVirtualMachineTests.RegisterTestObject(),
		                                 JVirtualMachineTests.RegisterTestObject(),
		                                 JVirtualMachineTests.RegisterTestObject(),
		                                 JVirtualMachineTests.RegisterTestObject(),
		                                 JVirtualMachineTests.RegisterTestObject(),
		                                 JVirtualMachineTests.RegisterTestObject());
		await Task.WhenAny(registerTask, Task.Delay(500)); // Set timeout
	}
	[Fact]
	internal void GetVirtualMachineErrorTest()
	{
		JResult result = (JResult)Random.Shared.Next(-6, -1);
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy(false);

		try
		{
			proxyEnv.VirtualMachine.GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>()).Returns(result);
			proxyEnv.VirtualMachine.AttachCurrentThread(Arg.Any<ValPtr<JEnvironmentRef>>(),
			                                            Arg.Any<ReadOnlyValPtr<VirtualMachineArgumentValue>>())
			        .Returns(result);
			JniException ex =
				Assert.Throws<JniException>(() => JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference));
			Assert.Equal(result, ex.Result);
			Assert.Equal(Enum.GetName(result), ex.Message);

			proxyEnv.VirtualMachine.Received(1).GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>());
			proxyEnv.VirtualMachine.Received(1).AttachCurrentThread(Arg.Any<ValPtr<JEnvironmentRef>>(),
			                                                        Arg.Any<ReadOnlyValPtr<
				                                                        VirtualMachineArgumentValue>>());
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.False(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Fact]
	internal void RemoveVirtualMachineFalseTest() => Assert.False(JVirtualMachine.RemoveVirtualMachine(default));
	[Fact]
	internal void FatalErrorTest()
	{
		String fatalErrorMessage = JVirtualMachineTests.fixture.Create<String>();
		CString fatalErrorUtf8Message = (CString)fatalErrorMessage;
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			proxyEnv.UseVirtualMachineRef = false;
			proxyEnv.When(e => e.GetVirtualMachine(Arg.Any<ValPtr<JVirtualMachineRef>>()))
			        .Do(c => ((ValPtr<JVirtualMachineRef>)c[0]).Reference = proxyEnv.VirtualMachine.Reference);
			proxyEnv.GetVirtualMachine(Arg.Any<ValPtr<JVirtualMachineRef>>()).Returns(JResult.Ok);

#pragma warning disable CA1859
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
#pragma warning restore CA1859
			proxyEnv.Received(1).GetVirtualMachine(Arg.Any<ValPtr<JVirtualMachineRef>>());
			IVirtualMachine vm = env.VirtualMachine;

			proxyEnv.When(e => e.FatalError(Arg.Any<ReadOnlyValPtr<Byte>>())).Do(c =>
			{
				CString cstr = ((ReadOnlyValPtr<Byte>)c[0]).Pointer.GetUnsafeCString(fatalErrorUtf8Message.Length);
				Assert.Equal(fatalErrorUtf8Message, cstr);
			});

			vm.FatalError(fatalErrorMessage);
			proxyEnv.Received(1).FatalError(Arg.Any<ReadOnlyValPtr<Byte>>());

			proxyEnv.ClearReceivedCalls();

			vm.FatalError(fatalErrorUtf8Message);
			proxyEnv.Received(1).FatalError(Arg.Any<ReadOnlyValPtr<Byte>>());
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Boolean removeResult = JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference);
			if (Environment.Is64BitProcess)
				Assert.True(removeResult);
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	internal void InvocationTest(Boolean forceLocal)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JLocalObject? jLocal = default;
		JGlobal? jGlobal = default;
		JWeak? jWeak = default;
		try
		{
			using IInvokedVirtualMachine invoked = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference,
				proxyEnv.Reference, out IEnvironment env);
			Assert.Equal(proxyEnv.VirtualMachine.Reference, invoked.Reference);
			Assert.Equal(proxyEnv.Reference, env.Reference);
			Assert.True((invoked as JVirtualMachine)!.IsAlive);
			Assert.True((env as JEnvironment)!.IsAttached);

			JGlobalRef globalRef = JVirtualMachineTests.fixture.Create<JGlobalRef>();
			JWeakRef weakRef = JVirtualMachineTests.fixture.Create<JWeakRef>();
			proxyEnv.GetObjectRefType(globalRef.Value).Returns(JReferenceType.GlobalRefType);
			proxyEnv.GetObjectRefType(weakRef.Value).Returns(JReferenceType.WeakGlobalRefType);
			proxyEnv.NewWeakGlobalRef(proxyEnv.VoidObjectLocalRef.Value).Returns(weakRef);
			proxyEnv.NewWeakGlobalRef(globalRef.Value).Returns(weakRef);
			proxyEnv.NewGlobalRef(proxyEnv.VoidObjectLocalRef.Value).Returns(globalRef);
			proxyEnv.NewGlobalRef(weakRef.Value).Returns(globalRef);
			proxyEnv.NewObject(proxyEnv.VoidObjectLocalRef, Arg.Any<JMethodId>(), ReadOnlyValPtr<JValue>.Zero)
			        .Returns(JVirtualMachineTests.fixture.Create<JObjectLocalRef>());

			Assert.True((invoked as JVirtualMachine)!.IsAlive);

			jLocal = env.ClassFeature.VoidObject;
			jWeak = jLocal.Weak;
			jGlobal = jLocal.Global;
		}
		finally
		{
			Assert.Equal(default, (jLocal?.LocalReference).GetValueOrDefault());
			Assert.NotEqual(default, (jWeak?.Reference).GetValueOrDefault());
			Assert.NotEqual(default, (jGlobal?.Reference).GetValueOrDefault());

			DateTime? lastGlobalCheck = jGlobal?.LastValidation;
			DateTime? lastWeakCheck = jWeak?.LastValidation;
			try
			{
				Assert.False(JObject.IsNullOrDefault(jLocal));
				Assert.Equal(lastGlobalCheck, jGlobal?.LastValidation);
			}
			catch (InvalidOperationException)
			{
				// If not avoidable global reference check.
				Assert.NotEqual(lastGlobalCheck, jGlobal?.LastValidation);
			}
			finally
			{
				Assert.Equal(lastWeakCheck, jWeak?.LastValidation);
			}
			Assert.False(JObject.IsNullOrDefault(jWeak));
			Assert.False(JObject.IsNullOrDefault(jGlobal));

			if (forceLocal) jLocal?.SetValue(proxyEnv.VoidObjectLocalRef);

			jLocal?.Dispose();
			if (jWeak is IDisposable dw)
				dw.Dispose();
			if (jGlobal is IDisposable dg)
				dg.Dispose();

			Assert.True(JObject.IsNullOrDefault(jLocal));
			Assert.True(JObject.IsNullOrDefault(jWeak));
			Assert.True(JObject.IsNullOrDefault(jGlobal));

			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Assert.False(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Fact]
	internal void NestedInvocationTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			using IInvokedVirtualMachine invoked = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference,
				proxyEnv.Reference, out IEnvironment env);
			Assert.Equal(proxyEnv.VirtualMachine.Reference, invoked.Reference);
			Assert.Equal(proxyEnv.Reference, env.Reference);
			Assert.True((invoked as JVirtualMachine)!.IsAlive);
			Assert.True((env as JEnvironment)!.IsAttached);

			using (IInvokedVirtualMachine invoked2 =
			       JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference,
			                                         out IEnvironment env2))
			{
				Assert.Equal(proxyEnv.VirtualMachine.Reference, invoked.Reference);
				Assert.Equal(proxyEnv.Reference, env.Reference);
				Assert.Same(invoked, invoked2);
				Assert.Same(env, env2);

				Assert.True((invoked2 as JVirtualMachine)!.IsAlive);
				Assert.True((env2 as JEnvironment)!.IsAttached);
			}

			Assert.False((invoked as JVirtualMachine)!.IsAlive);
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Assert.False(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Fact]
	internal void NoInvocationTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference);
			using (IInvokedVirtualMachine invoked = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference,
				       proxyEnv.Reference, out IEnvironment env))
			{
				Assert.NotSame(vm, invoked);
				Assert.Same(vm.GetEnvironment(), env);

				Assert.Equal(proxyEnv.VirtualMachine.Reference, invoked.Reference);
				Assert.Equal(proxyEnv.Reference, env.Reference);
				Assert.True((invoked as JVirtualMachine)!.IsAlive);
				Assert.True((env as JEnvironment)!.IsAttached);
			}

			Assert.True((vm as JVirtualMachine)!.IsAlive);
			Assert.True((vm.GetEnvironment() as JEnvironment)!.IsAttached);
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Boolean removeResult = JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference);
			if (Environment.Is64BitProcess)
				Assert.True(removeResult);
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Fact]
	internal void ProxyNoProxyTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			JEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			EnvProxy proxyProxy = Substitute.For<EnvProxy>();

			using JClassObject jClassClassProxy =
				EnvProxy.CreateClassObject(proxyProxy, JVirtualMachineTests.fixture.Create<JClassLocalRef>());

			Assert.Throws<ProxyObjectException>(() => EnvProxy.CreateClassObject<JStringObject>(env.ClassObject));
			Assert.Throws<ProxyObjectException>(() => (env as IEnvironment).IsSameObject(
				                                    jClassClassProxy, env.ClassObject));
			Assert.Throws<ProxyObjectException>(() => (env as IEnvironment).ClassFeature.GetObjectClass(
				                                    ILocalObject.CreateMetadata(jClassClassProxy)));
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Boolean removeResult = JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference);
			if (Environment.Is64BitProcess)
				Assert.True(removeResult);
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Fact]
	internal void InvalidThreadTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			JEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			Thread thread = new(() =>
			{
				Assert.Throws<DifferentThreadException>(() => env.ClassObject.GetClassName(out _));
			});
			thread.Start();
			thread.Join();
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Boolean removeResult = JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference);
			if (Environment.Is64BitProcess)
				Assert.True(removeResult);
			proxyEnv.FinalizeProxy(true);
		}
	}
}