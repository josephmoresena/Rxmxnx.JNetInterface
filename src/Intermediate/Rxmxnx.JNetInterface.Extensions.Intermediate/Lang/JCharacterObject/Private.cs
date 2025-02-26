namespace Rxmxnx.JNetInterface.Lang;

public sealed partial class JCharacterObject
{
	private static readonly JPrimitiveWrapperTypeMetadata<JCharacterObject> typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JCharacterObject>(IPrimitiveType.GetMetadata<JChar>()));

	static JPrimitiveWrapperTypeMetadata<JCharacterObject> IPrimitiveWrapperType<JCharacterObject>.Metadata
		=> JCharacterObject.typeMetadata;

	/// <inheritdoc cref="JCharacterObject.Value"/>
	private JChar? _value;

	/// <inheritdoc/>
	private JCharacterObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JCharacterObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JCharacterObject(IReferenceType.ObjectInitializer initializer) : base(initializer)
	{
		JCharacterObject? jBooleanObject = initializer.Instance as JCharacterObject;
		this._value = jBooleanObject?._value;
	}

	[ExcludeFromCodeCoverage]
	void IPrimitiveWrapperType<JCharacterObject>.SetPrimitiveValue(IPrimitiveType value)
		=> this._value = value.ToChar(CultureInfo.InvariantCulture);

	static JCharacterObject IClassType<JCharacterObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JCharacterObject IClassType<JCharacterObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer.WithClass<JCharacterObject>());
	static JCharacterObject IClassType<JCharacterObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}