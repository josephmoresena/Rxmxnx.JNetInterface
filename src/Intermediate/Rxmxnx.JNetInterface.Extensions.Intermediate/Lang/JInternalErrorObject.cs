namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JInternalErrorObject>;

/// <summary>
/// This class represents a local <c>java.lang.InternalError</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public class JInternalErrorObject : JVirtualMachineErrorObject, IThrowableType<JInternalErrorObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.InternalErrorHash, 23);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JInternalErrorObject>(JInternalErrorObject.typeInfo,
		                                                             IClassType
			                                                             .GetMetadata<JVirtualMachineErrorObject>(),
		                                                             JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JInternalErrorObject>.Metadata => JInternalErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JInternalErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JInternalErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JInternalErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JInternalErrorObject IClassType<JInternalErrorObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JInternalErrorObject IClassType<JInternalErrorObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JInternalErrorObject IClassType<JInternalErrorObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}