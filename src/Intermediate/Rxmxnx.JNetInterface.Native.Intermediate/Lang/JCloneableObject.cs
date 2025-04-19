namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JInterfaceTypeMetadata<JCloneableObject>;

/// <summary>
/// This class represents a local <c>java.lang.Cloneable</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public sealed class JCloneableObject : JInterfaceObject<JCloneableObject>, IInterfaceType<JCloneableObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.CloneableHash, 19);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JLocalObject.InterfaceView.CreateBuiltInMetadata<JCloneableObject>(
			JCloneableObject.typeInfo, InterfaceSet.Empty);

	static TypeMetadata IInterfaceType<JCloneableObject>.Metadata => JCloneableObject.typeMetadata;

	/// <inheritdoc/>
	private JCloneableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JCloneableObject IInterfaceType<JCloneableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}