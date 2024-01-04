namespace Rxmxnx.JNetInterface.Nio;

/// <summary>
/// This class represents a local <c>java.nio.Buffer</c> instance.
/// </summary>
public class JBufferObject : JLocalObject, IClassType<JBufferObject>,
	IInterfaceImplementation<JBufferObject, JComparableObject>
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly JClassTypeMetadata metadata = JTypeMetadataBuilder<JBufferObject>
	                                                      .Create(UnicodeClassNames.BufferObject(),
	                                                              JTypeModifier.Abstract)
	                                                      .Implements<JComparableObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JBufferObject.metadata;

	/// <inheritdoc/>
	internal JBufferObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef) { }

	/// <inheritdoc/>
	protected JBufferObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	protected JBufferObject(JLocalObject jLocal, JClassObject? jClass = default) : base(jLocal, jClass) { }

	static JBufferObject? IReferenceType<JBufferObject>.Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JBufferObject>(jLocal)) : default;
	static JBufferObject? IReferenceType<JBufferObject>.Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ? new(env, JLocalObject.Validate<JBufferObject>(jGlobal, env)) : default;
}

/// <summary>
/// This class represents a local <c>java.nio.Buffer</c> instance.
/// </summary>
public abstract class JBufferObject<TValue> : JBufferObject
	where TValue : unmanaged, IPrimitiveType<TValue>, IBinaryNumber<TValue>
{
	/// <inheritdoc/>
	internal JBufferObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef) { }

	/// <inheritdoc/>
	protected JBufferObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	protected JBufferObject(JLocalObject jLocal, JClassObject? jClass = default) : base(jLocal, jClass) { }
}