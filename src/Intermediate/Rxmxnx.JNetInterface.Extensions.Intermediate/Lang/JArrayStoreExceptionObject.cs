namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JArrayStoreExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.ArrayStoreException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JArrayStoreExceptionObject : JIndexOutOfBoundsExceptionObject, IThrowableType<JArrayStoreExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JIndexOutOfBoundsExceptionObject>
	                                                    .Create<JArrayStoreExceptionObject>(
		                                                    "java/lang/ArrayStoreException"u8).Build();

	static TypeMetadata IThrowableType<JArrayStoreExceptionObject>.Metadata => JArrayStoreExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JArrayStoreExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JArrayStoreExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JArrayStoreExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JArrayStoreExceptionObject IClassType<JArrayStoreExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JArrayStoreExceptionObject IClassType<JArrayStoreExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JArrayStoreExceptionObject IClassType<JArrayStoreExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}