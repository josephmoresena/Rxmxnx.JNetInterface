namespace Rxmxnx.JNetInterface.Lang.Annotation;

/// <summary>
/// This class represents a local <c>java.lang.annotation.Annotation</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JAnnotationObject : JInterfaceObject<JAnnotationObject>, IInterfaceType<JAnnotationObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata<JAnnotationObject> typeMetadata =
		TypeMetadataBuilder<JAnnotationObject>.Create(UnicodeClassNames.AnnotationInterface()).Build();

	static JInterfaceTypeMetadata<JAnnotationObject> IInterfaceType<JAnnotationObject>.Metadata
		=> JAnnotationObject.typeMetadata;

	/// <inheritdoc/>
	private JAnnotationObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JAnnotationObject IInterfaceType<JAnnotationObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}

/// <summary>
/// This class represents an annotation instance.
/// </summary>
/// <typeparam name="TAnnotation">Type of <see cref="IInterfaceType"/>.</typeparam>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public abstract class JAnnotationObject<TAnnotation> : JInterfaceObject<TAnnotation>,
	IInterfaceObject<JAnnotationObject>,
	IDataType where TAnnotation : JAnnotationObject<TAnnotation>, IInterfaceType<TAnnotation>
{
	static JTypeKind IDataType.Kind => JTypeKind.Annotation;
	static Type IDataType.FamilyType => typeof(JAnnotationObject);

	/// <inheritdoc/>
	protected JAnnotationObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
}