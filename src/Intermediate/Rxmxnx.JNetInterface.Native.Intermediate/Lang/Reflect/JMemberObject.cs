namespace Rxmxnx.JNetInterface.Lang.Reflect;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Member</c> instance.
/// </summary>
public sealed class JMemberObject : JInterfaceObject<JMemberObject>, IInterfaceType<JMemberObject>
{
	/// <summary>
	/// class metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata<JMemberObject> metadata = TypeMetadataBuilder<JMemberObject>
	                                                                         .Create(
		                                                                         UnicodeClassNames.MemberInterface())
	                                                                         .Build();

	static JInterfaceTypeMetadata<JMemberObject> IInterfaceType<JMemberObject>.Metadata => JMemberObject.metadata;

	/// <inheritdoc/>
	private JMemberObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JMemberObject IInterfaceType<JMemberObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}