namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Exception</c> instance.
/// </summary>
public class JExceptionObject : JThrowableObject, IThrowableType<JExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata<JExceptionObject> typeMetadata =
		TypeMetadataBuilder<JThrowableObject>.Create<JExceptionObject>(UnicodeClassNames.ExceptionObject()).Build();

	static JThrowableTypeMetadata<JExceptionObject> IThrowableType<JExceptionObject>.Metadata
		=> JExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JExceptionObject IClassType<JExceptionObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JExceptionObject IClassType<JExceptionObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JExceptionObject IClassType<JExceptionObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}