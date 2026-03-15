namespace Rxmxnx.JNetInterface.Tests;

public partial class JVirtualMachineTests
{
	[Theory]
	[InlineData((Int32)JRuntimeVersion.SEd1)]
	[InlineData((Int32)JRuntimeVersion.SEd2)]
	[InlineData((Int32)JRuntimeVersion.SEd3)]
	[InlineData((Int32)JRuntimeVersion.SEd4)]
	[InlineData((Int32)JRuntimeVersion.J5)]
	internal void GetVirtualMachineVersionErrorTest(Int32 jniVersion)
	{
		Boolean vmInitialized = jniVersion >= (Int32)JRuntimeVersion.SEd2;
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			proxyEnv.GetVersion().Returns(jniVersion);

			IVirtualMachine? vm = default;
			if (vmInitialized)
			{
				vm = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference);
			}
			else
			{
				Exception ex =
					Assert.Throws<JavaVersionException>(() => JVirtualMachine.GetVirtualMachine(
						                                    proxyEnv.VirtualMachine.Reference));
				Assert.Equal(
					IMessageResource.GetInstance()
					                .InvalidCallVersion(jniVersion, "FindClass", (Int32)JRuntimeVersion.SEd2),
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
			proxyEnv.Received(0).GetObjectRefType(proxyEnv.VirtualMachine.VoidPGlobalRef.Value);
			proxyEnv.GetObjectRefType(Arg.Any<JObjectLocalRef>()).Returns(JReferenceType.WeakGlobalRefType);
			proxyEnv.NewWeakGlobalRef(proxyEnv.VirtualMachine.VoidPGlobalRef.Value).Returns(weakRef);
			Assert.Equal(weakRef, env.ClassFeature.VoidPrimitive.Weak.Reference);

			if (jniVersion < (Int32)JRuntimeVersion.SEd2)
				Assert.Throws<InvalidOperationException>(() => env.ClassFeature.VoidPrimitive.Weak.IsValid(env));
			else // JNI < 1.6 support 
				_ = env.ClassFeature.VoidPrimitive.Weak.IsValid(env);

			proxyEnv.Received(0).GetObjectRefType(proxyEnv.VirtualMachine.VoidPGlobalRef.Value);
			proxyEnv.Received(0).GetObjectRefType(weakRef.Value);
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.Equal(vmInitialized, JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
}