namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
internal sealed class NativeVoidParameterless<TReceiver> where TReceiver : unmanaged, INativePointerType<TReceiver>
{
	private readonly IMutableWrapper<Boolean> _disposed = IMutableReference<Boolean>.Create();

	public IWrapper<Boolean> IsDisposed => this._disposed;

#pragma warning disable CA1822
	public void VoidCall(JEnvironmentRef envRef, TReceiver classOrObjectRef) { }
#pragma warning restore CA1822

	~NativeVoidParameterless() => this._disposed.Value = true;
}