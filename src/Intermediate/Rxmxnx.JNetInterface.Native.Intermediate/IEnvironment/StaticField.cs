namespace Rxmxnx.JNetInterface;

public partial interface IEnvironment
{
	/// <summary>
	/// Retrieves a static field from given <see cref="IClass"/> instance.
	/// </summary>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <returns><see cref="JLocalObject"/> field instance.</returns>
	JLocalObject? StaticGet(IClass jClass, JNonTypedFieldDefinition definition) 
		=> this.Accessor.GetStaticField<JLocalObject>(jClass, definition);
	/// <summary>
	/// Retrieves a static field from given <see cref="IClass"/> instance.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <returns><typeparamref name="TField"/> field instance.</returns>
	TField? StaticGet<TField>(IClass jClass, FieldDefinition<TField> definition) 
		where TField : IDataType<TField>, IObject
		=> this.Accessor.GetStaticField<TField>(jClass, definition);
	/// <summary>
	/// Sets a static field to given <see cref="IClass"/> instance.
	/// </summary>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="value">The field value to set to.</param>
	void StaticSet(IClass jClass, JNonTypedFieldDefinition definition, JLocalObject? value) 
		=> this.Accessor.SetStaticField(jClass, definition, value);
	/// <summary>
	/// Sets a static field to given <see cref="IClass"/> instance.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="value">The field value to set to.</param>
	void StaticSet<TField>(IClass jClass, FieldDefinition<TField> definition, TField? value) 
		where TField : IDataType<TField>, IObject
		=> this.Accessor.SetStaticField(jClass, definition, value);
}