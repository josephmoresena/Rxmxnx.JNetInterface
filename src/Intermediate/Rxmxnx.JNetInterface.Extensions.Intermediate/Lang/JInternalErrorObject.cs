namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JInternalErrorObject>;

/// <summary>
/// This class represents a local <c>java.lang.InternalError</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JInternalErrorObject : JVirtualMachineErrorObject, IThrowableType<JInternalErrorObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JVirtualMachineErrorObject>
	                                                    .Create<JInternalErrorObject>("java/lang/InternalError"u8)
	                                                    .Build();

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