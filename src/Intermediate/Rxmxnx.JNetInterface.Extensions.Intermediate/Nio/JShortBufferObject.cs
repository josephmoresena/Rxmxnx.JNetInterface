namespace Rxmxnx.JNetInterface.Nio;

/// <summary>
/// This class represents a local <c>java.nio.ShortBuffer</c> instance.
/// </summary>
public class JShortBufferObject : JBufferObject<JShort>, IClassType<JShortBufferObject>
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly JClassTypeMetadata<JShortBufferObject> metadata = TypeMetadataBuilder<JBufferObject>
	                                                                          .Create<JShortBufferObject>(
		                                                                          UnicodeClassNames.ShortBufferObject(),
		                                                                          JTypeModifier.Abstract)
	                                                                          .Implements<JComparableObject>().Build();

	static JClassTypeMetadata<JShortBufferObject> IClassType<JShortBufferObject>.Metadata
		=> JShortBufferObject.metadata;

	/// <inheritdoc/>
	private protected JShortBufferObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef) { }

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