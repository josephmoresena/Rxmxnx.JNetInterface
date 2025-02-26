namespace Rxmxnx.JNetInterface.Tests;

public partial class JVirtualMachineTests
{
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
			JVirtualMachine jvm = Assert.IsType<JVirtualMachine>(vm);
			proxyVm.Received(1).GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>());
			proxyVm.Received(attached ? 0 : 1).AttachCurrentThread(Arg.Any<ValPtr<JEnvironmentRef>>(),
			                                                       Arg.Any<ReadOnlyValPtr<
				                                                       VirtualMachineArgumentValueWrapper>>());
			proxyEnv.Received(1).GetVersion();
			proxyEnv.Received(13).FindClass(Arg.Any<ReadOnlyValPtr<Byte>>());
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
			proxyEnv.FinalizeProxy(false);
			proxyVm.FinalizeProxy();
		}
	}
}