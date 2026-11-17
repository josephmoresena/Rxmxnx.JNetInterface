namespace Rxmxnx.JNetInterface.ContextBuilder;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
public unsafe ref partial struct SyncContextBuilder
{
#pragma warning disable CS8500
	/// <summary>
	/// Executes an action on a specific <see cref="SynchronizationContext"/> and ensures any resulting exception
	/// is properly propagated.
	/// </summary>
	/// <typeparam name="TAction">The type of the action, implementing <see cref="IAndroidNativeAction"/>.</typeparam>
	/// <param name="builder">Current builder instance.</param>
	/// <param name="context">The synchronization context on which the action will be executed.</param>
	/// <param name="action">The action to be executed, passed by reference.</param>
	/// <exception cref="Exception">Thrown if an exception occurs during the execution of the action.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void ActionSend<TAction>(in SyncContextBuilder builder, SynchronizationContext context,
		ref TAction action)
#if !NET9_0_OR_GREATER
		where TAction : IAndroidNativeAction
#else
		where TAction : IAndroidNativeAction, allows ref struct
#endif
	{
		Exception? ex;
		fixed (TAction* actionPtr = &action)
		fixed (SyncContextBuilder* builderPtr = &builder)
		{
			SendActionState<TAction> state = new(builderPtr) { ActionPointer = actionPtr, };
			context.Send(SendState.Send, state);
			ex = state.Exception;
		}
		if (ex is not null) throw ex;
	}
	/// <summary>
	/// Executes the specified function within the provided <paramref name="context"/> and returns the result.
	/// </summary>
	/// <typeparam name="TResult">The type of the result returned by the function.</typeparam>
	/// <typeparam name="TFunction">The type of the function implementing <see cref="IAndroidNativeFunction{TResult}"/>.</typeparam>
	/// <param name="builder">Current builder instance.</param>
	/// <param name="context">The synchronization context within which the function is executed.</param>
	/// <param name="func">A reference to the function to be executed.</param>
	/// <param name="result">Output. The result of the invoked function.</param>
	/// <returns>The result produced by the executed function.</returns>
	/// <exception cref="Exception">Thrown if any exception occurs during the execution of the function.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void FunctionSend<TResult, TFunction>(in SyncContextBuilder builder, SynchronizationContext context,
		ref TFunction func, out TResult? result)
#if !NET9_0_OR_GREATER
		where TFunction : IAndroidNativeFunction<TResult>
#else
		where TFunction : IAndroidNativeFunction<TResult>, allows ref struct
#endif
	{
		Exception? ex;
		fixed (TFunction* funcPtr = &func)
		fixed (SyncContextBuilder* builderPtr = &builder)
		{
			SendFunctionState<TResult, TFunction> state = new(builderPtr) { FunctionPointer = funcPtr, };
			context.Send(SendState.Send, state);
			result = state.Result;
			ex = state.Exception;
		}
		if (ex is not null) throw ex;
	}
#pragma warning restore CS8500
}