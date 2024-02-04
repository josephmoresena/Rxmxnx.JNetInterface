namespace Rxmxnx.JNetInterface.Nio.Ch;

/// <summary>
/// This class represents a local <c>sun.nio.ch.DirectBuffer</c> instance.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class JDirectBufferObject : JInterfaceObject<JDirectBufferObject>, IInterfaceType<JDirectBufferObject>
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata metadata = JTypeMetadataBuilder<JDirectBufferObject>
	                                                          .Create(UnicodeClassNames.DirectBufferObject()).Build();

	static JDataTypeMetadata IDataType.Metadata => JDirectBufferObject.metadata;

	/// <inheritdoc/>
	private JDirectBufferObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JDirectBufferObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JDirectBufferObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JDirectBufferObject IReferenceType<JDirectBufferObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JDirectBufferObject IReferenceType<JDirectBufferObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JDirectBufferObject IReferenceType<JDirectBufferObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}