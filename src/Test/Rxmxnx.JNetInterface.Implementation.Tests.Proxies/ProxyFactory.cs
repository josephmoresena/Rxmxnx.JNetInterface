namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class ProxyFactory(UInt32 maxVm, UInt32 maxThreads) : IDisposable
{
	private readonly MemoryHelper<JVirtualMachineRef> _invokeMemory =
		new(ReferenceHelper.InvokeInterface.AsMemory().Pin(), (Int32)maxVm);
	private readonly MemoryHelper<JEnvironmentRef> _nativeMemory =
		new(ReferenceHelper.NativeInterface.AsMemory().Pin(), (Int32)maxThreads);

	public void Dispose()
	{
		this.Dispose(true);
		GC.SuppressFinalize(this);
	}

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
	private void ReleaseUnmanagedResources()
	{
		this._nativeMemory.Free(e => ReferenceHelper.NativeProxies.TryRemove(e, out _));
		this._invokeMemory.Free(vm => ReferenceHelper.InvokeProxies.TryRemove(vm, out _));
	}
	private void Dispose(Boolean disposing)
	{
		this.ReleaseUnmanagedResources();
		if (!disposing) return;
		this._invokeMemory.Dispose();
		this._nativeMemory.Dispose();
	}
	~ProxyFactory() { this.Dispose(false); }
}

[ExcludeFromCodeCoverage]
public sealed class ProxyFactory<TTest> : IDisposable where TTest : class, IProxyRequest<TTest>
{
	private readonly ProxyFactory _value = new(TTest.MaxVms, TTest.MaxThreads);
	public void Dispose() { this._value.Dispose(); }
	public static implicit operator ProxyFactory(ProxyFactory<TTest> factory) => factory._value;
}