namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JInterfaceTypeMetadata<JComparableObject>;

/// <summary>
/// This class represents a local <c>java.lang.Comparable</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JComparableObject : JInterfaceObject<JComparableObject>, IInterfaceType<JComparableObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.ComparableHash, 20);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JLocalObject.InterfaceView.CreateBuiltInMetadata<JComparableObject>(
			JComparableObject.typeInfo, InterfaceSet.Empty);

	static TypeMetadata IInterfaceType<JComparableObject>.Metadata => JComparableObject.typeMetadata;

	/// <inheritdoc/>
	private JComparableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JComparableObject IInterfaceType<JComparableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}