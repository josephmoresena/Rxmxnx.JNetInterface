namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.VirtualMachineError</c> instance.
/// </summary>
public class JVirtualMachineErrorObject : JErrorObject, IThrowableType<JVirtualMachineErrorObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata<JVirtualMachineErrorObject> typeMetadata =
		TypeMetadataBuilder<JErrorObject>
			.Create<JVirtualMachineErrorObject>(UnicodeClassNames.VirtualMachineErrorObject()).Build();

	static JThrowableTypeMetadata<JVirtualMachineErrorObject> IThrowableType<JVirtualMachineErrorObject>.Metadata
		=> JVirtualMachineErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JVirtualMachineErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JVirtualMachineErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JVirtualMachineErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JVirtualMachineErrorObject IClassType<JVirtualMachineErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JVirtualMachineErrorObject IClassType<JVirtualMachineErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JVirtualMachineErrorObject IClassType<JVirtualMachineErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}