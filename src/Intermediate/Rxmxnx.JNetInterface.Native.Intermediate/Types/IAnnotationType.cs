namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java annotation type instance.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IAnnotationType : IInterfaceType
{
#if !NET8_0_OR_GREATER
	// .NET 7.0 has issues inheriting static abstract members in non-generic interfaces from base classes.
	static JTypeKind IDataType.Kind => JTypeKind.Annotation;
	static Type IDataType.FamilyType => typeof(JAnnotationObject);
#endif
}

/// <summary>
/// This interface exposes an object that represents a java annotation type instance.
/// </summary>
/// <typeparam name="TAnnotation">Type of java annotation type.</typeparam>
public interface
	IAnnotationType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TAnnotation> :
	IAnnotationType,
	IInterfaceType<TAnnotation> where TAnnotation : JAnnotationObject<TAnnotation>, IAnnotationType<TAnnotation>;