namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.VirtualMachineError</c> instance.
/// </summary>
public class JVirtualMachineErrorObject : JErrorObject, IThrowableType<JVirtualMachineErrorObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata typeMetadata = JTypeMetadataBuilder<JErrorObject>
	                                                              .Create<JVirtualMachineErrorObject>(
		                                                              UnicodeClassNames.VirtualMachineErrorObject())
	                                                              .Build();

	static JDataTypeMetadata IDataType.Metadata => JVirtualMachineErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JVirtualMachineErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JVirtualMachineErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JVirtualMachineErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JVirtualMachineErrorObject IReferenceType<JVirtualMachineErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JVirtualMachineErrorObject IReferenceType<JVirtualMachineErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JVirtualMachineErrorObject IReferenceType<JVirtualMachineErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}