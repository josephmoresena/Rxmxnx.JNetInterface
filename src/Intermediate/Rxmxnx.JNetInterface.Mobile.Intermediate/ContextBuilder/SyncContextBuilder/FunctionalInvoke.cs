// ReSharper disable MemberCanBePrivate.Global

namespace Rxmxnx.JNetInterface.ContextBuilder;

public ref partial struct SyncContextBuilder
{
	/// <summary>
	/// Creates a new JNI context and invokes <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TAction">Type of <see cref="IAndroidNativeAction"/>.</typeparam>
	/// <param name="action">A <typeparamref name="TAction"/> instance.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Invoke<TAction>(TAction action)
#if !NET9_0_OR_GREATER
		where TAction : IAndroidNativeAction
#else
		where TAction : IAndroidNativeAction, allows ref struct
#endif
	{
		if (typeof(TAction).IsValueType)
		{
			this.ActionInvoke(ref action);
			return;
		}
		NonGenericNativeAction nonGeneric = NonGenericNativeAction.Create(action);
		this.Invoke(ref nonGeneric);
	}
	/// <summary>
	/// Creates a new JNI context and invokes <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TAction">Type of <see cref="IAndroidNativeAction"/>.</typeparam>
	/// <param name="action">A <typeparamref name="TAction"/> instance.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Invoke<TAction>(ref TAction action)
#if !NET9_0_OR_GREATER
		where TAction : struct, IAndroidNativeAction
#else
		where TAction : struct, IAndroidNativeAction, allows ref struct
#endif
		=> this.ActionInvoke(ref action);
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <typeparam name="TFunction">Type of <see cref="IAndroidNativeFunction{TResult}"/>.</typeparam>
	/// <param name="func">A <typeparamref name="TFunction"/> instance.</param>
	/// <param name="result">Output. The result of the invoked function.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Invoke<TResult, TFunction>(TFunction func, out TResult result)
#if !NET9_0_OR_GREATER
		where TFunction : IAndroidNativeFunction<TResult>
#else
		where TFunction : IAndroidNativeFunction<TResult>, allows ref struct
#endif
	{
		if (typeof(TFunction).IsValueType)
		{
			result = this.FunctionInvoke<TResult, TFunction>(ref func);
			return;
		}
		NonGenericNativeFunction<TResult> nonGeneric = NonGenericNativeFunction<TResult>.Create(func);
		this.Invoke(ref nonGeneric, out result);
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <typeparam name="TFunction">Type of <see cref="IAndroidNativeFunction{TResult}"/>.</typeparam>
	/// <param name="func">A <typeparamref name="TFunction"/> instance.</param>
	/// <param name="result">Output. The result of the invoked function.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Invoke<TResult, TFunction>(ref TFunction func, out TResult result)
#if !NET9_0_OR_GREATER
		where TFunction : struct, IAndroidNativeFunction<TResult>
#else
		where TFunction : struct, IAndroidNativeFunction<TResult>, allows ref struct
#endif
		=> result = this.FunctionInvoke<TResult, TFunction>(ref func);
}