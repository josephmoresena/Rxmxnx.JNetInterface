namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JInstantiationExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.InstantiationException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JInstantiationExceptionObject : JReflectiveOperationExceptionObject,
	IThrowableType<JInstantiationExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JReflectiveOperationExceptionObject>
	                                                    .Create<JInstantiationExceptionObject>(
		                                                    "java/lang/InstantiationException"u8).Build();

	static TypeMetadata IThrowableType<JInstantiationExceptionObject>.Metadata
		=> JInstantiationExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JInstantiationExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JInstantiationExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JInstantiationExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JInstantiationExceptionObject IClassType<JInstantiationExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JInstantiationExceptionObject IClassType<JInstantiationExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JInstantiationExceptionObject IClassType<JInstantiationExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}