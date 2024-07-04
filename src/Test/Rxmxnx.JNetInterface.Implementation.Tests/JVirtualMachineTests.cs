namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class JVirtualMachineTests
{
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal unsafe void GetVirtualMachineTest(Boolean attached)
	{
		InvokeInterfaceProxy proxy = InvokeInterfaceProxy.CreateProxy();
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy(proxy);

		proxyEnv.UseDefaultClassRef = false;
		if (attached)
		{
			proxy.When(v => v.GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>()))
			     .Do(c => ((ValPtr<JEnvironmentRef>)c[0]).Reference = proxyEnv.Reference);
		}
		else
		{
			proxy.GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>()).Returns(JResult.DetachedThreadError);
			proxy.When(v => v.AttachCurrentThread(Arg.Any<ValPtr<JEnvironmentRef>>(),
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

		IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(proxy.Reference);
		Assert.IsType<JVirtualMachine>(vm);
		proxy.Received(1).GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>());
		proxy.Received(attached ? 0 : 1).AttachCurrentThread(Arg.Any<ValPtr<JEnvironmentRef>>(),
		                                                     Arg.Any<ReadOnlyValPtr<
			                                                     VirtualMachineArgumentValueWrapper>>());
		proxyEnv.Received(1).GetVersion();
		proxyEnv.Received(12).FindClass(Arg.Any<ReadOnlyValPtr<Byte>>());
		proxyEnv.Received(9).GetStaticFieldId(Arg.Any<JClassLocalRef>(), Arg.Any<ReadOnlyValPtr<Byte>>(),
		                                      Arg.Any<ReadOnlyValPtr<Byte>>());
		proxyEnv.Received(9).GetStaticObjectField(Arg.Any<JClassLocalRef>(), Arg.Any<JFieldId>());
	}
}