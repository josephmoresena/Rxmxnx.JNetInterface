namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// This interface exposes a JNI accessor instance.
/// </summary>
public interface IAccessor
{
	/// <summary>
	/// Retrieves a field from given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of field result.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <returns><typeparamref name="TResult"/> field instance.</returns>
	TResult? GetField<TResult>(JLocalObject jLocal, JFieldDefinition definition) where TResult : IDataType<TResult>;
	/// <summary>
	/// Sets a static field to given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="value">The field value to set to.</param>
	void SetField<TField>(JLocalObject jLocal, JFieldDefinition definition, TField? value)
		where TField : IDataType<TField>;
	/// <summary>
	/// Retrieves a static field from given <see cref="IClass"/> instance.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <returns><typeparamref name="TField"/> field instance.</returns>
	TField? GetStaticField<TField>(IClass jClass, JFieldDefinition definition) where TField : IDataType<TField>;
	/// <summary>
	/// Sets a static field to given <see cref="IClass"/> instance.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="value">The field value to set to.</param>
	void SetStaticField<TField>(IClass jClass, JFieldDefinition definition, TField? value)
		where TField : IDataType<TField>;
	/// <summary>
	/// Invokes a constructor method for given <see cref="IClass"/> instance.
	/// </summary>
	/// <typeparam name="TObject"><see cref="IDataType"/> type of created instance.</typeparam>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns>The new <typeparamref name="TObject"/> instance.</returns>
	TObject CallConstructor<TObject>(IClass jClass, JConstructorDefinition definition, IObject?[] args)
		where TObject : JLocalObject, IDataType<TObject>;
	/// <summary>
	/// Invokes a static function on given <see cref="IClass"/> instance.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	TResult? CallStaticFunction<TResult>(IClass jClass, JFunctionDefinition definition, IObject?[] args)
		where TResult : IDataType<TResult>;
	/// <summary>
	/// Invokes a static method on given <see cref="IClass"/> instance.
	/// </summary>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	void CallStaticMethod(IClass jClass, JMethodDefinition definition, IObject?[] args);
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	TResult? CallFunction<TResult>(JLocalObject jLocal, JFunctionDefinition definition, IObject?[] args)
		where TResult : IDataType<TResult>;
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> and <see cref="IClass"/> instances
	/// and returns its result.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	TResult? CallNonVirtualFunction<TResult>(JLocalObject jLocal, IClass jClass, JFunctionDefinition definition,
		IObject?[] args) where TResult : IDataType<TResult>;
	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	void CallMethod(JLocalObject jLocal, JMethodDefinition definition, IObject?[] args);
	/// <summary>
	/// Invokes a non-virtual method on given <see cref="JLocalObject"/> and <see cref="IClass"/>
	/// instances.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	void CallNonVirtualMethod(JLocalObject jLocal, IClass jClass, JMethodDefinition definition, IObject?[] args);
}