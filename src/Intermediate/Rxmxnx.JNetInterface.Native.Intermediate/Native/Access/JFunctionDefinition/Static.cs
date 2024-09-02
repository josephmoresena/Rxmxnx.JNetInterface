namespace Rxmxnx.JNetInterface.Native.Access;

public abstract partial class JFunctionDefinition
{
	/// <summary>
	/// Invokes <paramref name="definition"/> on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="definition">A <see cref="JFunctionDefinition{TResult}"/> definition.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	internal static TResult? Invoke<TResult>(JFunctionDefinition<TResult> definition, JLocalObject jLocal,
		JClassObject? jClass = default, Boolean nonVirtual = false, ReadOnlySpan<IObject?> args = default)
		where TResult : IDataType<TResult>
		=> JFunctionDefinition<TResult>.Invoke(definition, jLocal, jClass, nonVirtual, args);
	/// <summary>
	/// Invokes <paramref name="definition"/> on <paramref name="jClass"/> which matches with current definition
	/// passing the default value for each argument.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="definition">A <see cref="JFunctionDefinition{TResult}"/> definition.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	internal static TResult? StaticInvoke<TResult>(JFunctionDefinition<TResult> definition, JClassObject jClass,
		ReadOnlySpan<IObject?> args = default) where TResult : IDataType<TResult>
		=> JFunctionDefinition<TResult>.StaticInvoke(definition, jClass, args);
}

public partial class JFunctionDefinition<TResult>
{
	/// <summary>
	/// Invokes <paramref name="definition"/> on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="definition">A <see cref="JFunctionDefinition{TResult}"/> definition.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	internal static TResult? Invoke(JFunctionDefinition<TResult> definition, JLocalObject jLocal,
		JClassObject? jClass = default, Boolean nonVirtual = false, ReadOnlySpan<IObject?> args = default)
	{
		IEnvironment env = jLocal.Environment;
		return env.AccessFeature.CallInternalFunction<TResult>(jLocal, jClass ?? jLocal.Class, definition, nonVirtual,
		                                                       args);
	}
	/// <summary>
	/// Invokes <paramref name="definition"/> on <paramref name="jClass"/> which matches with current definition
	/// passing the default value for each argument.
	/// </summary>
	/// <param name="definition">A <see cref="JFunctionDefinition{TResult}"/> definition.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	internal static TResult? StaticInvoke(JFunctionDefinition<TResult> definition, JClassObject jClass,
		ReadOnlySpan<IObject?> args = default)
	{
		IEnvironment env = jClass.Environment;
		return env.AccessFeature.CallInternalStaticFunction<TResult>(jClass, definition, args);
	}

	/// <summary>
	/// Create a <see cref="JFunctionDefinition{TResult}"/> instance for <paramref name="metadata"/>.
	/// </summary>
	/// <param name="functionName">Function name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>A <see cref="JFunctionDefinition{TResult}"/> instance.</returns>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	internal static JFunctionDefinition<TResult> Create(ReadOnlySpan<Byte> functionName,
		ReadOnlySpan<JArgumentMetadata> metadata = default)
		=> metadata.Length > 0 ?
			new JFunctionDefinition<TResult>(functionName, metadata) :
			new Parameterless(functionName);
}