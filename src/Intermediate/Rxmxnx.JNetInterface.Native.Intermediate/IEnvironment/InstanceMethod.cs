namespace Rxmxnx.JNetInterface;

public partial interface IEnvironment
{
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args"><see cref="IFixedMemory"/> with arguments information.</param>
	/// <returns><see cref="JLocalObject"/> function result.</returns>
	JLocalObject CallFunction(JLocalObject jLocal, JNonTypedFunctionDefinition definition, IFixedMemory args)
		=> this.CallGenericFunction<JLocalObject>(jLocal, definition, args);
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args"><see cref="IFixedMemory"/> with arguments information.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	TResult CallFunction<TResult>(JLocalObject jLocal, JFunctionDefinition<TResult> definition, IFixedMemory args)
		where TResult : IDataType<TResult>, IObject
		=> this.CallGenericFunction<TResult>(jLocal, definition, args);

	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args"><see cref="IFixedMemory"/> with arguments information.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	TResult CallGenericFunction<TResult>(JLocalObject jLocal, JFunctionDefinition definition, IFixedMemory args)
		where TResult : IDataType<TResult>;

	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> and <see cref="IClass"/> instances 
	/// and returns its result.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <returns><see cref="JLocalObject"/> function result.</returns>
	JLocalObject CallNonVirtualFunction(JLocalObject jLocal, IClass jClass, JNonTypedFieldDefinition definition) 
		=> this.CallNonVirtualGenericFunction<JLocalObject>(jLocal, jClass, definition);
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> and <see cref="IClass"/> instances 
	/// and returns its result.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	TResult CallNonVirtualFunction<TResult>(JLocalObject jLocal, IClass jClass, FieldDefinition<TResult> definition) 
		where TResult : IDataType<TResult>, IObject
		=> this.CallNonVirtualGenericFunction<TResult>(jLocal, jClass, definition);
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> and <see cref="IClass"/> instances 
	/// and returns its result.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	TResult CallNonVirtualGenericFunction<TResult>(JLocalObject jLocal, IClass jClass, JFieldDefinition definition) 
		where TResult : IDataType<TResult>;

	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args"><see cref="IFixedMemory"/> with arguments information.</param>
	void CallMethod(JLocalObject jLocal, JMethodDefinition definition, IFixedMemory args);

	/// <summary>
	/// Invokes a non-virtual method on given <see cref="JLocalObject"/> and <see cref="IClass"/>
	/// instances.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args"><see cref="IFixedMemory"/> with arguments information.</param>
	void CallNonVirtualMethod(JLocalObject jLocal, IClass jClass, JMethodDefinition definition, IFixedMemory args); 
}