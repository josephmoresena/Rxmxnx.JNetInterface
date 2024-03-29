namespace Rxmxnx.JNetInterface.Restricted;

public partial interface IAccessFeature
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
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	internal TResult? CallInternalFunction<TResult>(JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, Boolean nonVirtual, IObject?[] args) where TResult : IDataType<TResult>
		=> this.CallFunction<TResult>(jLocal, jClass, definition, nonVirtual, args);
	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	internal void CallInternalMethod(JLocalObject jLocal, JClassObject jClass, JMethodDefinition definition,
		Boolean nonVirtual, IObject?[] args)
		=> this.CallMethod(jLocal, jClass, definition, nonVirtual, args);

	/// <summary>
	/// Retrieves a primitive field from given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="bytes">Binary span to hold result.</param>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFieldDefinition"/> definition.</param>
	internal void GetPrimitiveField(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
		JFieldDefinition definition);
	/// <summary>
	/// Sets a primitive static field to given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFieldDefinition"/> definition.</param>
	/// <param name="bytes">Binary span containing value to set to.</param>
	internal void SetPrimitiveField(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition,
		ReadOnlySpan<Byte> bytes);
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
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	internal void CallPrimitiveFunction(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, Boolean nonVirtual, IObject?[] args);
	/// <summary>
	/// Retrieves <see cref="JMethodId"/> for <paramref name="jExecutable"/>
	/// </summary>
	/// <param name="jExecutable">A <see cref="JExecutableObject"/> instance.</param>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	internal JMethodId GetMethodId(JExecutableObject jExecutable);
	/// <summary>
	/// Retrieves <see cref="JFieldId"/> for <paramref name="jField"/>
	/// </summary>
	/// <param name="jField">A <see cref="JFieldObject"/> instance.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	internal JFieldId GetFieldId(JFieldObject jField);
}