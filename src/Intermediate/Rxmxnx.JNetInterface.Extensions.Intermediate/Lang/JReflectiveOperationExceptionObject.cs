namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JReflectiveOperationExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.ReflectiveOperationException</c> instance.
/// </summary>
public class JReflectiveOperationExceptionObject : JExceptionObject, IThrowableType<JReflectiveOperationExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JExceptionObject>
	                                                    .Create<JReflectiveOperationExceptionObject>(
		                                                    "java/lang/ReflectiveOperationException"u8).Build();

	static TypeMetadata IThrowableType<JReflectiveOperationExceptionObject>.Metadata
		=> JReflectiveOperationExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JReflectiveOperationExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JReflectiveOperationExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JReflectiveOperationExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JReflectiveOperationExceptionObject IClassType<JReflectiveOperationExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JReflectiveOperationExceptionObject IClassType<JReflectiveOperationExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JReflectiveOperationExceptionObject IClassType<JReflectiveOperationExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}