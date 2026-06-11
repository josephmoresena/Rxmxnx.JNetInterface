//[assembly: SupportedOSPlatform("android")]

namespace Rxmxnx.JNetInterface;

/// <summary>
/// Delegate for Android JNI action.
/// </summary>
public delegate void AndroidJniAction(AndroidJniContext context);

/// <summary>
/// Delegate for Android JNI action.
/// </summary>
/// <typeparam name="TState">Type of state object</typeparam>
public delegate void AndroidJniAction<in TState>(AndroidJniContext context, TState state)
#if NET9_0_OR_GREATER
	where TState : allows ref struct;
#else
	;
#endif

/// <summary>
/// Delegate for Android JNI function.
/// </summary>
/// <typeparam name="TResult">Type of result value</typeparam>
public delegate TResult AndroidJniFunc<out TResult>(AndroidJniContext context);

/// <summary>
/// Delegate for Android JNI function.
/// </summary>
/// <typeparam name="TResult">Type of result value</typeparam>
/// <typeparam name="TState">Type of state object</typeparam>
public delegate TResult AndroidJniFunc<in TState, out TResult>(AndroidJniContext context, TState state)
#if NET9_0_OR_GREATER
	where TState : allows ref struct;
#else
	;
#endif