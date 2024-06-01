namespace Rxmxnx.JNetInterface.Lang;

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
	private static readonly JThrowableTypeMetadata<JClassNotFoundExceptionObject> typeMetadata =
		TypeMetadataBuilder<JReflectiveOperationExceptionObject>
			.Create<JClassNotFoundExceptionObject>(UnicodeClassNames.ClassNotFoundExceptionObject()).Build();

	static JThrowableTypeMetadata<JClassNotFoundExceptionObject> IThrowableType<JClassNotFoundExceptionObject>.Metadata
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