namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.NoSuchMethodError</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JNoSuchMethodErrorObject : JIncompatibleClassChangeErrorObject, IThrowableType<JNoSuchMethodErrorObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata<JNoSuchMethodErrorObject> typeMetadata =
		TypeMetadataBuilder<JIncompatibleClassChangeErrorObject>
			.Create<JNoSuchMethodErrorObject>(UnicodeClassNames.NoSuchMethodErrorObject()).Build();

	static JThrowableTypeMetadata<JNoSuchMethodErrorObject> IThrowableType<JNoSuchMethodErrorObject>.Metadata
		=> JNoSuchMethodErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JNoSuchMethodErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNoSuchMethodErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNoSuchMethodErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JNoSuchMethodErrorObject IReferenceType<JNoSuchMethodErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JNoSuchMethodErrorObject IReferenceType<JNoSuchMethodErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JNoSuchMethodErrorObject IReferenceType<JNoSuchMethodErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}