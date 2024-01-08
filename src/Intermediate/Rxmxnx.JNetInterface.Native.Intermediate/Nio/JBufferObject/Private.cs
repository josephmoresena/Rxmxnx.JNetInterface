namespace Rxmxnx.JNetInterface.Nio;

public partial class JBufferObject
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly JClassTypeMetadata metadata = JTypeMetadataBuilder<JBufferObject>
	                                                      .Create(UnicodeClassNames.BufferObject(),
	                                                              JTypeModifier.Abstract).Build();

	static JDataTypeMetadata IDataType.Metadata => JBufferObject.metadata;

	/// <inheritdoc cref="JBufferObject.Address"/>
	private IntPtr? _address;
	/// <inheritdoc cref="JBufferObject.Capacity"/>
	private Int64? _capacity;

	/// <inheritdoc cref="JBufferObject.IsDirect"/>
	private Boolean? _isDirect;

	static JBufferObject? IReferenceType<JBufferObject>.Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JBufferObject>(jLocal)) : default;
	static JBufferObject? IReferenceType<JBufferObject>.Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ? new(env, JLocalObject.Validate<JBufferObject>(jGlobal, env)) : default;
}