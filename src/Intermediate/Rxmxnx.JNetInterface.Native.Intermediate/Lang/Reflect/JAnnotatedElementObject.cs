namespace Rxmxnx.JNetInterface.Reflect;

/// <summary>
/// This class represents a local <c>java.reflect.AnnotatedElement</c> instance.
/// </summary>
public sealed class JAnnotatedElementObject : JInterfaceObject<JAnnotatedElementObject>,
	IInterfaceType<JAnnotatedElementObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata typeMetadata = JTypeMetadataBuilder<JAnnotatedElementObject>
	                                                              .Create(UnicodeClassNames.AnnotatedElementInterface())
	                                                              .Build();

	static JDataTypeMetadata IDataType.Metadata => JAnnotatedElementObject.typeMetadata;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	private JAnnotatedElementObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	private JAnnotatedElementObject(JLocalObject jLocal) : base(jLocal) { }

	/// <inheritdoc/>
	public static JAnnotatedElementObject? Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JAnnotatedElementObject>(jLocal)) : default;
	/// <inheritdoc/>
	public static JAnnotatedElementObject? Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ?
			new(env, JLocalObject.Validate<JAnnotatedElementObject>(jGlobal, env)) :
			default;
}