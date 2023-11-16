namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Comparable</c> instance.
/// </summary>
public sealed class JComparableObject : JInterfaceObject<JComparableObject>, IInterfaceType<JComparableObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata typeMetadata = JTypeMetadataBuilder<JComparableObject>
	                                                              .Create(UnicodeClassNames.JComparableInterfaceName)
	                                                              .WithSignature(
		                                                              UnicodeObjectSignatures
			                                                              .JComparableObjectSignature).Build();

	static JDataTypeMetadata IDataType.Metadata => JComparableObject.typeMetadata;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	public JComparableObject(IEnvironment env, JGlobalBase jGlobal) : base(
		env, JLocalObject.Validate<JComparableObject>(jGlobal, env)) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	private JComparableObject(JLocalObject jLocal) : base(jLocal) { }

	/// <inheritdoc/>
	static JComparableObject? IReferenceType<JComparableObject>.Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JComparableObject>(jLocal)) : default;
}