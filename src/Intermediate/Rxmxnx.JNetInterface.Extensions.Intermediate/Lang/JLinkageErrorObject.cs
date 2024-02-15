namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.LinkageError</c> instance.
/// </summary>
public class JLinkageErrorObject : JErrorObject, IThrowableType<JLinkageErrorObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata<JLinkageErrorObject> typeMetadata =
		JTypeMetadataBuilder<JErrorObject>.Create<JLinkageErrorObject>(UnicodeClassNames.LinkageErrorObject()).Build();

	static JThrowableTypeMetadata<JLinkageErrorObject> IThrowableType<JLinkageErrorObject>.Metadata
		=> JLinkageErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JLinkageErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JLinkageErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JLinkageErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JLinkageErrorObject IReferenceType<JLinkageErrorObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JLinkageErrorObject IReferenceType<JLinkageErrorObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JLinkageErrorObject IReferenceType<JLinkageErrorObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}