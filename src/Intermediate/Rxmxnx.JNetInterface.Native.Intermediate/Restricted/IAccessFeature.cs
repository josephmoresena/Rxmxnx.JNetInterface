namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// This interface exposes JNI accessing feature.
/// </summary>
public partial interface IAccessFeature
{
	/// <summary>
	/// Retrieves a field from given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field result.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFieldDefinition"/> definition.</param>
	/// <returns><typeparamref name="TField"/> field instance.</returns>
	TField? GetField<TField>(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition)
		where TField : IObject, IDataType<TField>;
	/// <summary>
	/// Sets a static field to given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFieldDefinition"/> definition.</param>
	/// <param name="value">The field value to set to.</param>
	void SetField<TField>(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition, TField? value)
		where TField : IObject, IDataType<TField>;
	/// <summary>
	/// Retrieves a static field from given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFieldDefinition"/> definition.</param>
	/// <returns><typeparamref name="TField"/> field instance.</returns>
	TField? GetStaticField<TField>(JClassObject jClass, JFieldDefinition definition)
		where TField : IObject, IDataType<TField>;
	/// <summary>
	/// Sets a static field to given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFieldDefinition"/> definition.</param>
	/// <param name="value">The field value to set to.</param>
	void SetStaticField<TField>(JClassObject jClass, JFieldDefinition definition, TField? value)
		where TField : IObject, IDataType<TField>;
	/// <summary>
	/// Invokes a constructor method for given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TObject"><see cref="IDataType"/> type of created instance.</typeparam>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JConstructorDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns>The new <typeparamref name="TObject"/> instance.</returns>
	TObject CallConstructor<TObject>(JClassObject jClass, JConstructorDefinition definition, IObject?[] args)
		where TObject : JLocalObject, IDataType<TObject>;
	/// <summary>
	/// Invokes a reflected constructor method on <paramref name="jMethod"/>.
	/// </summary>
	/// <typeparam name="TObject"><see cref="IDataType"/> type of created instance.</typeparam>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="definition"><see cref="JConstructorDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns>The new <typeparamref name="TObject"/> instance.</returns>
	TObject CallConstructor<TObject>(JMethodObject jMethod, JConstructorDefinition definition, IObject?[] args)
		where TObject : JLocalObject, IClassType<TObject>;
	/// <summary>
	/// Invokes a static function on <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	TResult? CallStaticFunction<TResult>(JClassObject jClass, JFunctionDefinition definition, IObject?[] args)
		where TResult : IDataType<TResult>;
	/// <summary>
	/// Invokes a static function reflected on <paramref name="jMethod"/> instance.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	TResult? CallStaticFunction<TResult>(JMethodObject jMethod, JFunctionDefinition<TResult> definition,
		IObject?[] args) where TResult : IDataType<TResult>;
	/// <summary>
	/// Invokes a static method on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	void CallStaticMethod(JClassObject jClass, JMethodDefinition definition, IObject?[] args);
	/// <summary>
	/// Invokes an static method reflected on <paramref name="jMethod"/>.
	/// </summary>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	void CallStaticMethod(JMethodObject jMethod, JMethodDefinition definition, IObject?[] args);
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
	TResult? CallFunction<TResult>(JLocalObject jLocal, JClassObject jClass, JFunctionDefinition definition,
		Boolean nonVirtual, IObject?[] args) where TResult : IDataType<TResult>;
	/// <summary>
	/// Invokes a function reflected on <paramref name="jMethod"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	TResult? CallFunction<TResult>(JMethodObject jMethod, JLocalObject jLocal, JFunctionDefinition<TResult> definition,
		Boolean nonVirtual, IObject?[] args) where TResult : IDataType<TResult>;
	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	void CallMethod(JLocalObject jLocal, JClassObject jClass, JMethodDefinition definition, Boolean nonVirtual,
		IObject?[] args);
	/// <summary>
	/// Invokes a method reflected on <paramref name="jMethod"/>.
	/// </summary>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	void CallMethod(JMethodObject jMethod, JLocalObject jLocal, JMethodDefinition definition, Boolean nonVirtual,
		IObject?[] args);
	/// <summary>
	/// Register <paramref name="calls"/> as native methods in current class.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="calls">Native calls to register.</param>
	void RegisterNatives(JClassObject jClass, IReadOnlyList<JNativeCall> calls);
	/// <summary>
	/// Clears native method registration.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	void ClearNatives(JClassObject jClass);

	/// <summary>
	/// Retrieves the <see cref="JCallDefinition"/> instance from <paramref name="parameterTypes"/> and
	/// <paramref name="returnType"/>.
	/// </summary>
	/// <param name="memberName">A <see cref="JStringObject"/> instance.</param>
	/// <param name="parameterTypes">A <see cref="JClassObject"/> array.</param>
	/// <param name="returnType">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JCallDefinition"/> instance.</returns>
	JCallDefinition GetDefinition(JStringObject memberName, JArrayObject<JClassObject> parameterTypes,
		JClassObject? returnType);
	/// <summary>
	/// Retrieves a <see cref="JMethodObject"/> reflected from current definition on
	/// <paramref name="declaringClass"/>.
	/// </summary>
	/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
	/// <param name="declaringClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="isStatic">
	/// Indicates whether <paramref name="definition"/> matches with an static method in <paramref name="declaringClass"/>.
	/// </param>
	/// <returns>A <see cref="JMethodObject"/> instance.</returns>
	JMethodObject GetReflectedMethod(JMethodDefinition definition, JClassObject declaringClass, Boolean isStatic);
	/// <summary>
	/// Retrieves a <see cref="JMethodObject"/> reflected from current definition on
	/// <paramref name="declaringClass"/>.
	/// </summary>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="declaringClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="isStatic">
	/// Indicates whether <paramref name="definition"/> matches with an static method in <paramref name="declaringClass"/>.
	/// </param>
	/// <returns>A <see cref="JMethodObject"/> instance.</returns>
	JMethodObject GetReflectedFunction(JFunctionDefinition definition, JClassObject declaringClass, Boolean isStatic);
	/// <summary>
	/// Retrieves a <see cref="JConstructorObject"/> reflected from current definition on
	/// <paramref name="declaringClass"/>.
	/// </summary>
	/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
	/// <param name="declaringClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JConstructorObject"/> instance.</returns>
	JConstructorObject GetReflectedConstructor(JConstructorDefinition definition, JClassObject declaringClass);
}