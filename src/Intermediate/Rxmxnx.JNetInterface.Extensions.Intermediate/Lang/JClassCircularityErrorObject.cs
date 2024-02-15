namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.ClassCircularityError</c> instance.
/// </summary>
public class JClassCircularityErrorObject : JLinkageErrorObject, IThrowableType<JClassCircularityErrorObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata<JClassCircularityErrorObject> typeMetadata =
		JTypeMetadataBuilder<JLinkageErrorObject>
			.Create<JClassCircularityErrorObject>(UnicodeClassNames.ClassCircularityErrorObject()).Build();

	static JThrowableTypeMetadata<JClassCircularityErrorObject> IThrowableType<JClassCircularityErrorObject>.Metadata
		=> JClassCircularityErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JClassCircularityErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassCircularityErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassCircularityErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JClassCircularityErrorObject IReferenceType<JClassCircularityErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JClassCircularityErrorObject IReferenceType<JClassCircularityErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JClassCircularityErrorObject IReferenceType<JClassCircularityErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}