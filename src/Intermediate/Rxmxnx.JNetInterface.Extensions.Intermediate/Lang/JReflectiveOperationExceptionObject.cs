namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.ReflectiveOperationException</c> instance.
/// </summary>
public class JReflectiveOperationExceptionObject : JExceptionObject, IThrowableType<JReflectiveOperationExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata<JReflectiveOperationExceptionObject> typeMetadata =
		TypeMetadataBuilder<JExceptionObject>
			.Create<JReflectiveOperationExceptionObject>(UnicodeClassNames.ReflectiveOperationExceptionObject())
			.Build();

	static JThrowableTypeMetadata<JReflectiveOperationExceptionObject>
		IThrowableType<JReflectiveOperationExceptionObject>.Metadata
		=> JReflectiveOperationExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JReflectiveOperationExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JReflectiveOperationExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JReflectiveOperationExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JReflectiveOperationExceptionObject IReferenceType<JReflectiveOperationExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JReflectiveOperationExceptionObject IReferenceType<JReflectiveOperationExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JReflectiveOperationExceptionObject IReferenceType<JReflectiveOperationExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}