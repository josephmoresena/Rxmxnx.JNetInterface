namespace Rxmxnx.JNetInterface.Native.Dummies;

public abstract partial class EnvironmentProxy
{
	/// <inheritdoc/>
	public abstract IntPtr GetDirectAddress(JBufferObject buffer);
	/// <inheritdoc/>
	public abstract Int64 GetDirectCapacity(JBufferObject buffer);
}