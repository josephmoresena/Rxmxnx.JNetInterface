namespace Rxmxnx.JNetInterface.Restricted;

using TDirectBuffer =
#if !PACKAGE
	JBufferObject
#else
	JByteBufferObject
#endif
	;

internal partial interface INioFeature
{
	/// <summary>
	/// Creates a direct <see cref="JBufferObject"/> instance.
	/// </summary>
	/// <param name="memory">A <see cref="IFixedMemory"/> instance.</param>
	/// <returns>A direct <see cref="JBufferObject"/> instance.</returns>
	internal TDirectBuffer NewDirectByteBuffer(IFixedMemory.IDisposable memory);
	/// <summary>
	/// Creates an ephemeral <c>java.nio.DirectByteBuffer</c> instance and executes <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TBuffer">Type of created buffer.</typeparam>
	/// <param name="capacity">Capacity of created buffer.</param>
	/// <param name="action">Action to execute.</param>
	internal void WithDirectByteBuffer<TBuffer>(Int32 capacity, Action<TBuffer> action) where TBuffer : TDirectBuffer;
	/// <summary>
	/// Creates an ephemeral <c>java.nio.DirectByteBuffer</c> instance and executes <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TBuffer">Type of created buffer.</typeparam>
	/// <typeparam name="TState">The type of the state object used by the action.</typeparam>
	/// <param name="capacity">Capacity of created buffer.</param>
	/// <param name="state">The state object of type <typeparamref name="TState"/>.</param>
	/// <param name="action">Action to execute.</param>
	internal void WithDirectByteBuffer<TBuffer, TState>(Int32 capacity, TState state, Action<TBuffer, TState> action)
#if NET9_0_OR_GREATER
	where TState : allows ref struct
#endif
		where TBuffer : TDirectBuffer;
	/// <summary>
	/// Creates an ephemeral <c>java.nio.DirectByteBuffer</c> instance and executes <paramref name="func"/>.
	/// </summary>
	/// <typeparam name="TBuffer">Type of created buffer.</typeparam>
	/// <typeparam name="TResult">The type of the return value of the function.</typeparam>
	/// <param name="capacity">Capacity of created buffer.</param>
	/// <param name="func">Function to execute.</param>
	/// <returns>The result of <paramref name="func"/> execution.</returns>
	internal TResult WithDirectByteBuffer<TBuffer, TResult>(Int32 capacity, Func<TBuffer, TResult> func)
		where TBuffer : TDirectBuffer;
	/// <summary>
	/// Creates an ephemeral <c>java.nio.DirectByteBuffer</c> instance and executes <paramref name="func"/>.
	/// </summary>
	/// <typeparam name="TBuffer">Type of created buffer.</typeparam>
	/// <typeparam name="TState">The type of the state object used by the function.</typeparam>
	/// <typeparam name="TResult">The type of the return value of the function.</typeparam>
	/// <param name="capacity">Capacity of created buffer.</param>
	/// <param name="state">The state object of type <typeparamref name="TState"/>.</param>
	/// <param name="func">Function to execute.</param>
	/// <returns>The result of <paramref name="func"/> execution.</returns>
	internal TResult WithDirectByteBuffer<TBuffer, TState, TResult>(Int32 capacity, TState state,
		Func<TBuffer, TState, TResult> func)
#if NET9_0_OR_GREATER
	where TState : allows ref struct
#endif
		where TBuffer : TDirectBuffer;
}