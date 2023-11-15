namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores a primitive function definition.
/// </summary>
internal sealed record JPrimitiveFunctionDefinition : JFunctionDefinition
{
	/// <inheritdoc cref="JPrimitiveFunctionDefinition.Return"/>
	private readonly Type _type;

	/// <inheritdoc/>
	internal override Type Return => this._type;

	/// <inheritdoc/>
	private JPrimitiveFunctionDefinition(CString functionName, JArgumentMetadata[] metadata, CString returnType,
		Type type) : base(functionName, returnType, metadata)
		=> this._type = type;

	/// <summary>
	/// Creates a <c>boolean</c> function definition.
	/// </summary>
	/// <param name="functionName">Method defined name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>A <see cref="JPrimitiveFunctionDefinition"/> instance.</returns>
	public static JPrimitiveFunctionDefinition CreateBooleanDefinition(CString functionName,
		params JArgumentMetadata[] metadata)
		=> new(functionName, metadata, UnicodePrimitiveSignatures.JBooleanSignature, typeof(Byte));
	/// <summary>
	/// Creates a <c>byte</c> function definition.
	/// </summary>
	/// <param name="functionName">Method defined name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>A <see cref="JPrimitiveFunctionDefinition"/> instance.</returns>
	public static JPrimitiveFunctionDefinition CreateByteDefinition(CString functionName,
		params JArgumentMetadata[] metadata)
		=> new(functionName, metadata, UnicodePrimitiveSignatures.JByteSignature, typeof(SByte));
	/// <summary>
	/// Creates a <c>char</c> function definition.
	/// </summary>
	/// <param name="functionName">Method defined name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>A <see cref="JPrimitiveFunctionDefinition"/> instance.</returns>
	public static JPrimitiveFunctionDefinition CreateCharDefinition(CString functionName,
		params JArgumentMetadata[] metadata)
		=> new(functionName, metadata, UnicodePrimitiveSignatures.JCharSignature, typeof(Char));
	/// <summary>
	/// Creates a <c>double</c> function definition.
	/// </summary>
	/// <param name="functionName">Method defined name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>A <see cref="JPrimitiveFunctionDefinition"/> instance.</returns>
	public static JPrimitiveFunctionDefinition CreateDoubleDefinition(CString functionName,
		params JArgumentMetadata[] metadata)
		=> new(functionName, metadata, UnicodePrimitiveSignatures.JDoubleSignature, typeof(Double));
	/// <summary>
	/// Creates a <c>float</c> function definition.
	/// </summary>
	/// <param name="functionName">Method defined name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>A <see cref="JPrimitiveFunctionDefinition"/> instance.</returns>
	public static JPrimitiveFunctionDefinition CreateFloatDefinition(CString functionName,
		params JArgumentMetadata[] metadata)
		=> new(functionName, metadata, UnicodePrimitiveSignatures.JFloatSignature, typeof(Single));
	/// <summary>
	/// Creates a <c>int</c> function definition.
	/// </summary>
	/// <param name="functionName">Method defined name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>A <see cref="JPrimitiveFunctionDefinition"/> instance.</returns>
	public static JPrimitiveFunctionDefinition CreateIntDefinition(CString functionName,
		params JArgumentMetadata[] metadata)
		=> new(functionName, metadata, UnicodePrimitiveSignatures.JIntSignature, typeof(Int32));
	/// <summary>
	/// Creates a <c>long</c> function definition.
	/// </summary>
	/// <param name="functionName">Method defined name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>A <see cref="JPrimitiveFunctionDefinition"/> instance.</returns>
	public static JPrimitiveFunctionDefinition CreateLongDefinition(CString functionName,
		params JArgumentMetadata[] metadata)
		=> new(functionName, metadata, UnicodePrimitiveSignatures.JLongSignature, typeof(Int64));
	/// <summary>
	/// Creates a <c>short</c> function definition.
	/// </summary>
	/// <param name="functionName">Method defined name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>A <see cref="JPrimitiveFunctionDefinition"/> instance.</returns>
	public static JPrimitiveFunctionDefinition CreateShortDefinition(CString functionName,
		params JArgumentMetadata[] metadata)
		=> new(functionName, metadata, UnicodePrimitiveSignatures.JShortSignature, typeof(Int16));

	/// <summary>
	/// Invokes <paramref name="definition"/> on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="definition">A <see cref="JPrimitiveFunctionDefinition"/> definition.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	internal static TResult Invoke<TResult>(JPrimitiveFunctionDefinition definition, JLocalObject jLocal,
		IObject?[]? args = default) where TResult : unmanaged
	{
		IEnvironment env = jLocal.Environment;
		Span<TResult> result = stackalloc TResult[1];
		env.AccessProvider.CallPrimitiveFunction(result.AsBytes(), jLocal, definition,
		                                         args ?? definition.CreateArgumentsArray());
		return result[0];
	}
	/// <summary>
	/// Invokes <paramref name="definition"/> on <paramref name="jLocal"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jClass"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JPrimitiveFunctionDefinition"/> definition.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="args">The arguments to pass to.</param>
	internal static TResult Invoke<TResult>(JPrimitiveFunctionDefinition definition, JLocalObject jLocal,
		JClassObject jClass, IObject?[]? args = default) where TResult : unmanaged
	{
		IEnvironment env = jLocal.Environment;
		Span<TResult> result = stackalloc TResult[1];
		env.AccessProvider.CallPrimitiveNonVirtualFunction(result.AsBytes(), jLocal, jClass, definition,
		                                                   args ?? definition.CreateArgumentsArray());
		return result[0];
	}
	/// <summary>
	/// Invokes <paramref name="definition"/> on <paramref name="jClass"/> which matches with current definition
	/// passing the default value for each argument.
	/// </summary>
	/// <param name="definition">A <see cref="JPrimitiveFunctionDefinition"/> definition.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	internal static TResult StaticInvoke<TResult>(JPrimitiveFunctionDefinition definition, JClassObject jClass,
		IObject?[]? args = default) where TResult : unmanaged
	{
		IEnvironment env = jClass.Environment;
		Span<TResult> result = stackalloc TResult[1];
		env.AccessProvider.CallPrimitiveStaticFunction(result.AsBytes(), jClass, definition,
		                                               args ?? definition.CreateArgumentsArray());
		return result[0];
	}
}