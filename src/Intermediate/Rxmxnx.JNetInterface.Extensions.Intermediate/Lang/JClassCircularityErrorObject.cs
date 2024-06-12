namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JClassCircularityErrorObject>;

/// <summary>
/// This class represents a local <c>java.lang.ClassCircularityError</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JClassCircularityErrorObject : JLinkageErrorObject, IThrowableType<JClassCircularityErrorObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JLinkageErrorObject>
	                                                    .Create<JClassCircularityErrorObject>(
		                                                    "java/lang/ClassCircularityError"u8).Build();

	static TypeMetadata IThrowableType<JClassCircularityErrorObject>.Metadata
		=> JClassCircularityErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JClassCircularityErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassCircularityErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassCircularityErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JClassCircularityErrorObject IClassType<JClassCircularityErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JClassCircularityErrorObject IClassType<JClassCircularityErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JClassCircularityErrorObject IClassType<JClassCircularityErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}