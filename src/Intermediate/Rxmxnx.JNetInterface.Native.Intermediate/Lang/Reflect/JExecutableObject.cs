namespace Rxmxnx.JNetInterface.Reflect;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Executable</c> instance.
/// </summary>
public partial class JExecutableObject : JAccessibleObject, IClassType<JExecutableObject>,
	IInterfaceObject<JGenericDeclarationObject>, IInterfaceObject<JMemberObject>
{
	/// <summary>
	/// class metadata.
	/// </summary>
	private static readonly JClassTypeMetadata metadata = JTypeMetadataBuilder<JAccessibleObject>
	                                                      .Create<JAccessibleObject>(
		                                                      UnicodeClassNames.ExecutableObject(),
		                                                      JTypeModifier.Abstract)
	                                                      .Implements<JAnnotatedElementObject>()
	                                                      .Implements<JGenericDeclarationObject>()
	                                                      .Implements<JMemberObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JExecutableObject.metadata;

	/// <inheritdoc/>
	internal JExecutableObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef) { }
	/// <inheritdoc/>
	internal JExecutableObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	internal JExecutableObject(JLocalObject jLocal, JClassObject? jClass = default) : base(jLocal, jClass) { }

	static JExecutableObject? IReferenceType<JExecutableObject>.Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JExecutableObject>(jLocal)) : default;
	static JExecutableObject? IReferenceType<JExecutableObject>.Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ?
			new(env, JLocalObject.Validate<JExecutableObject>(jGlobal, env)) :
			default;
}