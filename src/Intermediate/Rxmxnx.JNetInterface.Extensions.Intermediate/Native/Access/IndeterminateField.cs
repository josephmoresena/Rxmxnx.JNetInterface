namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores the definition of an indeterminate java field.
/// </summary>
public sealed partial class IndeterminateField : IWrapper<JFieldDefinition>
{
	/// <summary>
	/// Field type signature.
	/// </summary>
	public CString FieldType { get; }
	/// <summary>
	/// Internal field definition.
	/// </summary>
	public JFieldDefinition Definition { get; }

#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	JFieldDefinition IWrapper<JFieldDefinition>.Value => this.Definition;

	/// <summary>
	/// Creates a <see cref="IndeterminateField"/> instance for a java field.
	/// </summary>
	/// <param name="returnType">Return type metadata.</param>
	/// <param name="fieldName">UTF-8 field name.</param>
	/// <returns>A new <see cref="IndeterminateField"/> instance.</returns>
	public static IndeterminateField Create(JArgumentMetadata returnType, ReadOnlySpan<Byte> fieldName)
	{
		JFieldDefinition definition = returnType.Signature.Length == 1 ?
			IndeterminateField.CreatePrimitiveField(fieldName, returnType.Signature) :
			new JNonTypedFieldDefinition(fieldName, returnType.Signature);
		return new(definition, returnType.Signature);
	}
	/// <summary>
	/// Creates a <see cref="IndeterminateField"/> instance for a java field.
	/// </summary>
	/// <typeparam name="TResult">Return <see cref="IDataType{TResult}"/> type.</typeparam>
	/// <param name="fieldName">UTF-8 field name.</param>
	/// <returns>A new <see cref="IndeterminateField"/> instance.</returns>
	public static IndeterminateField Create<TResult>(ReadOnlySpan<Byte> fieldName) where TResult : IDataType<TResult>
	{
		JDataTypeMetadata typeMetadata = IDataType.GetMetadata<TResult>();
		JFieldDefinition definition = typeMetadata is not JReferenceTypeMetadata referenceTypeMetadata ?
			IndeterminateField.CreatePrimitiveField(fieldName, typeMetadata.Signature) :
			referenceTypeMetadata.CreateFieldDefinition(fieldName);
		return new(definition, typeMetadata.Signature);
	}
}