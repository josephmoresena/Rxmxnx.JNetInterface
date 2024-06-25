namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JNumberFormatExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.NumberFormatException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JNumberFormatExceptionObject : JIllegalArgumentExceptionObject,
	IThrowableType<JNumberFormatExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JIllegalArgumentExceptionObject>
	                                                    .Create<JNumberFormatExceptionObject>(
		                                                    "java/lang/NumberFormatException"u8).Build();

	static TypeMetadata IThrowableType<JNumberFormatExceptionObject>.Metadata
		=> JNumberFormatExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JNumberFormatExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNumberFormatExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNumberFormatExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JNumberFormatExceptionObject IClassType<JNumberFormatExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JNumberFormatExceptionObject IClassType<JNumberFormatExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JNumberFormatExceptionObject IClassType<JNumberFormatExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}