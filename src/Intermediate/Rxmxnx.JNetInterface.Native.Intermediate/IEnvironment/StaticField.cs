namespace Rxmxnx.JNetInterface;

public partial interface IEnvironment
{
	/// <summary>
	/// Retrieves a static field from given <see cref="IClass"/> instance.
	/// </summary>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <returns><see cref="JLocalObject"/> field instance.</returns>
	JLocalObject GetStaticField(IClass jClass, JNonTypedFieldDefinition definition) 
		=> this.GetStaticGenericField<JLocalObject>(jClass, definition);
	/// <summary>
	/// Retrieves a static field from given <see cref="IClass"/> instance.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <returns><typeparamref name="TField"/> field instance.</returns>
	TField GetStaticField<TField>(IClass jClass, FieldDefinition<TField> definition) 
		where TField : IDataType<TField>, IObject
		=> this.GetStaticGenericField<TField>(jClass, definition);

	/// <summary>
	/// Retrieves a static field from given <see cref="IClass"/> instance.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <returns><typeparamref name="TField"/> field instance.</returns>
	TField GetStaticGenericField<TField>(IClass jClass, JFieldDefinition definition) 
		where TField : IDataType<TField>;

	/// <summary>
	/// Sets a static field to given <see cref="IClass"/> instance.
	/// </summary>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="value">The field value to set to.</param>
	void SetStaticField(IClass jClass, JNonTypedFieldDefinition definition, JLocalObject value) 
		=> this.SetStaticGenericField(jClass, definition, value);
	/// <summary>
	/// Sets a static field to given <see cref="IClass"/> instance.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="value">The field value to set to.</param>
	void SetStaticField<TField>(IClass jClass, FieldDefinition<TField> definition, TField value) 
		where TField : IDataType<TField>, IObject
		=> this.SetStaticGenericField(jClass, definition, value);

	/// <summary>
	/// Sets a static field to given <see cref="IClass"/> instance.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="value">The field value to set to.</param>
	void SetStaticGenericField<TField>(IClass jClass, JFieldDefinition definition, TField value) 
		where TField : IDataType<TField>;
}