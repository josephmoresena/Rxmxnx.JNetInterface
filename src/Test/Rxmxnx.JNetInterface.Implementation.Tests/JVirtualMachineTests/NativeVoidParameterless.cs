namespace Rxmxnx.JNetInterface.Tests;

public partial class JVirtualMachineTests
{
	[ExcludeFromCodeCoverage]
	public sealed class NativeVoidParameterless<TReceiver> where TReceiver : unmanaged, IWrapper<JObjectLocalRef>
	{
		private readonly IMutableWrapper<Boolean> _disposed = IMutableReference<Boolean>.Create();

		public IWrapper<Boolean> IsDisposed => this._disposed;

		public void VoidCall(JEnvironmentRef envRef, TReceiver classOrObjectRef) { }

		~NativeVoidParameterless() => this._disposed.Value = true;
	}
}