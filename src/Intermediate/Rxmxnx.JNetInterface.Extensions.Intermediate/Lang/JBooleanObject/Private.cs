namespace Rxmxnx.JNetInterface.Lang;

public sealed partial class JBooleanObject
{
	private static readonly JPrimitiveWrapperTypeMetadata<JBooleanObject> typeMetadata =
		new(TypeMetadataBuilder<JBooleanObject>.Build(IPrimitiveType.GetMetadata<JBoolean>()));

	static JPrimitiveWrapperTypeMetadata<JBooleanObject> IPrimitiveWrapperType<JBooleanObject>.Metadata
		=> JBooleanObject.typeMetadata;

	/// <inheritdoc cref="JBooleanObject.Value"/>
	private JBoolean? _value;

	/// <inheritdoc/>
	private JBooleanObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JBooleanObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JBooleanObject(IReferenceType.ObjectInitializer initializer) : base(initializer)
	{
		JBooleanObject? jBooleanObject = initializer.Instance as JBooleanObject;
		this._value = jBooleanObject?._value;
	}

	static JBooleanObject IClassType<JBooleanObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JBooleanObject IClassType<JBooleanObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer.WithClass<JBooleanObject>());
	static JBooleanObject IClassType<JBooleanObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}