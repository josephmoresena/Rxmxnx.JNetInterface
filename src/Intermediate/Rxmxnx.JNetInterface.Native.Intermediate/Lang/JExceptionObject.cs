namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Exception</c> instance.
/// </summary>
public class JExceptionObject : JThrowableObject, IThrowableType<JExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata typeMetadata = JTypeMetadataBuilder<JThrowableObject>
	                                                              .Create<JExceptionObject>(
		                                                              UnicodeClassNames.ExceptionObject()).Build();

	static JDataTypeMetadata IDataType.Metadata => JExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JExceptionObject IReferenceType<JExceptionObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JExceptionObject IReferenceType<JExceptionObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JExceptionObject IReferenceType<JExceptionObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}