namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.InstantiationException</c> instance.
/// </summary>
public class JInstantiationExceptionObject : JReflectiveOperationExceptionObject,
	IThrowableType<JInstantiationExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata<JInstantiationExceptionObject> typeMetadata =
		JTypeMetadataBuilder<JReflectiveOperationExceptionObject>
			.Create<JInstantiationExceptionObject>(UnicodeClassNames.InstantiationExceptionObject()).Build();

	static JThrowableTypeMetadata<JInstantiationExceptionObject> IThrowableType<JInstantiationExceptionObject>.Metadata
		=> JInstantiationExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JInstantiationExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JInstantiationExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JInstantiationExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JInstantiationExceptionObject IReferenceType<JInstantiationExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JInstantiationExceptionObject IReferenceType<JInstantiationExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JInstantiationExceptionObject IReferenceType<JInstantiationExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}