namespace Rxmxnx.JNetInterface.Reflect;

/// <summary>
/// This class represents a local <c>java.reflect.GenericDeclaration</c> instance.
/// </summary>
public sealed class JGenericDeclarationObject : JInterfaceObject<JGenericDeclarationObject>,
	IInterfaceType<JGenericDeclarationObject>,
	IInterfaceImplementation<JGenericDeclarationObject, JAnnotatedElementObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata typeMetadata = JTypeMetadataBuilder<JGenericDeclarationObject>
	                                                              .Create("java/reflect/GenericDeclaration"u8)
	                                                              .Extends<JAnnotatedElementObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JGenericDeclarationObject.typeMetadata;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	private JGenericDeclarationObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	private JGenericDeclarationObject(JLocalObject jLocal) : base(jLocal) { }

	/// <inheritdoc/>
	public static JGenericDeclarationObject? Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JGenericDeclarationObject>(jLocal)) : default;
	/// <inheritdoc/>
	public static JGenericDeclarationObject? Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ?
			new(env, JLocalObject.Validate<JGenericDeclarationObject>(jGlobal, env)) :
			default;
}