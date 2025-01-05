namespace Rxmxnx.JNetInterface.Tests;

public partial class JVirtualMachineTests
{
	[Theory]
	[InlineData(0x00010001)]
	[InlineData(0x00010002)]
	[InlineData(0x00010003)]
	[InlineData(0x00010004)]
	[InlineData(0x00010005)]
	internal void GetVirtualMachineVersionErrorTest(Int32 jniVersion)
	{
		Boolean vmInitialized = jniVersion >= 0x00010002;
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
			proxyEnv.Received(0).GetObjectRefType(proxyEnv.VirtualMachine.VoidPGlobalRef.Value);
			proxyEnv.GetObjectRefType(Arg.Any<JObjectLocalRef>()).Returns(JReferenceType.WeakGlobalRefType);
			proxyEnv.NewWeakGlobalRef(proxyEnv.VirtualMachine.VoidPGlobalRef.Value).Returns(weakRef);
			Assert.Equal(weakRef, env.ClassFeature.VoidPrimitive.Weak.Reference);

			if (jniVersion < NativeInterface.RequiredVersion)
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