namespace Rxmxnx.JNetInterface.Lang.Reflect;

using TypeMetadata = JInterfaceTypeMetadata<JMemberObject>;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Member</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JMemberObject : JInterfaceObject<JMemberObject>, IInterfaceType<JMemberObject>
{
	/// <summary>
	/// class metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JMemberObject>
	                                                    .Create("java/lang/reflect/Member"u8).Build();

	static TypeMetadata IInterfaceType<JMemberObject>.Metadata => JMemberObject.typeMetadata;

	/// <inheritdoc/>
	private JMemberObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JMemberObject IInterfaceType<JMemberObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}