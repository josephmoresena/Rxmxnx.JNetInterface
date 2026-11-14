// ReSharper disable MemberCanBePrivate.Global

namespace Rxmxnx.JNetInterface.ContextBuilder;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
public ref partial struct SyncContextBuilder
{
	/// <summary>
	/// Creates a new JNI context and invokes <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TAction">Type of <see cref="IAndroidNativeAction"/>.</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="action">A <typeparamref name="TAction"/> instance.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Send<TAction>(SynchronizationContext context, TAction? action)
#if !NET9_0_OR_GREATER
		where TAction : IAndroidNativeAction
#else
		where TAction : IAndroidNativeAction, allows ref struct
#endif
	{
		if (action is null) return;
		if (typeof(TAction).IsValueType)
		{
			SyncContextBuilder.ActionSend(in this, context, ref action);
			return;
		}
		NonGenericNativeAction nonGeneric = NonGenericNativeAction.Create(action);
		this.Send(context, ref nonGeneric);
	}
	/// <summary>
	/// Creates a new JNI context and invokes <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TAction">Type of <see cref="IAndroidNativeAction"/>.</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="action">A <typeparamref name="TAction"/> instance.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Send<TAction>(SynchronizationContext context, ref TAction action)
#if !NET9_0_OR_GREATER
		where TAction : struct, IAndroidNativeAction
#else
		where TAction : struct, IAndroidNativeAction, allows ref struct
#endif
		=> SyncContextBuilder.ActionSend(in this, context, ref action);
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <typeparam name="TFunction">Type of <see cref="IAndroidNativeFunction{TResult}"/>.</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="func">A <typeparamref name="TFunction"/> instance.</param>
	/// <param name="result">Output. The result of the invoked function.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Send<TResult, TFunction>(SynchronizationContext context, TFunction? func, out TResult? result)
#if !NET9_0_OR_GREATER
		where TFunction : IAndroidNativeFunction<TResult>
#else
		where TFunction : IAndroidNativeFunction<TResult>, allows ref struct
#endif
	{
		if (func is null)
		{
			Unsafe.SkipInit(out result);
			return;
		}
		if (typeof(TFunction).IsValueType)
		{
			SyncContextBuilder.FunctionSend(in this, context, ref func, out result);
			return;
		}
		NonGenericNativeFunction<TResult> nonGeneric = NonGenericNativeFunction<TResult>.Create(func);
		this.Send(context, ref nonGeneric, out result);
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <typeparam name="TFunction">Type of <see cref="IAndroidNativeFunction{TResult}"/>.</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="func">A <typeparamref name="TFunction"/> instance.</param>
	/// <param name="result">Output. The result of the invoked function.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Send<TResult, TFunction>(SynchronizationContext context, ref TFunction func, out TResult? result)
#if !NET9_0_OR_GREATER
		where TFunction : struct, IAndroidNativeFunction<TResult>
#else
		where TFunction : struct, IAndroidNativeFunction<TResult>, allows ref struct
#endif
		=> SyncContextBuilder.FunctionSend(in this, context, ref func, out result);
}