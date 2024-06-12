namespace Rxmxnx.JNetInterface.Nio;

using TypeMetadata = JClassTypeMetadata<JShortBufferObject>;

/// <summary>
/// This class represents a local <c>java.nio.ShortBuffer</c> instance.
/// </summary>
public class JShortBufferObject : JBufferObject<JShort>, IClassType<JShortBufferObject>
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JBufferObject>
	                                                    .Create<JShortBufferObject>(
		                                                    "java/nio/ShortBuffer"u8, JTypeModifier.Abstract)
	                                                    .Implements<JComparableObject>().Build();

	static TypeMetadata IClassType<JShortBufferObject>.Metadata => JShortBufferObject.typeMetadata;

	/// <inheritdoc/>
	protected JShortBufferObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JShortBufferObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JShortBufferObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JShortBufferObject IClassType<JShortBufferObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JShortBufferObject IClassType<JShortBufferObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JShortBufferObject IClassType<JShortBufferObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}