namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.ArrayStoreException</c> instance.
/// </summary>
public class JArrayStoreExceptionObject : JIndexOutOfBoundsExceptionObject, IThrowableType<JArrayStoreExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata<JArrayStoreExceptionObject> typeMetadata =
		JTypeMetadataBuilder<JIndexOutOfBoundsExceptionObject>
			.Create<JArrayStoreExceptionObject>(UnicodeClassNames.ArrayStoreExceptionObject()).Build();

	static JThrowableTypeMetadata<JArrayStoreExceptionObject> IThrowableType<JArrayStoreExceptionObject>.Metadata
		=> JArrayStoreExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JArrayStoreExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JArrayStoreExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JArrayStoreExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JArrayStoreExceptionObject IReferenceType<JArrayStoreExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JArrayStoreExceptionObject IReferenceType<JArrayStoreExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JArrayStoreExceptionObject IReferenceType<JArrayStoreExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}