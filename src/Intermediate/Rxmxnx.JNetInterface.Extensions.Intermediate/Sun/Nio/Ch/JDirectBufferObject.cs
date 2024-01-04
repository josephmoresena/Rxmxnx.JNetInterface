namespace Rxmxnx.JNetInterface.Sun.Nio.Ch;

/// <summary>
/// This class represents a local <c>sun.nio.ch.DirectBuffer</c> instance.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class JDirectBufferObject : JInterfaceObject<JDirectBufferObject>, IInterfaceType<JDirectBufferObject>
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata metadata = JTypeMetadataBuilder<JDirectBufferObject>
	                                                          .Create(UnicodeClassNames.DirectBufferObject()).Build();

	static JDataTypeMetadata IDataType.Metadata => JDirectBufferObject.metadata;

	/// <inheritdoc/>
	private JDirectBufferObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	private JDirectBufferObject(JLocalObject jLocal) : base(jLocal) { }

	static JDirectBufferObject? IReferenceType<JDirectBufferObject>.Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JDirectBufferObject>(jLocal)) : default;
	static JDirectBufferObject? IReferenceType<JDirectBufferObject>.Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ?
			new(env, JLocalObject.Validate<JDirectBufferObject>(jGlobal, env)) :
			default;
}