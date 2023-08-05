namespace Rxmxnx.JNetInterface.Reflect;

/// <summary>
/// This class represents a local <c>java.reflect.AnnotatedElement</c> instance.
/// </summary>
public class JAnnotatedElementObject : JInterfaceObject, IInterfaceType<JAnnotatedElementObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata typeMetadata = JTypeMetadataBuilder<JAnnotatedElementObject>
	                                                              .Create(UnicodeClassNames
		                                                                      .JAnnotatedElementInterfaceName)
	                                                              .WithSignature(
		                                                              UnicodeObjectSignatures
			                                                              .JAnnotatedElementObjectSignature).Build();

	static JDataTypeMetadata IDataType.Metadata => JAnnotatedElementObject.typeMetadata;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	public JAnnotatedElementObject(IEnvironment env, JGlobalBase jGlobal) : base(
		env, JLocalObject.Validate<JAnnotatedElementObject>(jGlobal, env)) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	protected JAnnotatedElementObject(JLocalObject jLocal) : base(jLocal) { }

	static JAnnotatedElementObject? IDataType<JAnnotatedElementObject>.Create(JObject? jObject)
		=> jObject is JLocalObject jLocal ? new(JLocalObject.Validate<JAnnotatedElementObject>(jLocal)) : default;
}