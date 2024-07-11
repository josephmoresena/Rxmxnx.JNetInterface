namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public abstract class InvokeInterfaceProxy
{
	public JGlobalRef ClassGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef ThrowableGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef StackTraceElementGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef VoidGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef BooleanGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef ByteGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef CharGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef DoubleGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef FloatGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef IntGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef LongGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
	public JGlobalRef ShortGlobalRef { get; } = ReferenceHelper.Fixture.Create<JGlobalRef>();
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
	public ProxyFactory Factory { get; private set; } = default!;

	~InvokeInterfaceProxy() { this.Factory?.FinalizeProxy(this.Reference); }

	public abstract JResult DestroyVirtualMachine();
	public abstract JResult AttachCurrentThread(ValPtr<JEnvironmentRef> envRef,
		ReadOnlyValPtr<VirtualMachineArgumentValueWrapper> arg);
	public abstract JResult DetachCurrentThread();
	public abstract JResult GetEnv(ValPtr<JEnvironmentRef> envRef, Int32 version);
	public abstract JResult AttachCurrentThreadAsDaemon(ValPtr<JEnvironmentRef> envRef,
		ReadOnlyValPtr<VirtualMachineArgumentValueWrapper> arg);

	public static InvokeInterfaceProxy CreateProxy(ProxyFactory factory)
	{
		InvokeInterfaceProxy proxy = Substitute.For<InvokeInterfaceProxy>();
		proxy.Factory = factory;
		proxy.Reference = factory.InitializeProxy(proxy);
		return proxy;
	}
}