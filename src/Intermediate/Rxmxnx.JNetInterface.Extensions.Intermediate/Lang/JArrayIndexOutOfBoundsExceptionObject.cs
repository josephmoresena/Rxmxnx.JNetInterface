namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.ArrayIndexOutOfBoundsException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JArrayIndexOutOfBoundsExceptionObject : JIndexOutOfBoundsExceptionObject,
	IThrowableType<JArrayIndexOutOfBoundsExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata<JArrayIndexOutOfBoundsExceptionObject> typeMetadata =
		JTypeMetadataBuilder<JIndexOutOfBoundsExceptionObject>
			.Create<JArrayIndexOutOfBoundsExceptionObject>(UnicodeClassNames.ArrayIndexOutOfBoundsExceptionObject())
			.Build();

	static JThrowableTypeMetadata<JArrayIndexOutOfBoundsExceptionObject>
		IThrowableType<JArrayIndexOutOfBoundsExceptionObject>.Metadata
		=> JArrayIndexOutOfBoundsExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JArrayIndexOutOfBoundsExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JArrayIndexOutOfBoundsExceptionObject(IReferenceType.GlobalInitializer initializer) :
		base(initializer) { }
	/// <inheritdoc/>
	protected JArrayIndexOutOfBoundsExceptionObject(IReferenceType.ObjectInitializer initializer) :
		base(initializer) { }

	static JArrayIndexOutOfBoundsExceptionObject IReferenceType<JArrayIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JArrayIndexOutOfBoundsExceptionObject IReferenceType<JArrayIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JArrayIndexOutOfBoundsExceptionObject IReferenceType<JArrayIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}