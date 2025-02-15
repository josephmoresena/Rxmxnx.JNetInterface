namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public abstract class InvokeInterfaceProxy
{
	internal static InvokeInterfaceProxy Detached = InvokeInterfaceProxy.CreateDetached();

	public JGlobalRef ClassGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef ThrowableGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef StackTraceElementGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef NumberGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef VoidGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef BooleanGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef ByteGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef CharacterGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef DoubleGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef FloatGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef IntegerGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef LongGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef ShortGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef VoidPGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef BooleanPGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef BytePGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef CharPGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef DoublePGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef FloatPGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef IntPGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef LongPGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef ShortPGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JMethodId ClassGetNameMethodId { get; } = ReferenceHelper.Fixture.Create<JMethodId>();
	public JMethodId ClassIsPrimitiveMethodId { get; } = ReferenceHelper.Fixture.Create<JMethodId>();
	public JMethodId ClassGetModifiersMethodId { get; } = ReferenceHelper.Fixture.Create<JMethodId>();
	public JMethodId ClassGetInterfacesMethodId { get; } = ReferenceHelper.Fixture.Create<JMethodId>();
	public JMethodId ThrowableGetMessageMethodId { get; } = ReferenceHelper.Fixture.Create<JMethodId>();
	public JMethodId ThrowableGetStackTraceMethodId { get; } = ReferenceHelper.Fixture.Create<JMethodId>();
	public JMethodId StackTraceElementGetClassNameMethodId { get; } = ReferenceHelper.Fixture.Create<JMethodId>();
	public JMethodId StackTraceElementGetLineNumberMethodId { get; } = ReferenceHelper.Fixture.Create<JMethodId>();
	public JMethodId StackTraceElementGetFileNameMethodId { get; } = ReferenceHelper.Fixture.Create<JMethodId>();
	public JMethodId StackTraceElementGetMethodNameMethodId { get; } = ReferenceHelper.Fixture.Create<JMethodId>();
	public JMethodId StackTraceElementIsNativeMethodMethodId { get; } = ReferenceHelper.Fixture.Create<JMethodId>();

	public JVirtualMachineRef Reference { get; private set; }
	public Int32? AllowedThread { get; set; } = Environment.CurrentManagedThreadId;

	public abstract JResult DestroyVirtualMachine();
	public abstract JResult AttachCurrentThread(ValPtr<JEnvironmentRef> envRef,
		ReadOnlyValPtr<VirtualMachineArgumentValueWrapper> arg);
	public abstract JResult DetachCurrentThread();
	public abstract JResult GetEnv(ValPtr<JEnvironmentRef> envRef, Int32 version);
	public abstract JResult AttachCurrentThreadAsDaemon(ValPtr<JEnvironmentRef> envRef,
		ReadOnlyValPtr<VirtualMachineArgumentValueWrapper> arg);

	public void FinalizeProxy() => ReferenceHelper.FinalizeProxy(this);

	public static InvokeInterfaceProxy CreateProxy()
	{
		InvokeInterfaceProxy proxy = Substitute.For<InvokeInterfaceProxy>();
		proxy.Reference = ProxyFactory.Instance.InvokeMemory.Get();
		return ReferenceHelper.Initialize(proxy);
	}

	private static InvokeInterfaceProxy CreateDetached()
	{
		InvokeInterfaceProxy proxy = Substitute.For<InvokeInterfaceProxy>();
		proxy.GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>()).Returns(JResult.DetachedThreadError);
		proxy.AttachCurrentThread(Arg.Any<ValPtr<JEnvironmentRef>>(),
		                          Arg.Any<ReadOnlyValPtr<VirtualMachineArgumentValueWrapper>>())
		     .Returns(JResult.DetachedThreadError);
		proxy.AttachCurrentThreadAsDaemon(Arg.Any<ValPtr<JEnvironmentRef>>(),
		                                  Arg.Any<ReadOnlyValPtr<VirtualMachineArgumentValueWrapper>>())
		     .Returns(JResult.DetachedThreadError);
		proxy.DetachCurrentThread().Returns(JResult.DetachedThreadError);
		proxy.DestroyVirtualMachine().Returns(JResult.DetachedThreadError);
		return proxy;
	}
}