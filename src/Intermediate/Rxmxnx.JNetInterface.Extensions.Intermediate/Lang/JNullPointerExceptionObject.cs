namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.NullPointerException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JNullPointerExceptionObject : JRuntimeExceptionObject, IThrowableType<JNullPointerExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata<JNullPointerExceptionObject> typeMetadata =
		TypeMetadataBuilder<JRuntimeExceptionObject>
			.Create<JNullPointerExceptionObject>(UnicodeClassNames.NullPointerExceptionObject()).Build();

	static JThrowableTypeMetadata<JNullPointerExceptionObject> IThrowableType<JNullPointerExceptionObject>.Metadata
		=> JNullPointerExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JNullPointerExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNullPointerExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNullPointerExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JNullPointerExceptionObject IClassType<JNullPointerExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JNullPointerExceptionObject IClassType<JNullPointerExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JNullPointerExceptionObject IClassType<JNullPointerExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}