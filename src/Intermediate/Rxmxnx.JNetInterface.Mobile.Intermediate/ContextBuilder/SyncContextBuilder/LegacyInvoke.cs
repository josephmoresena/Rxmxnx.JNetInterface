// ReSharper disable MemberCanBePrivate.Global

namespace Rxmxnx.JNetInterface.ContextBuilder;

public ref partial struct SyncContextBuilder
{
	/// <summary>
	/// Creates a new JNI context and invokes <paramref name="action"/>.
	/// </summary>
	/// <param name="action">A <see cref="AndroidJniAction"/> delegate.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Invoke(AndroidJniAction action)
	{
		AndroidNativeAction nativeAction = new(action);
		this.Invoke(ref nativeAction);
	}
	/// <summary>
	/// Creates a new JNI context and invokes <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <param name="state">A state instance object.</param>
	/// <param name="action">A <see cref="AndroidJniAction{TState}"/> delegate.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Invoke<TState>(TState state, AndroidJniAction<TState> action)
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
	{
		AndroidNativeAction<TState> nativeAction = new(state, action);
		this.Invoke(ref nativeAction);
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>A <typeparamref name="TResult"/> function result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TResult Invoke<TResult>(AndroidJniFunc<TResult> func)
	{
		AndroidNativeFunction<TResult> nativeFunction = new(func);
		this.Invoke(ref nativeFunction, out TResult result);
		return result;
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>A <typeparamref name="TResult"/> function result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TResult? Invoke<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
		AndroidJniFunc<JReferenceObject?> func) where TResult : class, IJavaPeerable
	{
		AndroidNativeFunction<JReferenceObject?> nativeFunction = new(func);
		this.Invoke(ref nativeFunction, out TResult? result);
		return result;
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="state">A state instance object.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>A <typeparamref name="TResult"/> function result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TResult Invoke<TState, TResult>(TState state, AndroidJniFunc<TState, TResult> func)
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
	{
		AndroidNativeFunction<TResult, TState> nativeFunction = new(state, func);
		this.Invoke(ref nativeFunction, out TResult result);
		return result;
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="state">A state instance object.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>A <typeparamref name="TResult"/> function result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TResult? Invoke<TState, [DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
		TState state, AndroidJniFunc<TState, JReferenceObject?> func) where TResult : class, IJavaPeerable
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
	{
		AndroidNativeFunction<JReferenceObject?, TState> nativeFunction = new(state, func);
		this.Invoke(ref nativeFunction, out TResult? result);
		return result;
	}
}