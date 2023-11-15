namespace Rxmxnx.JNetInterface.Restricted;

public partial interface IAccessProvider
{
	/// <summary>
	/// Invokes a constructor method for given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TObject"><see cref="IDataType"/> type of created instance.</typeparam>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JConstructorDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns>The new <typeparamref name="TObject"/> instance.</returns>
	internal TObject CallInternalConstructor<TObject>(JClassObject jClass, JConstructorDefinition definition,
		IObject?[] args) where TObject : JLocalObject, IDataType<TObject>
		=> this.CallConstructor<TObject>(jClass, definition, args);
	/// <summary>
	/// Invokes a static function on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	internal TResult? CallInternalStaticFunction<TResult>(JClassObject jClass, JFunctionDefinition definition,
		IObject?[] args) where TResult : IDataType<TResult>
		=> this.CallStaticFunction<TResult>(jClass, definition, args);
	/// <summary>
	/// Invokes a static method on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	internal void CallInternalStaticMethod(JClassObject jClass, JMethodDefinition definition, IObject?[] args)
		=> this.CallStaticMethod(jClass, definition, args);
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	internal TResult? CallInternalFunction<TResult>(JLocalObject jLocal, JFunctionDefinition definition,
		IObject?[] args) where TResult : IDataType<TResult>
		=> this.CallFunction<TResult>(jLocal, definition, args);
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> and <see cref="JClassObject"/> instances
	/// and returns its result.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	internal TResult? CallInternalNonVirtualFunction<TResult>(JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, IObject?[] args) where TResult : IDataType<TResult>
		=> this.CallNonVirtualFunction<TResult>(jLocal, jClass, definition, args);
	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	internal void CallInternalMethod(JLocalObject jLocal, JMethodDefinition definition, IObject?[] args)
		=> this.CallMethod(jLocal, definition, args);
	/// <summary>
	/// Invokes a non-virtual method on given <see cref="JLocalObject"/> and <see cref="JClassObject"/>
	/// instances.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	internal void CallInternalNonVirtualMethod(JLocalObject jLocal, JClassObject jClass, JMethodDefinition definition,
		IObject?[] args)
		=> this.CallNonVirtualMethod(jLocal, jClass, definition, args);

	/// <summary>
	/// Retrieves a primitive field from given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="bytes">Binary span to hold result.</param>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JFieldDefinition"/> definition.</param>
	internal void GetPrimitiveField(Span<Byte> bytes, JLocalObject jLocal, JFieldDefinition definition);
	/// <summary>
	/// Sets a primitive static field to given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JFieldDefinition"/> definition.</param>
	/// <param name="bytes">Binary span containing value to set to.</param>
	internal void SetPrimitiveField(JLocalObject jLocal, JFieldDefinition definition, ReadOnlySpan<Byte> bytes);
	/// <summary>
	/// Retrieves a primitive static field from given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <param name="bytes">Binary span to hold result.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFieldDefinition"/> definition.</param>
	internal void GetPrimitiveStaticField(Span<Byte> bytes, JClassObject jClass, JFieldDefinition definition);
	/// <summary>
	/// Sets a primitive static field to given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFieldDefinition"/> definition.</param>
	/// <param name="bytes">Binary span containing value to set to.</param>
	internal void SetPrimitiveStaticField(JClassObject jClass, JFieldDefinition definition, ReadOnlySpan<Byte> bytes);
	/// <summary>
	/// Invokes a primitive static function on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <param name="bytes">Binary span to hold result.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	internal void CallPrimitiveStaticFunction(Span<Byte> bytes, JClassObject jClass, JFunctionDefinition definition,
		IObject?[] args);
	/// <summary>
	/// Invokes a primitive function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <param name="bytes">Binary span to hold result.</param>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	internal void CallPrimitiveFunction(Span<Byte> bytes, JLocalObject jLocal, JFunctionDefinition definition,
		IObject?[] args);
	/// <summary>
	/// Invokes a primitive function on given <see cref="JLocalObject"/> and <see cref="JClassObject"/> instances
	/// and returns its result.
	/// </summary>
	/// <param name="bytes">Binary span to hold result.</param>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	internal void CallPrimitiveNonVirtualFunction(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, IObject?[] args);
}