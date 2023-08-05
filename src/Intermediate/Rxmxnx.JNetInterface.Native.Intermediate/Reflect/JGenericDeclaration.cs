namespace Rxmxnx.JNetInterface.Reflect;

/// <summary>
/// This class represents a local <c>java.reflect.GenericDeclaration</c> instance.
/// </summary>
public class JGenericDeclarationObject : JAnnotatedElementObject, IInterfaceType<JGenericDeclarationObject>,
	IInterfaceImplementation<JGenericDeclarationObject, JAnnotatedElementObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata typeMetadata = JTypeMetadataBuilder<JGenericDeclarationObject>
	                                                              .Create(UnicodeClassNames
		                                                                      .JGenericDeclarationInterfaceName)
	                                                              .WithSignature(
		                                                              UnicodeObjectSignatures
			                                                              .JGenericDeclarationObjectSignature)
	                                                              .AppendInterface<JAnnotatedElementObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JGenericDeclarationObject.typeMetadata;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	public JGenericDeclarationObject(IEnvironment env, JGlobalBase jGlobal) : base(
		env, JLocalObject.Validate<JGenericDeclarationObject>(jGlobal, env)) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	protected JGenericDeclarationObject(JLocalObject jLocal) : base(jLocal) { }

	static JGenericDeclarationObject? IDataType<JGenericDeclarationObject>.Create(JObject? jObject)
		=> jObject is JLocalObject jLocal ? new(JLocalObject.Validate<JCloneableObject>(jLocal)) : default;
}