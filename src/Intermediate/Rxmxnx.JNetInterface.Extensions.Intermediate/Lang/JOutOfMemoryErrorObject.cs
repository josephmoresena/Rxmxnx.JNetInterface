namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JOutOfMemoryErrorObject>;

/// <summary>
/// This class represents a local <c>java.lang.OutOfMemoryError</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JOutOfMemoryErrorObject : JVirtualMachineErrorObject, IThrowableType<JOutOfMemoryErrorObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.OutOfMemoryErrorHash, 26);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JOutOfMemoryErrorObject>(JOutOfMemoryErrorObject.typeInfo,
		                                                                IClassType
			                                                                .GetMetadata<JVirtualMachineErrorObject>(),
		                                                                JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JOutOfMemoryErrorObject>.Metadata => JOutOfMemoryErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JOutOfMemoryErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JOutOfMemoryErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JOutOfMemoryErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JOutOfMemoryErrorObject IClassType<JOutOfMemoryErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JOutOfMemoryErrorObject IClassType<JOutOfMemoryErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JOutOfMemoryErrorObject IClassType<JOutOfMemoryErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}