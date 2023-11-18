namespace Rxmxnx.JNetInterface.Reflect;

/// <summary>
/// This class represents a local <c>java.reflect.Type</c> instance.
/// </summary>
public sealed class JTypeObject : JInterfaceObject<JTypeObject>, IInterfaceType<JTypeObject>
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
	private JTypeObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	private JTypeObject(JLocalObject jLocal) : base(jLocal) { }

	/// <inheritdoc/>
	public static JTypeObject? Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JTypeObject>(jLocal)) : default;
	/// <inheritdoc/>
	public static JTypeObject? Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ? new(env, JLocalObject.Validate<JTypeObject>(jGlobal, env)) : default;
}