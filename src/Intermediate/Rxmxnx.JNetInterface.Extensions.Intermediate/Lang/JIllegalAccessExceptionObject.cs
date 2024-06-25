namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JIllegalAccessExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.IllegalAccessException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JIllegalAccessExceptionObject : JReflectiveOperationExceptionObject,
	IThrowableType<JIllegalAccessExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JReflectiveOperationExceptionObject>
	                                                    .Create<JIllegalAccessExceptionObject>(
		                                                    "java/lang/IllegalAccessException"u8).Build();

	static TypeMetadata IThrowableType<JIllegalAccessExceptionObject>.Metadata
		=> JIllegalAccessExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JIllegalAccessExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIllegalAccessExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIllegalAccessExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JIllegalAccessExceptionObject IClassType<JIllegalAccessExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JIllegalAccessExceptionObject IClassType<JIllegalAccessExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JIllegalAccessExceptionObject IClassType<JIllegalAccessExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}