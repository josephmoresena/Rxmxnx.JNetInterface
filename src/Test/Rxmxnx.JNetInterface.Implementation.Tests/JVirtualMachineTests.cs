namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class JVirtualMachineTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal unsafe void GetVirtualMachineTest(Boolean attached)
	{
		InvokeInterfaceProxy proxyVm = InvokeInterfaceProxy.CreateProxy();
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
			Assert.IsType<JVirtualMachine>(vm);
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

			Assert.Equal(vm, JVirtualMachine.GetVirtualMachine(proxyVm.Reference));

			proxyEnv.Received(0).GetVersion();
			proxyEnv.Received(0).FindClass(Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(0).GetStaticFieldId(Arg.Any<JClassLocalRef>(), Arg.Any<ReadOnlyValPtr<Byte>>(),
			                                      Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(0).GetStaticObjectField(Arg.Any<JClassLocalRef>(), Arg.Any<JFieldId>());
		}
		finally
		{
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine!.Reference));
		}
	}

	[Fact]
	internal void GetVirtualMachineErrorTest()
	{
		JResult result = (JResult)Random.Shared.Next(-6, -1);
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy(false);

		try
		{
			proxyEnv.VirtualMachine!.GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>()).Returns(result);
			proxyEnv.VirtualMachine!.AttachCurrentThread(Arg.Any<ValPtr<JEnvironmentRef>>(),
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
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine!.Reference));
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
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			proxyEnv.GetVersion().Returns(jniVersion);

			IVirtualMachine? vm = default;
			if (jniVersion >= 0x00010002)
			{
				vm = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine!.Reference);
			}
			else
			{
				Exception ex = Assert.Throws<InvalidOperationException>(
					() => JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine!.Reference));
				Assert.Equal(
					$"Current JNI version (0x{jniVersion:x8}) is invalid to call FindClass. JNI required: 0x{0x00010002:x8}",
					ex.Message);
			}

			proxyEnv.VirtualMachine!.Received(1).GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>());
			proxyEnv.VirtualMachine!.Received(0).AttachCurrentThread(Arg.Any<ValPtr<JEnvironmentRef>>(),
			                                                         Arg.Any<ReadOnlyValPtr<
				                                                         VirtualMachineArgumentValueWrapper>>());
			if (vm is null) return;
			IEnvironment env = vm.GetEnvironment()!;
			JWeakRef weakRef = JVirtualMachineTests.fixture.Create<JWeakRef>();

			proxyEnv.GetObjectRefType(Arg.Any<JObjectLocalRef>()).Returns(JReferenceType.GlobalRefType);
			Assert.True(env.ClassFeature.VoidPrimitive.Global.IsValid(env));
			proxyEnv.Received(0).GetObjectRefType(proxyEnv.VirtualMachine!.VoidGlobalRef.Value);
			proxyEnv.GetObjectRefType(Arg.Any<JObjectLocalRef>()).Returns(JReferenceType.WeakGlobalRefType);
			proxyEnv.NewWeakGlobalRef(proxyEnv.VirtualMachine.VoidGlobalRef.Value).Returns(weakRef);
			Assert.Equal(weakRef, env.ClassFeature.VoidPrimitive.Weak.Reference);
			Assert.Throws<InvalidOperationException>(() => env.ClassFeature.VoidPrimitive.Weak.IsValid(env));
			proxyEnv.Received(0).GetObjectRefType(proxyEnv.VirtualMachine!.VoidGlobalRef.Value);

			proxyEnv.Received(0).GetObjectRefType(weakRef.Value);
		}
		finally
		{
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine!.Reference));
		}
	}

	[Fact]
	internal void RemoveVirtualMachineFalseTest() => Assert.False(JVirtualMachine.RemoveVirtualMachine(default));
}