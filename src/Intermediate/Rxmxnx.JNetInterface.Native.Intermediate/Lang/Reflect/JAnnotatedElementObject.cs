namespace Rxmxnx.JNetInterface.Lang.Reflect;

/// <summary>
/// This class represents a local <c>java.lang.Annotation.AnnotatedElement</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JAnnotatedElementObject : JInterfaceObject<JAnnotatedElementObject>,
	IInterfaceType<JAnnotatedElementObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata<JAnnotatedElementObject> typeMetadata =
		TypeMetadataBuilder<JAnnotatedElementObject>.Create(UnicodeClassNames.AnnotatedElementInterface()).Build();

	static JInterfaceTypeMetadata<JAnnotatedElementObject> IInterfaceType<JAnnotatedElementObject>.Metadata
		=> JAnnotatedElementObject.typeMetadata;

	/// <inheritdoc/>
	private JAnnotatedElementObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JAnnotatedElementObject IInterfaceType<JAnnotatedElementObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}