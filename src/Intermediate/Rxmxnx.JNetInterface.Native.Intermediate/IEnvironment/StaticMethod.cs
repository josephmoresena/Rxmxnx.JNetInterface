namespace Rxmxnx.JNetInterface;

public partial interface IEnvironment
{
	/// <summary>
	/// Invokes a constructor method for given <see cref="IClass"/> instance.
	/// </summary>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns><see cref="JLocalObject"/> function result.</returns>
	JLocalObject New(IClass jClass, JConstructorDefinition definition, IObject?[] args)
		=> this.Accessor.CallConstructor<JLocalObject>(jClass, definition, args);
	/// <summary>
	/// Invokes a constructor method for given <see cref="IClass"/> instance.
	/// </summary>
	/// <typeparam name="TObject"><see cref="IDataType"/> type of created instance.</typeparam>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns>The new <typeparamref name="TObject"/> instance.</returns>
	TObject New<TObject>(JConstructorDefinition definition, IObject?[] args)
		where TObject : JLocalObject, IDataType<TObject>
		=> this.Accessor.CallConstructor<TObject>(this.GetObjectClass<TObject>(), definition, args);
	/// <summary>
	/// Invokes a static method on given <see cref="IClass"/> instance.
	/// </summary>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	void StaticInvoke(IClass jClass, JMethodDefinition definition, IObject?[] args)
		=> this.Accessor.CallStaticMethod(jClass, definition, args);
   /// <summary>
   /// Invokes a static function on given <see cref="IClass"/> instance.
   /// </summary>
   /// <param name="jClass"><see cref="IClass"/> instance.</param>
   /// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
   /// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
   /// <returns><see cref="JLocalObject"/> function result.</returns>
   JLocalObject? StaticInvoke(IClass jClass, JNonTypedFunctionDefinition definition, IObject?[] args)
		=> this.Accessor.CallStaticFunction<JLocalObject>(jClass, definition, args);
	/// <summary>
	/// Invokes a static function on given <see cref="IClass"/> instance.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	TResult? StaticInvoke<TResult>(IClass jClass, JFunctionDefinition<TResult> definition, IObject?[] args)
		where TResult : IDataType<TResult>, IObject
		=> this.Accessor.CallStaticFunction<TResult>(jClass, definition, args);
}