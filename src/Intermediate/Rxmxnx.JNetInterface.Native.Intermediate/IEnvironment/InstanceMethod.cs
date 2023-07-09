namespace Rxmxnx.JNetInterface;

public partial interface IEnvironment
{
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFunctionDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns><see cref="JLocalObject"/> function result.</returns>
	JLocalObject? Invoke(JLocalObject jLocal, JNonTypedFunctionDefinition definition, IObject?[] args)
		=> this.Accessor.CallFunction<JLocalObject>(jLocal, definition, args);
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition{TResult}"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	TResult? Invoke<TResult>(JLocalObject jLocal, JFunctionDefinition<TResult> definition, IObject?[] args)
		where TResult : IDataType<TResult>, IObject
		=> this.Accessor.CallFunction<TResult>(jLocal, definition, args);
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> and <see cref="IClass"/> instances 
	/// and returns its result.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFunctionDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns><see cref="JLocalObject"/> function result.</returns>
	JLocalObject? Invoke(JLocalObject jLocal, IClass jClass, JNonTypedFunctionDefinition definition, IObject?[] args) 
		=> this.Accessor.CallNonVirtualFunction<JLocalObject>(jLocal, jClass, definition, args);
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> and <see cref="IClass"/> instances 
	/// and returns its result.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition{TResult}"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	TResult? Invoke<TResult>(JLocalObject jLocal, IClass jClass, JFunctionDefinition<TResult> definition, IObject?[] args) 
		where TResult : IDataType<TResult>, IObject
		=> this.Accessor.CallNonVirtualFunction<TResult>(jLocal, jClass, definition, args);
	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	void Invoke(JLocalObject jLocal, JMethodDefinition definition, IObject?[] args)
		=> this.Accessor.CallMethod(jLocal, definition, args);
	/// <summary>
	/// Invokes a non-virtual method on given <see cref="JLocalObject"/> and <see cref="IClass"/>
	/// instances.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	void Invoke(JLocalObject jLocal, IClass jClass, JMethodDefinition definition, IObject?[] args)
		=> this.Accessor.CallNonVirtualMethod(jLocal, jClass, definition, args);
}