namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Defines a specific operation or behavior to be executed within the context of a JNI context.
/// </summary>
internal readonly struct AndroidNativeAction(AndroidJniAction action) : IAndroidNativeAction
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IAndroidNativeAction.Accept(AndroidJniContext context) => action(context);
}

/// <summary>
/// Defines a specific operation or behavior to be executed within the context of a JNI context.
/// </summary>
internal readonly struct AndroidNativeAction<TState>(TState state, AndroidJniAction<TState> action)
	: IAndroidNativeAction
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IAndroidNativeAction.Accept(AndroidJniContext context) => action(context, state);
}

/// <summary>
/// Defines a specific operation or behavior to be executed within the context of a JNI context.
/// </summary>
internal readonly struct AndroidNativeFunction<TResult>(AndroidJniFunc<TResult> func) : IAndroidNativeFunction<TResult>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	TResult IAndroidNativeFunction<TResult>.Apply(AndroidJniContext context) => func(context);
}

/// <summary>
/// Defines a specific operation or behavior to be executed within the context of a JNI context.
/// </summary>
internal readonly struct AndroidNativeFunction<TResult, TState>(TState state, AndroidJniFunc<TState, TResult> func)
	: IAndroidNativeFunction<TResult>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	TResult IAndroidNativeFunction<TResult>.Apply(AndroidJniContext context) => func(context, state);
}