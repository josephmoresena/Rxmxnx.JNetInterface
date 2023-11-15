namespace Rxmxnx.JNetInterface.Io;

/// <summary>
/// This class represents a local <c>java.io.Serializable</c> instance.
/// </summary>
public sealed class JSerializableObject : JInterfaceObject<JSerializableObject>, IInterfaceType<JSerializableObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata typeMetadata = JTypeMetadataBuilder<JSerializableObject>
	                                                              .Create(UnicodeClassNames.JSerializableInterfaceName)
	                                                              .WithSignature(
		                                                              UnicodeObjectSignatures
			                                                              .JSerializableObjectSignature).Build();

	static JDataTypeMetadata IDataType.Metadata => JSerializableObject.typeMetadata;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	public JSerializableObject(IEnvironment env, JGlobalBase jGlobal) : base(
		env, JLocalObject.Validate<JSerializableObject>(jGlobal, env)) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	private JSerializableObject(JLocalObject jLocal) : base(jLocal) { }

	static JSerializableObject? IDataType<JSerializableObject>.Create(JObject? jObject)
		=> jObject is JLocalObject jLocal ? new(JLocalObject.Validate<JSerializableObject>(jLocal)) : default;
}