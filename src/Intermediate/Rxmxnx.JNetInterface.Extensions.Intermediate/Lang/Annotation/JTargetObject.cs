namespace Rxmxnx.JNetInterface.Lang.Annotation;

/// <summary>
/// This class represents a local <c>java.lang.annotation.Target</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JTargetObject : JAnnotationObject<JTargetObject>, IInterfaceType<JTargetObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata<JTargetObject> typeMetadata =
		TypeMetadataBuilder<JTargetObject>.Create(UnicodeClassNames.TargetAnnotation()).Build();

	static JInterfaceTypeMetadata<JTargetObject> IInterfaceType<JTargetObject>.Metadata => JTargetObject.typeMetadata;

	/// <inheritdoc/>
	private JTargetObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JTargetObject IInterfaceType<JTargetObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}