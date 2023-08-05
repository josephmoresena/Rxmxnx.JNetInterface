namespace Rxmxnx.JNetInterface.Reflect;

/// <summary>
/// This class represents a local <c>java.reflect.Type</c> instance.
/// </summary>
public class JTypeObject : JInterfaceObject, IInterfaceType<JTypeObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata typeMetadata = JTypeMetadataBuilder<JTypeObject>
	                                                              .Create(UnicodeClassNames.JTypeInterfaceName)
	                                                              .WithSignature(
		                                                              UnicodeObjectSignatures.JTypeObjectSignature)
	                                                              .Build();

	static JDataTypeMetadata IDataType.Metadata => JTypeObject.typeMetadata;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	public JTypeObject(IEnvironment env, JGlobalBase jGlobal) : base(
		env, JLocalObject.Validate<JTypeObject>(jGlobal, env)) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	protected JTypeObject(JLocalObject jLocal) : base(jLocal) { }

	static JTypeObject? IDataType<JTypeObject>.Create(JObject? jObject)
		=> jObject is JLocalObject jLocal ? new(JLocalObject.Validate<JTypeObject>(jLocal)) : default;
}