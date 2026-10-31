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
	protected TResult? Invoke<TArgs>(JLocalObject jLocal, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return this.Invoke(jLocal, ref ICallArgument.Default);
		if (typeof(TArgs).IsValueType)
		{
			IEnvironment env = jLocal.Environment;
			return env.AccessFeature.CallFunction<TResult, TArgs>(jLocal, jLocal.Class, this, false, ref args);
		}
		NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
		return this.Invoke(jLocal, ref nonGeneric);
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
	protected TResult? Invoke<TArgs>(JLocalObject jLocal, JClassObject jClass, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return this.Invoke(jLocal, jClass, ref ICallArgument.Default);
		if (typeof(TArgs).IsValueType)
		{
			IEnvironment env = jLocal.Environment;
			return env.AccessFeature.CallFunction<TResult, TArgs>(jLocal, jClass, this, false, ref args);
		}
		NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
		return this.Invoke(jLocal, jClass, ref nonGeneric);
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
	protected TResult? InvokeNonVirtual<TArgs>(JLocalObject jLocal, JClassObject jClass, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return this.InvokeNonVirtual(jLocal, jClass, ref ICallArgument.Default);
		if (typeof(TArgs).IsValueType)
		{
			IEnvironment env = jLocal.Environment;
			return env.AccessFeature.CallFunction<TResult, TArgs>(jLocal, jClass, this, true, ref args);
		}
		NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
		return this.InvokeNonVirtual(jLocal, jClass, ref nonGeneric);
	}
	/// <summary>
	/// Invokes a static function on <paramref name="jClass"/> which matches with current definition
	/// passing the default value for each argument.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the function.</typeparam>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	protected TResult? StaticInvoke<TArgs>(JClassObject jClass, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return this.StaticInvoke(jClass, ref ICallArgument.Default);
		if (typeof(TArgs).IsValueType)
		{
			IEnvironment env = jClass.Environment;
			return env.AccessFeature.CallStaticFunction<TResult, TArgs>(jClass, this, ref args);
		}
		NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
		return this.StaticInvoke(jClass, ref nonGeneric);
	}
	/// <summary>
	/// Invokes a reflected function which matches with current definition.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the function.</typeparam>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	protected TResult? InvokeReflected<TArgs>(JMethodObject jMethod, JLocalObject jLocal, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return this.InvokeReflected(jMethod, jLocal, ref ICallArgument.Default);
		if (typeof(TArgs).IsValueType)
		{
			IEnvironment env = jMethod.Environment;
			return env.AccessFeature.CallFunction<TResult, TArgs>(jMethod, jLocal, this, false, ref args);
		}
		NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
		return this.InvokeReflected(jMethod, jLocal, ref nonGeneric);
	}
	/// <summary>
	/// Invokes a reflected function which matches with current definition.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the function.</typeparam>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	protected TResult? InvokeNonVirtualReflected<TArgs>(JMethodObject jMethod, JLocalObject jLocal, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return this.InvokeNonVirtualReflected(jMethod, jLocal, ref ICallArgument.Default);
		if (typeof(TArgs).IsValueType)
		{
			IEnvironment env = jMethod.Environment;
			return env.AccessFeature.CallFunction<TResult, TArgs>(jMethod, jLocal, this, true, ref args);
		}
		NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
		return this.InvokeNonVirtualReflected(jMethod, jLocal, ref nonGeneric);
	}
	/// <summary>
	/// Invokes a reflected static function which matches with current definition.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the function.</typeparam>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	protected TResult? InvokeStaticReflected<TArgs>(JMethodObject jMethod, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return this.InvokeStaticReflected(jMethod, ref ICallArgument.Default);
		if (typeof(TArgs).IsValueType)
		{
			IEnvironment env = jMethod.Environment;
			return env.AccessFeature.CallStaticFunction<TResult, TArgs>(jMethod, this, ref args);
		}
		NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
		return this.InvokeStaticReflected(jMethod, ref nonGeneric);
	}
}