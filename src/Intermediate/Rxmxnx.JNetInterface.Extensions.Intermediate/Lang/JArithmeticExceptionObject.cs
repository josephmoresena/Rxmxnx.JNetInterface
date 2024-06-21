namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JArithmeticExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.ArithmeticException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JArithmeticExceptionObject : JRuntimeExceptionObject, IThrowableType<JArithmeticExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JRuntimeExceptionObject>
	                                                    .Create<JArithmeticExceptionObject>(
		                                                    "java/lang/ArithmeticException"u8).Build();

	static TypeMetadata IThrowableType<JArithmeticExceptionObject>.Metadata => JArithmeticExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JArithmeticExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JArithmeticExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JArithmeticExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JArithmeticExceptionObject IClassType<JArithmeticExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JArithmeticExceptionObject IClassType<JArithmeticExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JArithmeticExceptionObject IClassType<JArithmeticExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}