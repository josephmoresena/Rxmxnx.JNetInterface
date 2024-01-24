namespace Rxmxnx.JNetInterface.Lang;

public sealed partial class JBooleanObject
{
	static JPrimitiveTypeMetadata IPrimitiveWrapperType.PrimitiveMetadata => IPrimitiveType.GetMetadata<JBoolean>();
	static JDataTypeMetadata IDataType.Metadata => new JPrimitiveWrapperTypeMetadata<JBooleanObject>();

	/// <inheritdoc cref="JBooleanObject.Value"/>
	private JBoolean? _value;

	/// <inheritdoc/>
	private JBooleanObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JBooleanObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JBooleanObject(IReferenceType.ObjectInitializer initializer) : base(initializer)
	{
		JLocalObject jLocal = initializer.Instance;
		if (jLocal is JBooleanObject wrapper)
			this._value = wrapper._value;
	}

	static JBooleanObject IReferenceType<JBooleanObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JBooleanObject IReferenceType<JBooleanObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer.WithClass<JBooleanObject>());
	static JBooleanObject IReferenceType<JBooleanObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}