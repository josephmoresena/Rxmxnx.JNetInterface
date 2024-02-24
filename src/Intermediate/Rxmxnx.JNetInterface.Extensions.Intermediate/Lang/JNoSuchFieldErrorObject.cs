namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.NoSuchFieldError</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JNoSuchFieldErrorObject : JIncompatibleClassChangeErrorObject, IThrowableType<JNoSuchFieldErrorObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata<JNoSuchFieldErrorObject> typeMetadata =
		TypeMetadataBuilder<JIncompatibleClassChangeErrorObject>
			.Create<JNoSuchFieldErrorObject>(UnicodeClassNames.NoSuchFieldErrorObject()).Build();

	static JThrowableTypeMetadata<JNoSuchFieldErrorObject> IThrowableType<JNoSuchFieldErrorObject>.Metadata
		=> JNoSuchFieldErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JNoSuchFieldErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNoSuchFieldErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNoSuchFieldErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JNoSuchFieldErrorObject IReferenceType<JNoSuchFieldErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JNoSuchFieldErrorObject IReferenceType<JNoSuchFieldErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JNoSuchFieldErrorObject IReferenceType<JNoSuchFieldErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}