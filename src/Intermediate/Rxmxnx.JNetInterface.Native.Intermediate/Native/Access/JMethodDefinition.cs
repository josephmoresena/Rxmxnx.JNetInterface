﻿namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores a method definition.
/// </summary>
public record JMethodDefinition : JCallDefinition
{
	/// <inheritdoc/>
	internal override Type? Return => default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="methodName">Method name.</param>
	/// <remarks>This constructor should be never inherited.</remarks>
	public JMethodDefinition(ReadOnlySpan<Byte> methodName) : base(methodName) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="methodName">Function name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	protected JMethodDefinition(ReadOnlySpan<Byte> methodName, params JArgumentMetadata[] metadata) : base(
		methodName, metadata) { }

	/// <summary>
	/// Retrieves a <see cref="JMethodObject"/> reflected from current definition on
	/// <paramref name="declaringClass"/>.
	/// </summary>
	/// <param name="declaringClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JMethodObject"/> instance.</returns>
	public JMethodObject GetReflected(JClassObject declaringClass)
	{
		IEnvironment env = declaringClass.Environment;
		return env.AccessFeature.GetReflectedMethod(this, declaringClass, false);
	}
	/// <summary>
	/// Retrieves a <see cref="JMethodObject"/> reflected from current static definition on
	/// <paramref name="declaringClass"/>.
	/// </summary>
	/// <param name="declaringClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JMethodObject"/> instance.</returns>
	public JMethodObject GetStaticReflected(JClassObject declaringClass)
	{
		IEnvironment env = declaringClass.Environment;
		return env.AccessFeature.GetReflectedMethod(this, declaringClass, true);
	}

	/// <summary>
	/// Invokes a method on <paramref name="jLocal"/> which matches with current definition passing the
	/// default value for each argument.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	protected void Invoke(JLocalObject jLocal) => this.Invoke(jLocal, this.CreateArgumentsArray());
	/// <summary>
	/// Invokes a method on <paramref name="jLocal"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jClass"/> passing the default value for each argument.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	protected void Invoke(JLocalObject jLocal, JClassObject jClass)
		=> this.Invoke(jLocal, jClass, this.CreateArgumentsArray());

	/// <summary>
	/// Invokes a method on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void Invoke(JLocalObject jLocal, IObject?[] args)
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
	protected void Invoke(JLocalObject jLocal, JClassObject jClass, IObject?[] args)
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
	protected void InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass, IObject?[] args)
	{
		IEnvironment env = jLocal.Environment;
		env.AccessFeature.CallMethod(jLocal, jClass, this, true, args);
	}
	/// <summary>
	/// Invokes a static method on <paramref name="jClass"/> which matches with current definition
	/// passing the default value for each argument.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	protected void StaticInvoke(JClassObject jClass) => this.Invoke(jClass, this.CreateArgumentsArray());
	/// <summary>
	/// Invokes a static method on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void StaticInvoke(JClassObject jClass, IObject?[] args)
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
	protected void InvokeReflected(JMethodObject jMethod, JLocalObject jLocal, IObject?[] args)
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
	protected void InvokeNonVirtualReflected(JMethodObject jMethod, JLocalObject jLocal, IObject?[] args)
	{
		IEnvironment env = jMethod.Environment;
		env.AccessFeature.CallMethod(jMethod, jLocal, this, true, args);
	}
	/// <summary>
	/// Invokes a reflected static method which matches with current definition.
	/// </summary>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void InvokeStaticReflected(JMethodObject jMethod, IObject?[] args)
	{
		IEnvironment env = jMethod.Environment;
		env.AccessFeature.CallStaticMethod(jMethod, this, args);
	}

	/// <summary>
	/// Invokes <paramref name="definition"/> on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="definition">A <see cref="JMethodDefinition"/> definition.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">The arguments to pass to.</param>
	internal static void Invoke(JMethodDefinition definition, JLocalObject jLocal, JClassObject? jClass = default,
		Boolean nonVirtual = false, IObject?[]? args = default)
	{
		IEnvironment env = jLocal.Environment;
		env.AccessFeature.CallInternalMethod(jLocal, jClass ?? jLocal.Class, definition, nonVirtual,
		                                     args ?? definition.CreateArgumentsArray());
	}
	/// <summary>
	/// Invokes <paramref name="definition"/> on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="definition">A <see cref="JMethodDefinition"/> definition.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	internal static void StaticInvoke(JMethodDefinition definition, JClassObject jClass, IObject?[]? args = default)
	{
		IEnvironment env = jClass.Environment;
		env.AccessFeature.CallInternalStaticMethod(jClass, definition, args ?? definition.CreateArgumentsArray());
	}

	/// <summary>
	/// Create a <see cref="JMethodDefinition"/> instance for <paramref name="metadata"/>.
	/// </summary>
	/// <param name="methodName">Method name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>A <see cref="JMethodDefinition"/> instance.</returns>
	public static JMethodDefinition Create(ReadOnlySpan<Byte> methodName, JArgumentMetadata[] metadata)
		=> new(methodName, metadata);
}