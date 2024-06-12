namespace Rxmxnx.JNetInterface.Nio;

using TypeMetadata = JClassTypeMetadata<JIntBufferObject>;

/// <summary>
/// This class represents a local <c>java.nio.IntBuffer</c> instance.
/// </summary>
public class JIntBufferObject : JBufferObject<JInt>, IClassType<JIntBufferObject>
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JBufferObject>
	                                                    .Create<JIntBufferObject>(
		                                                    "java/nio/IntBuffer"u8, JTypeModifier.Abstract)
	                                                    .Implements<JComparableObject>().Build();

	static TypeMetadata IClassType<JIntBufferObject>.Metadata => JIntBufferObject.typeMetadata;

	/// <inheritdoc/>
	protected JIntBufferObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIntBufferObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIntBufferObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JIntBufferObject IClassType<JIntBufferObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JIntBufferObject IClassType<JIntBufferObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JIntBufferObject IClassType<JIntBufferObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}