namespace Rxmxnx.JNetInterface.Nio;

/// <summary>
/// This class represents a local <c>java.nio.MappedByteBuffer</c> instance.
/// </summary>
public class JMappedByteBufferObject : JByteBufferObject, IClassType<JMappedByteBufferObject>
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly JClassTypeMetadata metadata = JTypeMetadataBuilder<JByteBufferObject>
	                                                      .Create<JMappedByteBufferObject>(
		                                                      UnicodeClassNames.MappedByteBufferObject(),
		                                                      JTypeModifier.Abstract).Implements<JComparableObject>()
	                                                      .Build();

	static JDataTypeMetadata IDataType.Metadata => JMappedByteBufferObject.metadata;

	/// <inheritdoc/>
	internal JMappedByteBufferObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef) { }

	/// <inheritdoc/>
	protected JMappedByteBufferObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JMappedByteBufferObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JMappedByteBufferObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JMappedByteBufferObject IReferenceType<JMappedByteBufferObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JMappedByteBufferObject IReferenceType<JMappedByteBufferObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JMappedByteBufferObject IReferenceType<JMappedByteBufferObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}