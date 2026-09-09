namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// This interface exposes JNI accessing feature.
/// </summary>
internal partial interface IAccessFeature
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
		where TField : IDataType<TField>;
	/// <summary>
	/// Retrieves a reflected field on <paramref name="jField"/>.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field result.</typeparam>
	/// <param name="jField">A <see cref="JFieldObject"/> instance.</param>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JFieldDefinition"/> definition.</param>
	/// <returns><typeparamref name="TField"/> field instance.</returns>
	TField? GetField<TField>(JFieldObject jField, JLocalObject jLocal, JFieldDefinition definition)
		where TField : IDataType<TField>, IObject;
	/// <summary>
	/// Sets a field to given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFieldDefinition"/> definition.</param>
	/// <param name="value">The field value to set to.</param>
	void SetField<TField>(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition, TField? value)
		where TField : IDataType<TField>;
	/// <summary>
	/// Sets a reflected field on <paramref name="jField"/>.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="jField">A <see cref="JFieldObject"/> instance.</param>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JFieldDefinition"/> definition.</param>
	/// <param name="value">The field value to set to.</param>
	void SetField<TField>(JFieldObject jField, JLocalObject jLocal, JFieldDefinition definition, TField? value)
		where TField : IDataType<TField>, IObject;
	/// <summary>
	/// Retrieves a static field from given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFieldDefinition"/> definition.</param>
	/// <returns><typeparamref name="TField"/> field instance.</returns>
	TField? GetStaticField<TField>(JClassObject jClass, JFieldDefinition definition) where TField : IDataType<TField>;
	/// <summary>
	/// Retrieves a reflected static field on <paramref name="jField"/>.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="jField">A <see cref="JFieldObject"/> instance.</param>
	/// <param name="definition"><see cref="JFieldDefinition"/> definition.</param>
	/// <returns><typeparamref name="TField"/> field instance.</returns>
	TField? GetStaticField<TField>(JFieldObject jField, JFieldDefinition definition)
		where TField : IDataType<TField>, IObject;
	/// <summary>
	/// Sets a static field to given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFieldDefinition"/> definition.</param>
	/// <param name="value">The field value to set to.</param>
	void SetStaticField<TField>(JClassObject jClass, JFieldDefinition definition, TField? value)
		where TField : IDataType<TField>;
	/// <summary>
	/// Sets a reflected static field on <paramref name="jField"/>.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="jField">A <see cref="JFieldObject"/> instance.</param>
	/// <param name="definition"><see cref="JFieldDefinition"/> definition.</param>
	/// <param name="value">The field value to set to.</param>
	void SetStaticField<TField>(JFieldObject jField, JFieldDefinition definition, TField? value)
		where TField : IDataType<TField>, IObject;
	
	/// <summary>
	/// Register <paramref name="calls"/> as native methods in current class.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="calls">Native calls to register.</param>
	void RegisterNatives(JClassObject jClass, IReadOnlyList<JNativeCallEntry> calls);
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
	/// Retrieves the <see cref="JFieldDefinition"/> instance from <paramref name="fieldType"/>.
	/// </summary>
	/// <param name="memberName">A <see cref="JStringObject"/> instance.</param>
	/// <param name="fieldType">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JFieldDefinition"/> instance.</returns>
	JFieldDefinition GetDefinition(JStringObject memberName, JClassObject fieldType);
	/// <summary>
	/// Retrieves a <see cref="JMethodObject"/> reflected from current definition on
	/// <paramref name="declaringClass"/>.
	/// </summary>
	/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
	/// <param name="declaringClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="isStatic">
	/// Indicates whether <paramref name="definition"/> matches with a static method in <paramref name="declaringClass"/>.
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
	/// Indicates whether <paramref name="definition"/> matches with a static method in <paramref name="declaringClass"/>.
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
	/// <summary>
	/// Retrieves a <see cref="JFieldObject"/> reflected from current definition on
	/// <paramref name="declaringClass"/>.
	/// </summary>
	/// <param name="definition"><see cref="JFieldDefinition"/> definition.</param>
	/// <param name="declaringClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="isStatic">
	/// Indicates whether <paramref name="definition"/> matches with a static field in <paramref name="declaringClass"/>.
	/// </param>
	/// <returns>A <see cref="JFieldObject"/> instance.</returns>
	JFieldObject GetReflectedField(JFieldDefinition definition, JClassObject declaringClass, Boolean isStatic);
	/// <summary>
	/// Retrieves <see cref="JMethodId"/> for <paramref name="jExecutable"/>
	/// </summary>
	/// <param name="jExecutable">A <see cref="JExecutableObject"/> instance.</param>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	JMethodId GetMethodId(JExecutableObject jExecutable);
	/// <summary>
	/// Retrieves <see cref="JFieldId"/> for <paramref name="jField"/>
	/// </summary>
	/// <param name="jField">A <see cref="JFieldObject"/> instance.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	JFieldId GetFieldId(JFieldObject jField);
}