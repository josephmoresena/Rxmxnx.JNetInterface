namespace Rxmxnx.JNetInterface;

public partial interface IEnvironment
{
	/// <summary>
	/// Retrieves a field from given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <returns><see cref="JLocalObject"/> field instance.</returns>
	JLocalObject? Get(JLocalObject jLocal, JNonTypedFieldDefinition definition) 
		=> this.Accessor.GetField<JLocalObject>(jLocal, definition);
	/// <summary>
	/// Retrieves a field from given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of field result.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <returns><typeparamref name="TResult"/> field instance.</returns>
	TResult? Get<TResult>(JLocalObject jLocal, FieldDefinition<TResult> definition) 
		where TResult : IDataType<TResult>, IObject
		=> this.Accessor.GetField<TResult>(jLocal, definition);
	/// <summary>
	/// Sets a static field to given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="value">The field value to set to.</param>
	void Set(JLocalObject jLocal, JNonTypedFieldDefinition definition, JLocalObject? value) 
		=> this.Accessor.SetField(jLocal, definition, value);
	/// <summary>
	/// Sets a static field to given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JNonTypedFieldDefinition"/> definition.</param>
	/// <param name="value">The field value to set to.</param>
	void Set<TField>(JLocalObject jLocal, FieldDefinition<TField> definition, TField? value) 
		where TField : IDataType<TField>, IObject
		=> this.Accessor.SetField(jLocal, definition, value);
}