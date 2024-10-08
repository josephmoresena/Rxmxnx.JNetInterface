namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JVirtualMachineErrorObject>;

/// <summary>
/// This class represents a local <c>java.lang.VirtualMachineError</c> instance.
/// </summary>
public class JVirtualMachineErrorObject : JErrorObject, IThrowableType<JVirtualMachineErrorObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.VirtualMachineErrorHash, 29);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JVirtualMachineErrorObject>(
			    JVirtualMachineErrorObject.typeInfo, IClassType.GetMetadata<JErrorObject>(), JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JVirtualMachineErrorObject>.Metadata => JVirtualMachineErrorObject.typeMetadata;

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