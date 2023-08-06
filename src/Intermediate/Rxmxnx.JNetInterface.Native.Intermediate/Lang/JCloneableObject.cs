namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Cloneable</c> instance.
/// </summary>
public sealed class JCloneableObject : JInterfaceObject<JCloneableObject>, IInterfaceType<JCloneableObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata typeMetadata = JTypeMetadataBuilder<JCloneableObject>
	                                                              .Create(UnicodeClassNames.JCloneableInterfaceName)
	                                                              .WithSignature(
		                                                              UnicodeObjectSignatures.JCloneableObjectSignature)
	                                                              .Build();

	static JDataTypeMetadata IDataType.Metadata => JCloneableObject.typeMetadata;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	public JCloneableObject(IEnvironment env, JGlobalBase jGlobal) : base(
		env, JLocalObject.Validate<JCloneableObject>(jGlobal, env)) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	private JCloneableObject(JLocalObject jLocal) : base(jLocal) { }

	/// <inheritdoc/>
	static JCloneableObject? IDataType<JCloneableObject>.Create(JObject? jObject)
		=> jObject is JLocalObject jLocal ? new(JLocalObject.Validate<JCloneableObject>(jLocal)) : default;
}