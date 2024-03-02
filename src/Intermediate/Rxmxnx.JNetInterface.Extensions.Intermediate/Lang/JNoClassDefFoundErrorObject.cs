namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.NoSuchFieldError</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JNoClassDefFoundErrorObject : JLinkageErrorObject, IThrowableType<JNoClassDefFoundErrorObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata<JNoClassDefFoundErrorObject> typeMetadata =
		TypeMetadataBuilder<JLinkageErrorObject>
			.Create<JNoClassDefFoundErrorObject>(UnicodeClassNames.NoClassDefFoundErrorObject()).Build();

	static JThrowableTypeMetadata<JNoClassDefFoundErrorObject> IThrowableType<JNoClassDefFoundErrorObject>.Metadata
		=> JNoClassDefFoundErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JNoClassDefFoundErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNoClassDefFoundErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNoClassDefFoundErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JNoClassDefFoundErrorObject IClassType<JNoClassDefFoundErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JNoClassDefFoundErrorObject IClassType<JNoClassDefFoundErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JNoClassDefFoundErrorObject IClassType<JNoClassDefFoundErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}