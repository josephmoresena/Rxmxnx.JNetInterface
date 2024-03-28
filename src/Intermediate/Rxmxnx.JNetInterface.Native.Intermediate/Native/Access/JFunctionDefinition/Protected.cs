namespace Rxmxnx.JNetInterface.Native.Access;

public partial class JFunctionDefinition<TResult>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="functionName">Function name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	protected JFunctionDefinition(ReadOnlySpan<Byte> functionName, params JArgumentMetadata[] metadata) : base(
		functionName, IDataType.GetMetadata<TResult>().Signature, metadata) { }

	/// <summary>
	/// Invokes a function on <paramref name="jLocal"/> which matches with current definition passing the
	/// default value for each argument.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	protected TResult? Invoke(JLocalObject jLocal) => this.Invoke(jLocal, this.CreateArgumentsArray());
	/// <summary>
	/// Invokes a function on <paramref name="jLocal"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jClass"/> passing the default value for each argument.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	protected TResult? Invoke(JLocalObject jLocal, JClassObject jClass)
		=> this.Invoke(jLocal, jClass, this.CreateArgumentsArray());
	/// <summary>
	/// Invokes a function on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	protected TResult? Invoke(JLocalObject jLocal, IObject?[] args)
	{
		IEnvironment env = jLocal.Environment;
		return env.AccessFeature.CallFunction<TResult>(jLocal, jLocal.Class, this, false, args);
	}
	/// <summary>
	/// Invokes a function on <paramref name="jClass"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	protected TResult? Invoke(JLocalObject jLocal, JClassObject jClass, IObject?[] args)
	{
		IEnvironment env = jLocal.Environment;
		return env.AccessFeature.CallFunction<TResult>(jLocal, jClass, this, false, args);
	}
	/// <summary>
	/// Invokes a function on <paramref name="jLocal"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jClass"/> passing the default value for each argument.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	protected TResult? InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass)
		=> this.InvokeNonVirtual(jLocal, jClass, this.CreateArgumentsArray());
	/// <summary>
	/// Invokes a function on <paramref name="jLocal"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	protected TResult? InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass, IObject?[] args)
	{
		IEnvironment env = jLocal.Environment;
		return env.AccessFeature.CallFunction<TResult>(jLocal, jClass, this, true, args);
	}
	/// <summary>
	/// Invokes a static function on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	protected TResult? StaticInvoke(JClassObject jClass) => this.StaticInvoke(jClass, this.CreateArgumentsArray());
	/// <summary>
	/// Invokes a static function on <paramref name="jClass"/> which matches with current definition
	/// passing the default value for each argument.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	protected TResult? StaticInvoke(JClassObject jClass, IObject?[] args)
	{
		IEnvironment env = jClass.Environment;
		return env.AccessFeature.CallStaticFunction<TResult>(jClass, this, args);
	}
	/// <summary>
	/// Invokes a reflected function which matches with current definition passing the default value for each argument.
	/// </summary>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	protected TResult? InvokeReflected(JMethodObject jMethod, JLocalObject jLocal)
		=> this.InvokeReflected(jMethod, jLocal, this.CreateArgumentsArray());
	/// <summary>
	/// Invokes a reflected function which matches with current definition.
	/// </summary>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	protected TResult? InvokeReflected(JMethodObject jMethod, JLocalObject jLocal, IObject?[] args)
	{
		IEnvironment env = jMethod.Environment;
		return env.AccessFeature.CallFunction<TResult>(jMethod, jLocal, this, false, args);
	}
	/// <summary>
	/// Invokes a reflected function which matches with current definition passing the default value for each argument.
	/// </summary>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	protected TResult? InvokeNonVirtualReflected(JMethodObject jMethod, JLocalObject jLocal)
		=> this.InvokeNonVirtualReflected(jMethod, jLocal, this.CreateArgumentsArray());
	/// <summary>
	/// Invokes a reflected function which matches with current definition.
	/// </summary>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	protected TResult? InvokeNonVirtualReflected(JMethodObject jMethod, JLocalObject jLocal, IObject?[] args)
	{
		IEnvironment env = jMethod.Environment;
		return env.AccessFeature.CallFunction<TResult>(jMethod, jLocal, this, true, args);
	}
	/// <summary>
	/// Invokes a reflected static function which matches with current definition passing the default value for each argument.
	/// </summary>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	protected TResult? InvokeStaticReflected(JMethodObject jMethod)
		=> this.InvokeStaticReflected(jMethod, this.CreateArgumentsArray());
	/// <summary>
	/// Invokes a reflected static function which matches with current definition.
	/// </summary>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	protected TResult? InvokeStaticReflected(JMethodObject jMethod, IObject?[] args)
	{
		IEnvironment env = jMethod.Environment;
		return env.AccessFeature.CallStaticFunction<TResult>(jMethod, this, args);
	}
}