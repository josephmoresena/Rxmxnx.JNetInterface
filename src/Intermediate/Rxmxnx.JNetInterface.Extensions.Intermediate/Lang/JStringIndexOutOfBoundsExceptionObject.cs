namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JStringIndexOutOfBoundsExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.StringIndexOutOfBoundsException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JStringIndexOutOfBoundsExceptionObject : JIndexOutOfBoundsExceptionObject,
	IThrowableType<JStringIndexOutOfBoundsExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JIndexOutOfBoundsExceptionObject>
	                                                    .Create<JStringIndexOutOfBoundsExceptionObject>(
		                                                    "java/lang/StringIndexOutOfBoundsException"u8).Build();

	static TypeMetadata IThrowableType<JStringIndexOutOfBoundsExceptionObject>.Metadata
		=> JStringIndexOutOfBoundsExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JStringIndexOutOfBoundsExceptionObject(IReferenceType.ClassInitializer initializer) :
		base(initializer) { }
	/// <inheritdoc/>
	protected JStringIndexOutOfBoundsExceptionObject(IReferenceType.GlobalInitializer initializer) :
		base(initializer) { }
	/// <inheritdoc/>
	protected JStringIndexOutOfBoundsExceptionObject(IReferenceType.ObjectInitializer initializer) :
		base(initializer) { }

	static JStringIndexOutOfBoundsExceptionObject IClassType<JStringIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JStringIndexOutOfBoundsExceptionObject IClassType<JStringIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JStringIndexOutOfBoundsExceptionObject IClassType<JStringIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}