namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JErrorObject>;

/// <summary>
/// This class represents a local <c>java.lang.Error</c> instance.
/// </summary>
public class JErrorObject : JThrowableObject, IThrowableType<JErrorObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JThrowableObject>
	                                                    .Create<JErrorObject>("java/lang/Error"u8).Build();

	static TypeMetadata IThrowableType<JErrorObject>.Metadata => JErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JErrorObject IClassType<JErrorObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JErrorObject IClassType<JErrorObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JErrorObject IClassType<JErrorObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}