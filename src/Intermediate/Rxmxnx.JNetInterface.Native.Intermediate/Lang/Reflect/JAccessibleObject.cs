namespace Rxmxnx.JNetInterface.Reflect;

/// <summary>
/// This class represents a local <c>java.lang.reflect.AccessibleObject</c> instance.
/// </summary>
public class JAccessibleObject : JLocalObject, IClassType<JAccessibleObject>, IInterfaceObject<JAnnotatedElementObject>
{
	/// <summary>
	/// class metadata.
	/// </summary>
	private static readonly JClassTypeMetadata metadata = JTypeMetadataBuilder<JAccessibleObject>
	                                                      .Create(UnicodeClassNames.AccessibleObject())
	                                                      .Implements<JAnnotatedElementObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JAccessibleObject.metadata;

	/// <inheritdoc/>
	internal JAccessibleObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef) { }
	/// <inheritdoc/>
	internal JAccessibleObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	internal JAccessibleObject(JLocalObject jLocal, JClassObject? jClass = default) : base(jLocal, jClass) { }

	static JAccessibleObject? IReferenceType<JAccessibleObject>.Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JAccessibleObject>(jLocal)) : default;
	static JAccessibleObject? IReferenceType<JAccessibleObject>.Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ?
			new(env, JLocalObject.Validate<JAccessibleObject>(jGlobal, env)) :
			default;
}