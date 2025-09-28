namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
internal sealed class NativeVoidParameterless<TReceiver> where TReceiver : unmanaged, INativePointerType<TReceiver>
{
	private readonly IMutableWrapper<Boolean> _disposed = IMutableReference<Boolean>.Create();

	public IWrapper<Boolean> IsDisposed => this._disposed;

	public void VoidCall(JEnvironmentRef envRef, TReceiver classOrObjectRef) { }

	~NativeVoidParameterless() => this._disposed.Value = true;
}