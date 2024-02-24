namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.ExceptionInInitializerError</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JExceptionInInitializerErrorObject : JLinkageErrorObject,
	IThrowableType<JExceptionInInitializerErrorObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata<JExceptionInInitializerErrorObject> typeMetadata =
		TypeMetadataBuilder<JLinkageErrorObject>
			.Create<JExceptionInInitializerErrorObject>(UnicodeClassNames.ExceptionInInitializerErrorObject()).Build();

	static JThrowableTypeMetadata<JExceptionInInitializerErrorObject> IThrowableType<JExceptionInInitializerErrorObject>
		.Metadata
		=> JExceptionInInitializerErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JExceptionInInitializerErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JExceptionInInitializerErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JExceptionInInitializerErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JExceptionInInitializerErrorObject IReferenceType<JExceptionInInitializerErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JExceptionInInitializerErrorObject IReferenceType<JExceptionInInitializerErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JExceptionInInitializerErrorObject IReferenceType<JExceptionInInitializerErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}