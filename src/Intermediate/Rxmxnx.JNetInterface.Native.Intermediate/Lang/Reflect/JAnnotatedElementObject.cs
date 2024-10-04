namespace Rxmxnx.JNetInterface.Lang.Reflect;

using TypeMetadata = JInterfaceTypeMetadata<JAnnotatedElementObject>;

/// <summary>
/// This class represents a local <c>java.lang.Annotation.AnnotatedElement</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JAnnotatedElementObject : JInterfaceObject<JAnnotatedElementObject>,
	IInterfaceType<JAnnotatedElementObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.AnnotatedElementHash, 34);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JLocalObject.InterfaceView.CreateBuiltInMetadata<JAnnotatedElementObject>(
			JAnnotatedElementObject.typeInfo, InterfaceSet.Empty);

	static TypeMetadata IInterfaceType<JAnnotatedElementObject>.Metadata => JAnnotatedElementObject.typeMetadata;

	/// <inheritdoc/>
	private JAnnotatedElementObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JAnnotatedElementObject IInterfaceType<JAnnotatedElementObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}