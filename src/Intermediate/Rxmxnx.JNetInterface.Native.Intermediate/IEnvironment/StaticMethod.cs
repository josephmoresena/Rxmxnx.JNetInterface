namespace Rxmxnx.JNetInterface;

public partial interface IEnvironment
{
	/// <summary>
	/// Invokes a constructor method for given <see cref="IClass"/> instance.
	/// </summary>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args"><see cref="IFixedMemory"/> with arguments information.</param>
	JLocalObject CallConstructor(IClass jClass, JConstructorDefinition definition, IFixedMemory args)
		=> this.CallConstructor<JLocalObject>(jClass, definition, args);
	/// <summary>
	/// Invokes a constructor method for given <see cref="IClass"/> instance.
	/// </summary>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args"><see cref="IFixedMemory"/> with arguments information.</param>
	TObject CallConstructor<TObject>(IClass jClass, JConstructorDefinition definition, IFixedMemory args)
		where TObject : JLocalObject, IDataType<TObject>;

	/// <summary>
	/// Invokes a static function on given <see cref="IClass"/> instance.
	/// </summary>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args"><see cref="IFixedMemory"/> with arguments information.</param>
	JLocalObject CallStaticFunction(IClass jClass, JNonTypedFunctionDefinition definition, IFixedMemory args)
		=> this.CallStaticGenericFunction<JLocalObject>(jClass, definition, args);
	/// <summary>
	/// Invokes a static function on given <see cref="IClass"/> instance.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args"><see cref="IFixedMemory"/> with arguments information.</param>
	TResult CallStaticFunction<TResult>(IClass jClass, JFunctionDefinition<TResult> definition, IFixedMemory args)
		where TResult : IDataType<TResult>, IObject
		=> this.CallStaticGenericFunction<TResult>(jClass, definition, args);

	/// <summary>
	/// Invokes a static function on given <see cref="IClass"/> instance.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args"><see cref="IFixedMemory"/> with arguments information.</param>
	TResult CallStaticGenericFunction<TResult>(IClass jClass, JFunctionDefinition definition, IFixedMemory args)
		where TResult : IDataType<TResult>;

	/// <summary>
	/// Invokes a static method on given <see cref="IClass"/> instance.
	/// </summary>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args"><see cref="IFixedMemory"/> with arguments information.</param>
	void CallStaticMethod(IClass jClass, JMethodDefinition definition, IFixedMemory args);
}