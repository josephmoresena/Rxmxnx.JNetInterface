namespace Rxmxnx.JNetInterface.Tests.Restricted;

[ExcludeFromCodeCoverage]
public abstract class NioFeatureProxy : INioFeature
{
	public abstract IntPtr GetDirectAddress(JBufferObject buffer);
	public abstract Int64 GetDirectCapacity(JBufferObject buffer);
	public abstract JBufferObject NewDirectByteBuffer(IFixedMemory.IDisposable memory);
	public abstract void WithDirectByteBuffer<TBuffer>(Int32 capacity, Action<TBuffer> action)
		where TBuffer : JBufferObject;
	public abstract void WithDirectByteBuffer<TBuffer, TState>(Int32 capacity, TState state,
		Action<TBuffer, TState> action) where TBuffer : JBufferObject;
	public abstract TResult WithDirectByteBuffer<TBuffer, TResult>(Int32 capacity, Func<TBuffer, TResult> func)
		where TBuffer : JBufferObject;
	public abstract TResult WithDirectByteBuffer<TBuffer, TState, TResult>(Int32 capacity, TState state,
		Func<TBuffer, TState, TResult> func) where TBuffer : JBufferObject;
}