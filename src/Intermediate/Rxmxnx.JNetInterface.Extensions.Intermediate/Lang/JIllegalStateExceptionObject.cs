namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JIllegalStateExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.IllegalStateException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JIllegalStateExceptionObject : JRuntimeExceptionObject, IThrowableType<JIllegalStateExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JRuntimeExceptionObject>
	                                                    .Create<JIllegalStateExceptionObject>(
		                                                    "java/lang/IllegalStateException"u8).Build();

	static TypeMetadata IThrowableType<JIllegalStateExceptionObject>.Metadata
		=> JIllegalStateExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JIllegalStateExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIllegalStateExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIllegalStateExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JIllegalStateExceptionObject IClassType<JIllegalStateExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JIllegalStateExceptionObject IClassType<JIllegalStateExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JIllegalStateExceptionObject IClassType<JIllegalStateExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}