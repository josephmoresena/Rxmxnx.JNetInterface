namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class InterfacesTests
{
	[Fact]
	internal void EnvironmentTest()
	{
		JNativeInterface jni = default;
		IntPtr jniPtr = NativeUtilities.GetUnsafeIntPtr(in jni);
		JEnvironmentValue envValue = jniPtr.Transform<IntPtr, JEnvironmentValue>();
		IntPtr valPtr = NativeUtilities.GetUnsafeIntPtr(in envValue);
		JEnvironmentRef envRef = valPtr.Transform<IntPtr, JEnvironmentRef>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		env.Reference.Returns(envRef);

		Assert.Equal(envRef, (env as IWrapper<JEnvironmentRef>).Value);
	}
	[Fact]
	internal void VirtualMachineTest()
	{
		JInvokeInterface jii = default;
		IntPtr jiiPtr = NativeUtilities.GetUnsafeIntPtr(in jii);
		JVirtualMachineValue vmValue = jiiPtr.Transform<IntPtr, JVirtualMachineValue>();
		IntPtr valPtr = NativeUtilities.GetUnsafeIntPtr(in vmValue);
		JVirtualMachineRef vmRef = valPtr.Transform<IntPtr, JVirtualMachineRef>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		env.VirtualMachine.Reference.Returns(vmRef);

		Assert.Equal(vmRef, (env.VirtualMachine as IWrapper<JVirtualMachineRef>).Value);

		foreach (ThreadPurpose purpose in Enum.GetValues<ThreadPurpose>())
		{
			String threadName = $"{Enum.GetName(purpose)}-{Environment.CurrentManagedThreadId}";
			using IThread thread = (env.VirtualMachine as IVirtualMachine).CreateThread(purpose);
			env.VirtualMachine.Received(1).InitializeThread(Arg.Is<CString?>(c => c!.ToString() == threadName));
		}
	}
	[Fact]
	internal void CriticalExceptionTest()
	{
		CriticalException ex = CriticalException.Instance;
		Assert.Equal(JResult.Error, ex.Result);
		Assert.Equal(CommonConstants.CriticalExceptionMessage, ex.Message);
	}
}