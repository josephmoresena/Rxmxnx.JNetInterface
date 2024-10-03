namespace Rxmxnx.JNetInterface.Io;

using TypeMetadata = JThrowableTypeMetadata<JIoExceptionObject>;

/// <summary>
/// This class represents a local <c>java.io.IOException</c> instance.
/// </summary>
public class JIoExceptionObject : JExceptionObject, IThrowableType<JIoExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JExceptionObject>
	                                                    .Create<JIoExceptionObject>("java/io/IOException"u8).Build();

	static TypeMetadata IThrowableType<JIoExceptionObject>.Metadata => JIoExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JIoExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIoExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIoExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JIoExceptionObject IClassType<JIoExceptionObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JIoExceptionObject IClassType<JIoExceptionObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JIoExceptionObject IClassType<JIoExceptionObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}