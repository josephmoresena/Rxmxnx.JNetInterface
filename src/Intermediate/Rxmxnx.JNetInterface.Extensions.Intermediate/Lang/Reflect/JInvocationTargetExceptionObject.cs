namespace Rxmxnx.JNetInterface.Lang.Reflect;

using TypeMetadata = JThrowableTypeMetadata<JInvocationTargetExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.reflect.InvocationTargetException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JInvocationTargetExceptionObject : JReflectiveOperationExceptionObject,
	IThrowableType<JInvocationTargetExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JReflectiveOperationExceptionObject>
	                                                    .Create<JInvocationTargetExceptionObject>(
		                                                    "java/lang/reflect/InvocationTargetException"u8).Build();

	static TypeMetadata IThrowableType<JInvocationTargetExceptionObject>.Metadata
		=> JInvocationTargetExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JInvocationTargetExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JInvocationTargetExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JInvocationTargetExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JInvocationTargetExceptionObject IClassType<JInvocationTargetExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JInvocationTargetExceptionObject IClassType<JInvocationTargetExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JInvocationTargetExceptionObject IClassType<JInvocationTargetExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}