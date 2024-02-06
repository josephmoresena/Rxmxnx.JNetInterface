namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.OutOfMemoryError</c> instance.
/// </summary>
public class JOutOfMemoryErrorObject : JVirtualMachineErrorObject, IThrowableType<JOutOfMemoryErrorObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata typeMetadata = JTypeMetadataBuilder<JVirtualMachineErrorObject>
	                                                              .Create<JOutOfMemoryErrorObject>(
		                                                              UnicodeClassNames.OutOfMemoryErrorObject())
	                                                              .Build();

	static JDataTypeMetadata IDataType.Metadata => JOutOfMemoryErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JOutOfMemoryErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JOutOfMemoryErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JOutOfMemoryErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JOutOfMemoryErrorObject IReferenceType<JOutOfMemoryErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JOutOfMemoryErrorObject IReferenceType<JOutOfMemoryErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JOutOfMemoryErrorObject IReferenceType<JOutOfMemoryErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}