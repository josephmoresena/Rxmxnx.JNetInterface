// ReSharper disable MemberCanBePrivate.Global

namespace Rxmxnx.JNetInterface.Native.Access;

public partial class JMethodDefinition
{
	/// <summary>
	/// Invokes a method on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the method.</typeparam>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void Invoke<TArgs>(JLocalObject jLocal, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jLocal.Environment;
		env.AccessFeature.CallMethod(jLocal, jLocal.Class, this, false, ref args);
	}
	/// <summary>
	/// Invokes a method on <paramref name="jLocal"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jClass"/>.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the method.</typeparam>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="args">The arguments to pass to.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void Invoke<TArgs>(JLocalObject jLocal, JClassObject jClass, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jLocal.Environment;
		env.AccessFeature.CallMethod(jLocal, jClass, this, false, ref args);
	}
	/// <summary>
	/// Invokes a method on <paramref name="jLocal"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jClass"/>.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the method.</typeparam>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="args">The arguments to pass to.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void InvokeNonVirtual<TArgs>(JLocalObject jLocal, JClassObject jClass, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jLocal.Environment;
		env.AccessFeature.CallMethod(jLocal, jClass, this, true, ref args);
	}
	/// <summary>
	/// Invokes a static method on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the method.</typeparam>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void StaticInvoke<TArgs>(JClassObject jClass, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jClass.Environment;
		env.AccessFeature.CallStaticMethod(jClass, this, ref args);
	}
	/// <summary>
	/// Invokes a reflected method which matches with current definition.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the method.</typeparam>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void InvokeReflected<TArgs>(JMethodObject jMethod, JLocalObject jLocal, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jMethod.Environment;
		env.AccessFeature.CallMethod(jMethod, jLocal, this, false, ref args);
	}
	/// <summary>
	/// Invokes a reflected method which matches with current definition.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the method.</typeparam>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void InvokeNonVirtualReflected<TArgs>(JMethodObject jMethod, JLocalObject jLocal, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jMethod.Environment;
		env.AccessFeature.CallMethod(jMethod, jLocal, this, true, ref args);
	}
	/// <summary>
	/// Invokes a reflected static method which matches with current definition.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the method.</typeparam>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void InvokeStaticReflected<TArgs>(JMethodObject jMethod, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jMethod.Environment;
		env.AccessFeature.CallStaticMethod(jMethod, this, ref args);
	}
}