namespace Rxmxnx.JNetInterface.Proxies;

using TDirectBuffer =
#if !PACKAGE
	JBufferObject
#else
	JByteBufferObject
#endif
	;

public abstract partial class EnvironmentProxy
{
	TDirectBuffer INioFeature.NewDirectByteBuffer(IFixedMemory.IDisposable memory) => this.NewDirectByteBuffer(memory);
	void INioFeature.WithDirectByteBuffer<TBuffer>(Int32 capacity, Action<TBuffer> action)
	{
		using TBuffer buffer = (TBuffer)(Object)this.CreateEphemeralByteBuffer(capacity);
		action(buffer);
	}
	void INioFeature.WithDirectByteBuffer<TBuffer, TState>(Int32 capacity, TState state, Action<TBuffer, TState> action)
	{
		using TBuffer buffer = (TBuffer)(Object)this.CreateEphemeralByteBuffer(capacity);
		action(buffer, state);
	}
	TResult INioFeature.WithDirectByteBuffer<TBuffer, TResult>(Int32 capacity, Func<TBuffer, TResult> func)
	{
		using TBuffer buffer = (TBuffer)(Object)this.CreateEphemeralByteBuffer(capacity);
		return func(buffer);
	}
	TResult INioFeature.WithDirectByteBuffer<TBuffer, TState, TResult>(Int32 capacity, TState state,
		Func<TBuffer, TState, TResult> func)
	{
		using TBuffer buffer = (TBuffer)(Object)this.CreateEphemeralByteBuffer(capacity);
		return func(buffer, state);
	}
}