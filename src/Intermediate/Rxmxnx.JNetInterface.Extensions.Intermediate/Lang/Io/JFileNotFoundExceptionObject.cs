namespace Rxmxnx.JNetInterface.Io;

using TypeMetadata = JThrowableTypeMetadata<JFileNotFoundExceptionObject>;

/// <summary>
/// This class represents a local <c>java.io.FileNotFoundException</c> instance.
/// </summary>
public class JFileNotFoundExceptionObject : JIoExceptionObject, IThrowableType<JFileNotFoundExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JIoExceptionObject>
	                                                    .Create<JFileNotFoundExceptionObject>(
		                                                    "java/io/FileNotFoundException"u8).Build();

	static TypeMetadata IThrowableType<JFileNotFoundExceptionObject>.Metadata
		=> JFileNotFoundExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JFileNotFoundExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JFileNotFoundExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JFileNotFoundExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JFileNotFoundExceptionObject IClassType<JFileNotFoundExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JFileNotFoundExceptionObject IClassType<JFileNotFoundExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JFileNotFoundExceptionObject IClassType<JFileNotFoundExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}