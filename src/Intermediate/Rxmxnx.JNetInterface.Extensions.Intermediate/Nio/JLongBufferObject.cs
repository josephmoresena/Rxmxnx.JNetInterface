namespace Rxmxnx.JNetInterface.Nio;

/// <summary>
/// This class represents a local <c>java.nio.LongBuffer</c> instance.
/// </summary>
public class JLongBufferObject : JBufferObject<JLong>, IClassType<JLongBufferObject>
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly JClassTypeMetadata<JLongBufferObject> metadata = TypeMetadataBuilder<JBufferObject>
	                                                                         .Create<JLongBufferObject>(
		                                                                         UnicodeClassNames.LongBufferObject(),
		                                                                         JTypeModifier.Abstract)
	                                                                         .Implements<JComparableObject>().Build();

	static JClassTypeMetadata<JLongBufferObject> IClassType<JLongBufferObject>.Metadata => JLongBufferObject.metadata;

	/// <inheritdoc/>
	protected JLongBufferObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JLongBufferObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JLongBufferObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JLongBufferObject IClassType<JLongBufferObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JLongBufferObject IClassType<JLongBufferObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JLongBufferObject IClassType<JLongBufferObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}