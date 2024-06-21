namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JClassCastExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.ClassCastException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JClassCastExceptionObject : JRuntimeExceptionObject, IThrowableType<JClassCastExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JRuntimeExceptionObject>
	                                                    .Create<JClassCastExceptionObject>(
		                                                    "java/lang/ClassCastException"u8).Build();

	static TypeMetadata IThrowableType<JClassCastExceptionObject>.Metadata => JClassCastExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JClassCastExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassCastExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassCastExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JClassCastExceptionObject IClassType<JClassCastExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JClassCastExceptionObject IClassType<JClassCastExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JClassCastExceptionObject IClassType<JClassCastExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}