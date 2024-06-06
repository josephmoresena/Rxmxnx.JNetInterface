namespace Rxmxnx.JNetInterface.Nio;

using TypeMetadata = JClassTypeMetadata<JLongBufferObject>;

/// <summary>
/// This class represents a local <c>java.nio.LongBuffer</c> instance.
/// </summary>
public class JLongBufferObject : JBufferObject<JLong>, IClassType<JLongBufferObject>
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JBufferObject>
	                                                    .Create<JLongBufferObject>("java/nio/LongBuffer"u8,
		                                                    JTypeModifier.Abstract).Implements<JComparableObject>()
	                                                    .Build();

	static TypeMetadata IClassType<JLongBufferObject>.Metadata => JLongBufferObject.typeMetadata;

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