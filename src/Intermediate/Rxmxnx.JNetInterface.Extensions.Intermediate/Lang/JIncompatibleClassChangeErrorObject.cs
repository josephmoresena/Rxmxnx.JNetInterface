namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.IncompatibleClassChangeError</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JIncompatibleClassChangeErrorObject : JLinkageErrorObject,
	IThrowableType<JIncompatibleClassChangeErrorObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata<JIncompatibleClassChangeErrorObject> typeMetadata =
		TypeMetadataBuilder<JLinkageErrorObject>
			.Create<JIncompatibleClassChangeErrorObject>(UnicodeClassNames.IncompatibleClassChangeErrorObject())
			.Build();

	static JThrowableTypeMetadata<JIncompatibleClassChangeErrorObject>
		IThrowableType<JIncompatibleClassChangeErrorObject>.Metadata
		=> JIncompatibleClassChangeErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JIncompatibleClassChangeErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIncompatibleClassChangeErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIncompatibleClassChangeErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JIncompatibleClassChangeErrorObject IClassType<JIncompatibleClassChangeErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JIncompatibleClassChangeErrorObject IClassType<JIncompatibleClassChangeErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JIncompatibleClassChangeErrorObject IClassType<JIncompatibleClassChangeErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}