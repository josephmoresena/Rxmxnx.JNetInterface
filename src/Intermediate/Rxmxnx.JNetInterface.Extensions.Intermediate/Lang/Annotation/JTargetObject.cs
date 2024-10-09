namespace Rxmxnx.JNetInterface.Lang.Annotation;

using TypeMetadata = JInterfaceTypeMetadata<JTargetObject>;

/// <summary>
/// This class represents a local <c>java.lang.annotation.Target</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JTargetObject : JAnnotationObject<JTargetObject>, IInterfaceType<JTargetObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.TargetHash, 27);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JLocalObject.InterfaceView.CreateBuiltInMetadata<JTargetObject>(
			JTargetObject.typeInfo, InterfaceSet.AnnotationSet);

	static TypeMetadata IInterfaceType<JTargetObject>.Metadata => JTargetObject.typeMetadata;

	/// <inheritdoc/>
	private JTargetObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JTargetObject IInterfaceType<JTargetObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}