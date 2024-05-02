namespace Rxmxnx.JNetInterface.Nio;

/// <summary>
/// This class represents a local <c>java.nio.CharBuffer</c> instance.
/// </summary>
public class JCharBufferObject : JBufferObject<JChar>, IClassType<JCharBufferObject>,
	IInterfaceObject<JAppendableObject>, IInterfaceObject<JReadableObject>
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly JClassTypeMetadata<JCharBufferObject> metadata = TypeMetadataBuilder<JBufferObject>
	                                                                         .Create<JCharBufferObject>(
		                                                                         UnicodeClassNames.CharBufferObject(),
		                                                                         JTypeModifier.Abstract)
	                                                                         .Implements<JComparableObject>()
	                                                                         .Implements<JAppendableObject>()
	                                                                         .Implements<JReadableObject>().Build();

	static JClassTypeMetadata<JCharBufferObject> IClassType<JCharBufferObject>.Metadata => JCharBufferObject.metadata;

	/// <inheritdoc/>
	protected JCharBufferObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JCharBufferObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JCharBufferObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JCharBufferObject IClassType<JCharBufferObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JCharBufferObject IClassType<JCharBufferObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JCharBufferObject IClassType<JCharBufferObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}