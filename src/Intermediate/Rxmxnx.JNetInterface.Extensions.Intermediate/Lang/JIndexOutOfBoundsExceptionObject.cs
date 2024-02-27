namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.IndexOutOfBoundsException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JIndexOutOfBoundsExceptionObject : JRuntimeExceptionObject,
	IThrowableType<JIndexOutOfBoundsExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata<JIndexOutOfBoundsExceptionObject> typeMetadata =
		TypeMetadataBuilder<JRuntimeExceptionObject>
			.Create<JIndexOutOfBoundsExceptionObject>(UnicodeClassNames.IndexOutOfBoundsExceptionObject()).Build();

	static JThrowableTypeMetadata<JIndexOutOfBoundsExceptionObject> IThrowableType<JIndexOutOfBoundsExceptionObject>.
		Metadata
		=> JIndexOutOfBoundsExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JIndexOutOfBoundsExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIndexOutOfBoundsExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIndexOutOfBoundsExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JIndexOutOfBoundsExceptionObject IClassType<JIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JIndexOutOfBoundsExceptionObject IClassType<JIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JIndexOutOfBoundsExceptionObject IClassType<JIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}