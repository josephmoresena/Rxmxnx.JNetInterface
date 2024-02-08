namespace Rxmxnx.JNetInterface.Reflect;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Member</c> instance.
/// </summary>
public sealed class JMemberObject : JInterfaceObject<JMemberObject>, IInterfaceType<JMemberObject>
{
	/// <summary>
	/// class metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata<JMemberObject> metadata = JTypeMetadataBuilder<JMemberObject>
	                                                                         .Create(
		                                                                         UnicodeClassNames.MemberInterface())
	                                                                         .Build();

	static JInterfaceTypeMetadata<JMemberObject> IInterfaceType<JMemberObject>.Metadata => JMemberObject.metadata;

	/// <inheritdoc/>
	private JMemberObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JMemberObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JMemberObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JMemberObject IReferenceType<JMemberObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JMemberObject IReferenceType<JMemberObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JMemberObject IReferenceType<JMemberObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}