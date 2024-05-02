namespace Rxmxnx.JNetInterface.Nio;

/// <summary>
/// This class represents a local <c>java.nio.DoubleBuffer</c> instance.
/// </summary>
public class JDoubleBufferObject : JBufferObject<JDouble>, IClassType<JDoubleBufferObject>
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly JClassTypeMetadata<JDoubleBufferObject> metadata = TypeMetadataBuilder<JBufferObject>
	                                                                           .Create<JDoubleBufferObject>(
		                                                                           UnicodeClassNames
			                                                                           .DoubleBufferObject(),
		                                                                           JTypeModifier.Abstract)
	                                                                           .Implements<JComparableObject>().Build();

	static JClassTypeMetadata<JDoubleBufferObject> IClassType<JDoubleBufferObject>.Metadata
		=> JDoubleBufferObject.metadata;

	/// <inheritdoc/>
	protected JDoubleBufferObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JDoubleBufferObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JDoubleBufferObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JDoubleBufferObject IClassType<JDoubleBufferObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JDoubleBufferObject IClassType<JDoubleBufferObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JDoubleBufferObject IClassType<JDoubleBufferObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}