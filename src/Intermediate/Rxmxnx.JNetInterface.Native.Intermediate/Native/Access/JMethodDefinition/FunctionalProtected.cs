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
	protected void Invoke<TArgs>(JLocalObject jLocal, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jLocal.Environment;
		env.AccessFeature.CallMethod(jLocal, jLocal.Class, this, false, in args);
	}
	/// <summary>
	/// Invokes a method on <paramref name="jLocal"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jClass"/>.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the method.</typeparam>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void Invoke<TArgs>(JLocalObject jLocal, JClassObject jClass, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jLocal.Environment;
		env.AccessFeature.CallMethod(jLocal, jClass, this, false, in args);
	}
	/// <summary>
	/// Invokes a method on <paramref name="jLocal"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jClass"/>.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the method.</typeparam>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void InvokeNonVirtual<TArgs>(JLocalObject jLocal, JClassObject jClass, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jLocal.Environment;
		env.AccessFeature.CallMethod(jLocal, jClass, this, true, in args);
	}
	/// <summary>
	/// Invokes a static method on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the method.</typeparam>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void StaticInvoke<TArgs>(JClassObject jClass, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jClass.Environment;
		env.AccessFeature.CallStaticMethod(jClass, this, in args);
	}
	/// <summary>
	/// Invokes a reflected method which matches with current definition.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the method.</typeparam>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void InvokeReflected<TArgs>(JMethodObject jMethod, JLocalObject jLocal, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jMethod.Environment;
		env.AccessFeature.CallMethod(jMethod, jLocal, this, false, in args);
	}
	/// <summary>
	/// Invokes a reflected method which matches with current definition.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the method.</typeparam>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void InvokeNonVirtualReflected<TArgs>(JMethodObject jMethod, JLocalObject jLocal, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jMethod.Environment;
		env.AccessFeature.CallMethod(jMethod, jLocal, this, true, in args);
	}
	/// <summary>
	/// Invokes a reflected static method which matches with current definition.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the method.</typeparam>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void InvokeStaticReflected<TArgs>(JMethodObject jMethod, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jMethod.Environment;
		env.AccessFeature.CallStaticMethod(jMethod, this, in args);
	}
}