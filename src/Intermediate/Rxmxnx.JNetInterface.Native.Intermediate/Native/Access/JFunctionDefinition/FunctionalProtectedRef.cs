// ReSharper disable MemberCanBePrivate.Global

namespace Rxmxnx.JNetInterface.Native.Access;

public partial class JFunctionDefinition<TResult>
{
	/// <summary>
	/// Invokes a function on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the function.</typeparam>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TResult? Invoke<TArgs>(JLocalObject jLocal, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jLocal.Environment;
		return env.AccessFeature.CallFunction<TResult, TArgs>(jLocal, jLocal.Class, this, false, ref args);
	}
	/// <summary>
	/// Invokes a function on <paramref name="jClass"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jLocal"/>.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the function.</typeparam>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TResult? Invoke<TArgs>(JLocalObject jLocal, JClassObject jClass, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jLocal.Environment;
		return env.AccessFeature.CallFunction<TResult, TArgs>(jLocal, jClass, this, false, ref args);
	}
	/// <summary>
	/// Invokes a function on <paramref name="jLocal"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jClass"/>.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the function.</typeparam>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TResult? InvokeNonVirtual<TArgs>(JLocalObject jLocal, JClassObject jClass, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jLocal.Environment;
		return env.AccessFeature.CallFunction<TResult, TArgs>(jLocal, jClass, this, true, ref args);
	}
	/// <summary>
	/// Invokes a static function on <paramref name="jClass"/> which matches with current definition
	/// passing the default value for each argument.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the function.</typeparam>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TResult? StaticInvoke<TArgs>(JClassObject jClass, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jClass.Environment;
		return env.AccessFeature.CallStaticFunction<TResult, TArgs>(jClass, this, ref args);
	}
	/// <summary>
	/// Invokes a reflected function which matches with current definition.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the function.</typeparam>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TResult? InvokeReflected<TArgs>(JMethodObject jMethod, JLocalObject jLocal, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jMethod.Environment;
		return env.AccessFeature.CallFunction<TResult, TArgs>(jMethod, jLocal, this, false, ref args);
	}
	/// <summary>
	/// Invokes a reflected function which matches with current definition.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the function.</typeparam>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TResult? InvokeNonVirtualReflected<TArgs>(JMethodObject jMethod, JLocalObject jLocal, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jMethod.Environment;
		return env.AccessFeature.CallFunction<TResult, TArgs>(jMethod, jLocal, this, true, ref args);
	}
	/// <summary>
	/// Invokes a reflected static function which matches with current definition.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the function.</typeparam>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TResult? InvokeStaticReflected<TArgs>(JMethodObject jMethod, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jMethod.Environment;
		return env.AccessFeature.CallStaticFunction<TResult, TArgs>(jMethod, this, ref args);
	}
}