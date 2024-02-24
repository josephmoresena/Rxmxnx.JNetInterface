namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.RuntimeException</c> instance.
/// </summary>
public class JRuntimeExceptionObject : JExceptionObject, IThrowableType<JRuntimeExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata<JRuntimeExceptionObject> typeMetadata =
		TypeMetadataBuilder<JExceptionObject>
			.Create<JRuntimeExceptionObject>(UnicodeClassNames.RuntimeExceptionObject()).Build();

	static JThrowableTypeMetadata<JRuntimeExceptionObject> IThrowableType<JRuntimeExceptionObject>.Metadata
		=> JRuntimeExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JRuntimeExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JRuntimeExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JRuntimeExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JRuntimeExceptionObject IReferenceType<JRuntimeExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JRuntimeExceptionObject IReferenceType<JRuntimeExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JRuntimeExceptionObject IReferenceType<JRuntimeExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}