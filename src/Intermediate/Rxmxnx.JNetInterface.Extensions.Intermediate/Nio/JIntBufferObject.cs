namespace Rxmxnx.JNetInterface.Nio;

/// <summary>
/// This class represents a local <c>java.nio.IntBuffer</c> instance.
/// </summary>
public class JIntBufferObject : JBufferObject<JInt>, IClassType<JIntBufferObject>
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly JClassTypeMetadata<JIntBufferObject> metadata = TypeMetadataBuilder<JBufferObject>
	                                                                        .Create<JIntBufferObject>(
		                                                                        UnicodeClassNames.IntBufferObject(),
		                                                                        JTypeModifier.Abstract)
	                                                                        .Implements<JComparableObject>().Build();

	static JClassTypeMetadata<JIntBufferObject> IClassType<JIntBufferObject>.Metadata => JIntBufferObject.metadata;

	/// <inheritdoc/>
	private protected JIntBufferObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef) { }

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