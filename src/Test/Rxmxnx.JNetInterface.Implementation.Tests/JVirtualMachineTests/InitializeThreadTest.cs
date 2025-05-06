namespace Rxmxnx.JNetInterface.Tests;

public partial class JVirtualMachineTests
{
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
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
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
			Assert.True((env as IEnvironment).NoProxy);
			Assert.False(env.IsDisposable);
			Assert.Equal(IVirtualMachine.MinimalVersion, env.Version);
			Assert.False(env.IsDaemon);
			Assert.True((env as IEnvironment).NoProxy);
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
			Assert.Null((env as IEnvironment).PendingException);
			Assert.Equal(removeAttachedThread, env.IsDisposable);
			Assert.True(env.IsAttached);
			Assert.Equal(IVirtualMachine.MinimalVersion, env.Version);
			Assert.Equal(removeAttachedThread && daemon, thread.Daemon);
			Assert.True((env as IEnvironment).NoProxy);
			Assert.Equal(removeAttachedThread ? threadName : CString.Zero, env.Name);
			proxyEnv.VirtualMachine.Received(!daemon ? 1 : 0).AttachCurrentThread(
				Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<ReadOnlyValPtr<VirtualMachineArgumentValueWrapper>>());
			proxyEnv.VirtualMachine.Received(daemon ? 1 : 0).AttachCurrentThreadAsDaemon(
				Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<ReadOnlyValPtr<VirtualMachineArgumentValueWrapper>>());

			proxyEnv.VirtualMachine.GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>()).Returns(JResult.Ok);
			Assert.Equal(removeAttachedThread && daemon, env.IsDaemon);
			Assert.True(thread.Attached);

			proxyEnv.EnsureLocalCapacity(SByte.MaxValue).Returns(JResult.Ok);
			proxyEnv.EnsureLocalCapacity(Byte.MaxValue).Returns(JResult.MemoryError);

			thread.LocalCapacity = SByte.MaxValue;
			proxyEnv.Received(1).EnsureLocalCapacity(SByte.MaxValue);
			Assert.Equal(SByte.MaxValue, thread.LocalCapacity);

			JniException ex = Assert.Throws<JniException>(() => thread.LocalCapacity = Byte.MaxValue);
			proxyEnv.Received(1).EnsureLocalCapacity(Byte.MaxValue);
			Assert.Equal(JResult.MemoryError, ex.Result);
			Assert.Equal(SByte.MaxValue, thread.LocalCapacity);
		}
		finally
		{
			if (useThreadGroup)
				proxyEnv.Received(1).DeleteGlobalRef(globalRef);
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.Equal(!removeAttachedThread, env?.IsAttached);
			Assert.Equal(!removeAttachedThread, (env as IThread)?.Attached);
			if (env is not null && removeAttachedThread)
				Assert.Throws<RunningStateException>(() => env.ClassObject.GetClassName(out _));
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
}