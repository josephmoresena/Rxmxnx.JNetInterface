namespace Rxmxnx.JNetInterface.Nio;

public partial class JBufferObject
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly JClassTypeMetadata metadata = JTypeMetadataBuilder<JBufferObject>
	                                                      .Create(UnicodeClassNames.BufferObject(),
	                                                              JTypeModifier.Abstract).Build();

	static JDataTypeMetadata IDataType.Metadata => JBufferObject.metadata;

	/// <inheritdoc cref="JBufferObject.Address"/>
	private IntPtr? _address;
	/// <inheritdoc cref="JBufferObject.Capacity"/>
	private Int64? _capacity;

	/// <inheritdoc cref="JBufferObject.IsDirect"/>
	private Boolean? _isDirect;

	static JBufferObject IReferenceType<JBufferObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JBufferObject IReferenceType<JBufferObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JBufferObject IReferenceType<JBufferObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}