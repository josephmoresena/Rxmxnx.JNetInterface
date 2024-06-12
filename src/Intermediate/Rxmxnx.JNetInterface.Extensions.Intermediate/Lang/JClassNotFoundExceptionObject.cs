namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JClassNotFoundExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.ClassNotFoundException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JClassNotFoundExceptionObject : JReflectiveOperationExceptionObject,
	IThrowableType<JClassNotFoundExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JReflectiveOperationExceptionObject>
	                                                    .Create<JClassNotFoundExceptionObject>(
		                                                    "java/lang/ClassNotFoundException"u8).Build();

	static TypeMetadata IThrowableType<JClassNotFoundExceptionObject>.Metadata
		=> JClassNotFoundExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JClassNotFoundExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassNotFoundExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassNotFoundExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JClassNotFoundExceptionObject IClassType<JClassNotFoundExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JClassNotFoundExceptionObject IClassType<JClassNotFoundExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JClassNotFoundExceptionObject IClassType<JClassNotFoundExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}