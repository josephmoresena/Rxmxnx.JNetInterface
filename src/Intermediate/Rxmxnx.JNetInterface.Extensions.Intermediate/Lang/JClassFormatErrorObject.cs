namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.ClassFormatError</c> instance.
/// </summary>
public class JClassFormatErrorObject : JLinkageErrorObject, IThrowableType<JClassFormatErrorObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata<JClassFormatErrorObject> typeMetadata =
		JTypeMetadataBuilder<JLinkageErrorObject>
			.Create<JClassFormatErrorObject>(UnicodeClassNames.ClassFormatErrorObject()).Build();

	static JThrowableTypeMetadata<JClassFormatErrorObject> IThrowableType<JClassFormatErrorObject>.Metadata
		=> JClassFormatErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JClassFormatErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassFormatErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassFormatErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JClassFormatErrorObject IReferenceType<JClassFormatErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JClassFormatErrorObject IReferenceType<JClassFormatErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JClassFormatErrorObject IReferenceType<JClassFormatErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}