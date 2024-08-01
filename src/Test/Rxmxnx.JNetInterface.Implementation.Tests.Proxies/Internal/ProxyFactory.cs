namespace Rxmxnx.JNetInterface.Tests.Internal;

[ExcludeFromCodeCoverage]
internal sealed class ProxyFactory
{
	public static readonly ProxyFactory Instance = new(UInt16.MaxValue, UInt16.MaxValue);

	internal readonly MemoryHelper<JVirtualMachineRef> InvokeMemory;
	internal readonly MemoryHelper<JEnvironmentRef> NativeMemory;

	private unsafe ProxyFactory(UInt32 maxVm, UInt32 maxThreads)
	{
		this.InvokeMemory = new(ReferenceHelper.InvokeInterface.AsMemory().Pin(), sizeof(InternalInvokeInterface),
		                        (Int32)maxVm);
		this.NativeMemory = new(ReferenceHelper.NativeInterface.AsMemory().Pin(), sizeof(InternalNativeInterface),
		                        (Int32)maxThreads);
	}
}