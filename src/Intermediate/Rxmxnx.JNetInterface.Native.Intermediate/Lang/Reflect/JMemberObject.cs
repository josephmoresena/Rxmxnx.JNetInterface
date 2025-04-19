namespace Rxmxnx.JNetInterface.Lang.Reflect;

using TypeMetadata = JInterfaceTypeMetadata<JMemberObject>;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Member</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public sealed class JMemberObject : JInterfaceObject<JMemberObject>, IInterfaceType<JMemberObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.MemberHash, 24);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JLocalObject.InterfaceView.CreateBuiltInMetadata<JMemberObject>(JMemberObject.typeInfo, InterfaceSet.Empty);

	static TypeMetadata IInterfaceType<JMemberObject>.Metadata => JMemberObject.typeMetadata;

	/// <inheritdoc/>
	private JMemberObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JMemberObject IInterfaceType<JMemberObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}