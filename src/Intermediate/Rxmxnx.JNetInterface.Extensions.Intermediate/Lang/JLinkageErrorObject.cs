namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JLinkageErrorObject>;

/// <summary>
/// This class represents a local <c>java.lang.LinkageError</c> instance.
/// </summary>
public class JLinkageErrorObject : JErrorObject, IThrowableType<JLinkageErrorObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JErrorObject>
	                                                    .Create<JLinkageErrorObject>("java/lang/LinkageError"u8)
	                                                    .Build();

	static TypeMetadata IThrowableType<JLinkageErrorObject>.Metadata => JLinkageErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JLinkageErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JLinkageErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JLinkageErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JLinkageErrorObject IClassType<JLinkageErrorObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JLinkageErrorObject IClassType<JLinkageErrorObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JLinkageErrorObject IClassType<JLinkageErrorObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}