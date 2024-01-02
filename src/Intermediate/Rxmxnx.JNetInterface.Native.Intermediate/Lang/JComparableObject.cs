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
	                                                              .Create(UnicodeClassNames.ComparableInterface)
	                                                              .Build();

	static JDataTypeMetadata IDataType.Metadata => JComparableObject.typeMetadata;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	private JComparableObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	private JComparableObject(JLocalObject jLocal) : base(jLocal) { }

	/// <inheritdoc/>
	public static JComparableObject? Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JComparableObject>(jLocal)) : default;
	/// <inheritdoc/>
	public static JComparableObject? Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ?
			new(env, JLocalObject.Validate<JComparableObject>(jGlobal, env)) :
			default;
}