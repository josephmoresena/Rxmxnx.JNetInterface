namespace Rxmxnx.JNetInterface.Lang.Annotation;

using TypeMetadata = JInterfaceTypeMetadata<JAnnotationObject>;

/// <summary>
/// This class represents a local <c>java.lang.annotation.Annotation</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public sealed class JAnnotationObject : JInterfaceObject<JAnnotationObject>, IInterfaceType<JAnnotationObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.AnnotationHash, 31);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JLocalObject.InterfaceView.CreateBuiltInMetadata<JAnnotationObject>(
			JAnnotationObject.typeInfo, InterfaceSet.Empty);

	static TypeMetadata IInterfaceType<JAnnotationObject>.Metadata => JAnnotationObject.typeMetadata;

	/// <inheritdoc/>
	private JAnnotationObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JAnnotationObject IInterfaceType<JAnnotationObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}

/// <summary>
/// This class represents an annotation instance.
/// </summary>
/// <typeparam name="TAnnotation">Type of <see cref="IInterfaceType"/>.</typeparam>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public abstract class JAnnotationObject<TAnnotation> : JInterfaceObject<TAnnotation>,
	IInterfaceObject<JAnnotationObject>,
	IDataType where TAnnotation : JAnnotationObject<TAnnotation>, IInterfaceType<TAnnotation>
{
	static JTypeKind IDataType.Kind => JTypeKind.Annotation;
	static Type IDataType.FamilyType => typeof(JAnnotationObject);

	/// <inheritdoc/>
	protected JAnnotationObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
}