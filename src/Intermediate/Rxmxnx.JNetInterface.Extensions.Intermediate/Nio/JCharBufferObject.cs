namespace Rxmxnx.JNetInterface.Nio;

using TypeMetadata = JClassTypeMetadata<JCharBufferObject>;

/// <summary>
/// This class represents a local <c>java.nio.CharBuffer</c> instance.
/// </summary>
public class JCharBufferObject : JBufferObject<JChar>, IClassType<JCharBufferObject>,
	IInterfaceObject<JAppendableObject>, IInterfaceObject<JReadableObject>
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JBufferObject>
	                                                    .Create<JCharBufferObject>(
		                                                    "java/ nio/ CharBuffer"u8, JTypeModifier.Abstract)
	                                                    .Implements<JComparableObject>().Implements<JAppendableObject>()
	                                                    .Implements<JReadableObject>().Build();

	static TypeMetadata IClassType<JCharBufferObject>.Metadata => JCharBufferObject.typeMetadata;

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