namespace Rxmxnx.JNetInterface.Native.Access;

public partial class JMethodDefinition
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="methodName">Function name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	protected JMethodDefinition(ReadOnlySpan<Byte> methodName,
#if !NET9_0_OR_GREATER
		params
#endif
			JArgumentMetadata[] metadata) : this(methodName, metadata.AsSpan()) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="methodName">Function name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	protected JMethodDefinition(ReadOnlySpan<Byte> methodName,
#if NET9_0_OR_GREATER
		params
#endif
		ReadOnlySpan<JArgumentMetadata> metadata) : base(methodName, metadata) { }

	/// <summary>
	/// Invokes a method on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void Invoke(JLocalObject jLocal,
#if NET9_0_OR_GREATER
		params
#endif
		ReadOnlySpan<IObject?> args)
	{
		IEnvironment env = jLocal.Environment;
		env.AccessFeature.CallMethod(jLocal, jLocal.Class, this, false, args);
	}
	/// <summary>
	/// Invokes a method on <paramref name="jLocal"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void Invoke(JLocalObject jLocal, JClassObject jClass,
#if NET9_0_OR_GREATER
		params
#endif
		ReadOnlySpan<IObject?> args)
	{
		IEnvironment env = jLocal.Environment;
		env.AccessFeature.CallMethod(jLocal, jClass, this, false, args);
	}
	/// <summary>
	/// Invokes a method on <paramref name="jLocal"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass,
#if NET9_0_OR_GREATER
		params
#endif
		ReadOnlySpan<IObject?> args)
	{
		IEnvironment env = jLocal.Environment;
		env.AccessFeature.CallMethod(jLocal, jClass, this, true, args);
	}
	/// <summary>
	/// Invokes a static method on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void StaticInvoke(JClassObject jClass,
#if NET9_0_OR_GREATER
		params
#endif
		ReadOnlySpan<IObject?> args)
	{
		IEnvironment env = jClass.Environment;
		env.AccessFeature.CallStaticMethod(jClass, this, args);
	}
	/// <summary>
	/// Invokes a reflected method which matches with current definition.
	/// </summary>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void InvokeReflected(JMethodObject jMethod, JLocalObject jLocal,
#if NET9_0_OR_GREATER
		params
#endif
		ReadOnlySpan<IObject?> args)
	{
		IEnvironment env = jMethod.Environment;
		env.AccessFeature.CallMethod(jMethod, jLocal, this, false, args);
	}
	/// <summary>
	/// Invokes a reflected method which matches with current definition.
	/// </summary>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void InvokeNonVirtualReflected(JMethodObject jMethod, JLocalObject jLocal,
#if NET9_0_OR_GREATER
		params
#endif
		ReadOnlySpan<IObject?> args)
	{
		IEnvironment env = jMethod.Environment;
		env.AccessFeature.CallMethod(jMethod, jLocal, this, true, args);
	}
	/// <summary>
	/// Invokes a reflected static method which matches with current definition.
	/// </summary>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void InvokeStaticReflected(JMethodObject jMethod,
#if NET9_0_OR_GREATER
		params
#endif
		ReadOnlySpan<IObject?> args)
	{
		IEnvironment env = jMethod.Environment;
		env.AccessFeature.CallStaticMethod(jMethod, this, args);
	}
}