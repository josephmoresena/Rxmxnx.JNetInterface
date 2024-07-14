namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class JVirtualMachineTests(ProxyFactory<JVirtualMachineTests> factory)
	: IClassFixture<ProxyFactory<JVirtualMachineTests>>, IProxyRequest<JVirtualMachineTests>
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	public static UInt32 MaxThreads => 50;

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal unsafe void GetVirtualMachineTest(Boolean attached)
	{
		InvokeInterfaceProxy proxyVm = InvokeInterfaceProxy.CreateProxy(factory);
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy(proxyVm);
		try
		{
			proxyEnv.UseDefaultClassRef = false;
			if (attached)
			{
				proxyVm.When(v => v.GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>()))
				       .Do(c => ((ValPtr<JEnvironmentRef>)c[0]).Reference = proxyEnv.Reference);
			}
			else
			{
				proxyVm.GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>())
				       .Returns(JResult.DetachedThreadError);
				proxyVm.When(v => v.AttachCurrentThread(Arg.Any<ValPtr<JEnvironmentRef>>(),
				                                        Arg.Any<ReadOnlyValPtr<VirtualMachineArgumentValueWrapper>>()))
				       .Do(c => ((ValPtr<JEnvironmentRef>)c[0]).Reference = proxyEnv.Reference);
			}

			proxyEnv.FindClass(Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(c =>
			{
				ReadOnlyValPtr<Byte> ptr = (ReadOnlyValPtr<Byte>)c[0];
				JClassLocalRef? classRef = proxyEnv.GetMainClassLocalRef((Byte*)ptr.Pointer);
				return classRef!.Value;
			});
			proxyEnv.GetStaticFieldId(Arg.Any<JClassLocalRef>(), Arg.Any<ReadOnlyValPtr<Byte>>(),
			                          Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(c =>
			{
				JClassLocalRef classRef = (JClassLocalRef)c[0];
				Byte* fieldName = (Byte*)((ReadOnlyValPtr<Byte>)c[1]).Pointer;
				JFieldId? fieldId = proxyEnv.GetPrimitiveWrapperClassTypeField(classRef, fieldName);
				return fieldId!.Value;
			});
			proxyEnv.GetStaticObjectField(Arg.Any<JClassLocalRef>(), Arg.Any<JFieldId>()).Returns(c =>
			{
				JClassLocalRef classRef = (JClassLocalRef)c[0];
				JFieldId fieldId = (JFieldId)c[1];
				JObjectLocalRef? localRef = proxyEnv.GetPrimitiveClass(classRef, fieldId)?.Value;
				return localRef!.Value;
			});
			proxyEnv.NewGlobalRef(Arg.Any<JObjectLocalRef>()).Returns(c =>
			{
				JObjectLocalRef localRef = (JObjectLocalRef)c[0];
				JGlobalRef? globalRef = proxyEnv.GetMainClassGlobalRef(JClassLocalRef.FromReference(in localRef));
				return globalRef!.Value;
			});

			IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(proxyVm.Reference);
			JVirtualMachine jvm = Assert.IsType<JVirtualMachine>(vm);
			proxyVm.Received(1).GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>());
			proxyVm.Received(attached ? 0 : 1).AttachCurrentThread(Arg.Any<ValPtr<JEnvironmentRef>>(),
			                                                       Arg.Any<ReadOnlyValPtr<
				                                                       VirtualMachineArgumentValueWrapper>>());
			proxyEnv.Received(1).GetVersion();
			proxyEnv.Received(12).FindClass(Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(9).GetStaticFieldId(Arg.Any<JClassLocalRef>(), Arg.Any<ReadOnlyValPtr<Byte>>(),
			                                      Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(9).GetStaticObjectField(Arg.Any<JClassLocalRef>(), Arg.Any<JFieldId>());

			proxyVm.ClearReceivedCalls();
			proxyEnv.ClearReceivedCalls();

			Assert.True(jvm.IsAlive);
			Assert.False(jvm.IsDisposable);
			Assert.Equal(vm, JVirtualMachine.GetVirtualMachine(proxyVm.Reference));

			proxyEnv.Received(0).GetVersion();
			proxyEnv.Received(0).FindClass(Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(0).GetStaticFieldId(Arg.Any<JClassLocalRef>(), Arg.Any<ReadOnlyValPtr<Byte>>(),
			                                      Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(0).GetStaticObjectField(Arg.Any<JClassLocalRef>(), Arg.Any<JFieldId>());
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
		}
	}

	[Fact]
	internal void GetVirtualMachineErrorTest()
	{
		JResult result = (JResult)Random.Shared.Next(-6, -1);
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy(factory, false);

		try
		{
			proxyEnv.VirtualMachine.GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>()).Returns(result);
			proxyEnv.VirtualMachine.AttachCurrentThread(Arg.Any<ValPtr<JEnvironmentRef>>(),
			                                            Arg.Any<ReadOnlyValPtr<VirtualMachineArgumentValueWrapper>>())
			        .Returns(result);
			JniException ex =
				Assert.Throws<JniException>(() => JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference));
			Assert.Equal(result, ex.Result);
			Assert.Equal(Enum.GetName(result), ex.Message);

			proxyEnv.VirtualMachine.Received(1).GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>());
			proxyEnv.VirtualMachine.Received(1).AttachCurrentThread(Arg.Any<ValPtr<JEnvironmentRef>>(),
			                                                        Arg.Any<ReadOnlyValPtr<
				                                                        VirtualMachineArgumentValueWrapper>>());
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
		}
	}

	[Theory]
	[InlineData(0x00010001)]
	[InlineData(0x00010002)]
	[InlineData(0x00010003)]
	[InlineData(0x00010004)]
	[InlineData(0x00010005)]
	internal void GetVirtualMachineVersionErrorTest(Int32 jniVersion)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy(factory);
		try
		{
			proxyEnv.GetVersion().Returns(jniVersion);

			IVirtualMachine? vm = default;
			if (jniVersion >= 0x00010002)
			{
				vm = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference);
			}
			else
			{
				Exception ex = Assert.Throws<InvalidOperationException>(
					() => JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference));
				Assert.Equal(
					$"Current JNI version (0x{jniVersion:x8}) is invalid to call FindClass. JNI required: 0x{0x00010002:x8}",
					ex.Message);
			}

			proxyEnv.VirtualMachine.Received(1).GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>());
			proxyEnv.VirtualMachine.Received(0).AttachCurrentThread(Arg.Any<ValPtr<JEnvironmentRef>>(),
			                                                        Arg.Any<ReadOnlyValPtr<
				                                                        VirtualMachineArgumentValueWrapper>>());
			if (vm is null) return;
			IEnvironment env = vm.GetEnvironment()!;
			JWeakRef weakRef = JVirtualMachineTests.fixture.Create<JWeakRef>();

			proxyEnv.GetObjectRefType(Arg.Any<JObjectLocalRef>()).Returns(JReferenceType.GlobalRefType);
			Assert.True(env.ClassFeature.VoidPrimitive.Global.IsValid(env));
			proxyEnv.Received(0).GetObjectRefType(proxyEnv.VirtualMachine.VoidGlobalRef.Value);
			proxyEnv.GetObjectRefType(Arg.Any<JObjectLocalRef>()).Returns(JReferenceType.WeakGlobalRefType);
			proxyEnv.NewWeakGlobalRef(proxyEnv.VirtualMachine.VoidGlobalRef.Value).Returns(weakRef);
			Assert.Equal(weakRef, env.ClassFeature.VoidPrimitive.Weak.Reference);
			Assert.Throws<InvalidOperationException>(() => env.ClassFeature.VoidPrimitive.Weak.IsValid(env));
			proxyEnv.Received(0).GetObjectRefType(proxyEnv.VirtualMachine.VoidGlobalRef.Value);

			proxyEnv.Received(0).GetObjectRefType(weakRef.Value);
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
		}
	}

	[Fact]
	internal void RemoveVirtualMachineFalseTest() => Assert.False(JVirtualMachine.RemoveVirtualMachine(default));

	[Fact]
	internal void FatalErrorTest()
	{
		String fatalErrorMessage = JVirtualMachineTests.fixture.Create<String>();
		CString fatalErrorUtf8Message = (CString)fatalErrorMessage;
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy(factory);
		try
		{
			proxyEnv.UseVirtualMachineRef = false;
			proxyEnv.When(e => e.GetVirtualMachine(Arg.Any<ValPtr<JVirtualMachineRef>>()))
			        .Do(c => ((ValPtr<JVirtualMachineRef>)c[0]).Reference = proxyEnv.VirtualMachine.Reference);
			proxyEnv.GetVirtualMachine(Arg.Any<ValPtr<JVirtualMachineRef>>()).Returns(JResult.Ok);

			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
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
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
		}
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	[InlineData(true, true)]
	[InlineData(false, true)]
	[InlineData(true, false, true)]
	[InlineData(false, false, true)]
	[InlineData(true, true, true)]
	[InlineData(false, true, true)]
	internal void InitializeThreadTest(Boolean daemon, Boolean removeAttachedThread = false,
		Boolean useThreadGroup = false)
	{
		JGlobalRef globalRef = useThreadGroup ? JVirtualMachineTests.fixture.Create<JGlobalRef>() : default;
		CString threadName = (CString)JVirtualMachineTests.fixture.Create<String>();
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy(factory);
		JEnvironment? env = default;
		try
		{
			proxyEnv.VirtualMachine.When(v => v.AttachCurrentThread(Arg.Any<ValPtr<JEnvironmentRef>>(),
			                                                        Arg.Any<ReadOnlyValPtr<
				                                                        VirtualMachineArgumentValueWrapper>>())).Do(c =>
			{
				((ValPtr<JEnvironmentRef>)c[0]).Reference = proxyEnv.Reference;
				VirtualMachineArgumentValueWrapper arg = ((ReadOnlyValPtr<VirtualMachineArgumentValueWrapper>)c[1])
					.Reference;
				Assert.Equal(threadName, arg.NamePtr.GetUnsafeCString(threadName.Length));
				Assert.Equal(globalRef, arg.Group);
				Assert.Equal(IVirtualMachine.MinimalVersion, arg.Version);
			});
			proxyEnv.VirtualMachine.When(v => v.AttachCurrentThreadAsDaemon(Arg.Any<ValPtr<JEnvironmentRef>>(),
			                                                                Arg.Any<ReadOnlyValPtr<
				                                                                VirtualMachineArgumentValueWrapper>>()))
			        .Do(c =>
			        {
				        ((ValPtr<JEnvironmentRef>)c[0]).Reference = proxyEnv.Reference;
				        VirtualMachineArgumentValueWrapper arg =
					        ((ReadOnlyValPtr<VirtualMachineArgumentValueWrapper>)c[1])
					        .Reference;
				        Assert.Equal(threadName, arg.NamePtr.GetUnsafeCString(threadName.Length));
				        Assert.Equal(globalRef, arg.Group);
				        Assert.Equal(IVirtualMachine.MinimalVersion, arg.Version);
			        });
			IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference);
			using JGlobal? threadGroup =
				useThreadGroup ? JVirtualMachineTests.CreateThreadGroup(vm, globalRef) : default;
			env = (vm as JVirtualMachine)!.GetEnvironment(proxyEnv.Reference);

			Assert.True(env.IsAttached);
			Assert.True(env.NoProxy);
			Assert.False(env.IsDisposable);
			Assert.Equal(IVirtualMachine.MinimalVersion, env.Version);
			Assert.False(env.IsDaemon);
			Assert.True(env.NoProxy);
			Assert.Equal(CString.Zero, env.Name);
			if (removeAttachedThread)
				JVirtualMachine.RemoveEnvironment(vm.Reference, proxyEnv.Reference);
			proxyEnv.VirtualMachine.GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>())
			        .Returns(JResult.DetachedThreadError);
			proxyEnv.ClearReceivedCalls();
			proxyEnv.VirtualMachine.ClearReceivedCalls();

			using IThread thread = !daemon ?
				vm.InitializeThread(threadName, threadGroup) :
				vm.InitializeDaemon(threadName, threadGroup);
			env = Assert.IsType<JEnvironment.JThread>(thread);

			Assert.Null((env as IEnvironment).LocalCapacity);
			Assert.Equal(removeAttachedThread, env.IsDisposable);
			Assert.True(env.IsAttached);
			Assert.Equal(IVirtualMachine.MinimalVersion, env.Version);
			Assert.Equal(removeAttachedThread && daemon, thread.Daemon);
			Assert.True(env.NoProxy);
			Assert.Equal(removeAttachedThread ? threadName : CString.Zero, env.Name);
			proxyEnv.VirtualMachine.Received(!daemon ? 1 : 0).AttachCurrentThread(
				Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<ReadOnlyValPtr<VirtualMachineArgumentValueWrapper>>());
			proxyEnv.VirtualMachine.Received(daemon ? 1 : 0).AttachCurrentThreadAsDaemon(
				Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<ReadOnlyValPtr<VirtualMachineArgumentValueWrapper>>());

			proxyEnv.VirtualMachine.GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>()).Returns(JResult.Ok);
			Assert.Equal(removeAttachedThread && daemon, env.IsDaemon);
			Assert.True(thread.Attached);
		}
		finally
		{
			if (useThreadGroup)
				proxyEnv.Received(1).DeleteGlobalRef(globalRef);
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.Equal(!removeAttachedThread, env?.IsAttached);
			Assert.Equal(!removeAttachedThread, (env as IThread)?.Attached);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
		}
	}

	[Fact]
	internal async Task RegisterTest()
	{
		Assert.False(JVirtualMachine.Register<JClassObject>());
		Assert.True(JVirtualMachine.Register<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JClassObject>>>>>());
		await Task.WhenAll(JVirtualMachineTests.RegisterTestObject(), JVirtualMachineTests.RegisterTestObject(),
		                   JVirtualMachineTests.RegisterTestObject(), JVirtualMachineTests.RegisterTestObject(),
		                   JVirtualMachineTests.RegisterTestObject(), JVirtualMachineTests.RegisterTestObject(),
		                   JVirtualMachineTests.RegisterTestObject(), JVirtualMachineTests.RegisterTestObject());
	}

	private static JGlobal CreateThreadGroup(IVirtualMachine vm, JGlobalRef globalRef)
	{
		EnvironmentProxy proxy = EnvironmentProxy.CreateEnvironment();
		JClassObject jClassClass = new(proxy);
		CStringSequence classInformation = MetadataHelper.GetClassInformation("java/lang/ThreadGroup"u8, false);
		JClassObject classLoaderClass = new(jClassClass, new TypeInformation(classInformation));
		return new(vm, new(classLoaderClass), globalRef);
	}
	private static Task RegisterTestObject()
		=> Task.Factory.StartNew(() =>
		{
			Boolean register = JVirtualMachine.Register<JTestObject>();
			if (register)
				Assert.NotNull(JTestObject.GetThreadMetadata());
		}, TaskCreationOptions.LongRunning);
}