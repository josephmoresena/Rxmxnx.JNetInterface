namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class ProxyFactory(UInt32 maxThreads)
{
	private readonly MemoryHelper<JVirtualMachineRef> _invokeMemory = new(ReferenceHelper.InvokeInterface.AsMemory().Pin(), (Int32)(maxThreads / 2 + 1));
	private readonly MemoryHelper<JEnvironmentRef> _nativeMemory = new(ReferenceHelper.NativeInterface.AsMemory().Pin(), (Int32)maxThreads);

	internal JVirtualMachineRef InitializeProxy(InvokeInterfaceProxy proxy)
	{
		JVirtualMachineRef result = this._invokeMemory.Get();
		ReferenceHelper.InvokeProxies[result] = proxy;
		return result;
	}
	internal void FinalizeProxy(JVirtualMachineRef vmRef)
	{
		if (this._invokeMemory.Free(vmRef))
			ReferenceHelper.InvokeProxies.TryRemove(vmRef, out _);
	}
	internal JEnvironmentRef InitializeProxy(NativeInterfaceProxy proxy)
	{
		JEnvironmentRef result = this._nativeMemory.Get();
		ReferenceHelper.NativeProxies[result] = proxy;
		return result;
	}
	internal void FinalizeProxy(JEnvironmentRef envRef)
	{
		if (this._nativeMemory.Free(envRef))
			ReferenceHelper.NativeProxies.TryRemove(envRef, out _);
	}
}