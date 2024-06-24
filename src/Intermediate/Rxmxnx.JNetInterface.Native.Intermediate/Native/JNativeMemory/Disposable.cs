namespace Rxmxnx.JNetInterface.Native;

public abstract partial class JNativeMemory
{
	/// <inheritdoc/>
	public void Dispose()
	{
		this.ReleaseUnmanagedResources();
		GC.SuppressFinalize(this);
	}

	/// <inheritdoc cref="IDisposable.Dispose"/>
	private void ReleaseUnmanagedResources()
	{
		if (this._disposed.Value) return;
		this._disposed.Value = true;
		this._adapter.Release(this.ReleaseMode);
		this._context.Dispose();
	}
}