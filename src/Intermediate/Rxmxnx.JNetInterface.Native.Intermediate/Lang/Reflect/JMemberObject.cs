namespace Rxmxnx.JNetInterface.Reflect;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Member</c> instance.
/// </summary>
public sealed class JMemberObject : JInterfaceObject<JMemberObject>, IInterfaceType<JMemberObject>
{
	/// <summary>
	/// class metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata metadata = JTypeMetadataBuilder<JMemberObject>
	                                                          .Create(UnicodeClassNames.MemberInterface()).Build();

	static JDataTypeMetadata IDataType.Metadata => JMemberObject.metadata;

	/// <inheritdoc/>
	private JMemberObject(JLocalObject jLocal) : base(jLocal) { }
	/// <inheritdoc/>
	private JMemberObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }

	/// <inheritdoc/>
	public static JMemberObject? Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JMemberObject>(jLocal)) : default;
	/// <inheritdoc/>
	public static JMemberObject? Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ? new(env, JLocalObject.Validate<JMemberObject>(jGlobal, env)) : default;
}