namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JRuntimeExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.RuntimeException</c> instance.
/// </summary>
public class JRuntimeExceptionObject : JExceptionObject, IThrowableType<JRuntimeExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JExceptionObject>
	                                                    .Create<JRuntimeExceptionObject>("java/lang/RuntimeException"u8)
	                                                    .Build();

	static TypeMetadata IThrowableType<JRuntimeExceptionObject>.Metadata => JRuntimeExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JRuntimeExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JRuntimeExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JRuntimeExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JRuntimeExceptionObject IClassType<JRuntimeExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JRuntimeExceptionObject IClassType<JRuntimeExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JRuntimeExceptionObject IClassType<JRuntimeExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}