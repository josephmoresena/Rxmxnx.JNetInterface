namespace Rxmxnx.JNetInterface.Nio;

/// <summary>
/// This class represents a local <c>java.nio.MappedByteBuffer</c> instance.
/// </summary>
public class JMappedByteBufferObject : JByteBufferObject, IClassType<JMappedByteBufferObject>,
	IInterfaceImplementation<JMappedByteBufferObject, JComparableObject>
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly JClassTypeMetadata metadata = JTypeMetadataBuilder<JByteBufferObject>
	                                                      .Create<JMappedByteBufferObject>(
		                                                      UnicodeClassNames.MappedByteBufferObject(),
		                                                      JTypeModifier.Abstract).Implements<JComparableObject>()
	                                                      .Build();

	static JDataTypeMetadata IDataType.Metadata => JMappedByteBufferObject.metadata;

	/// <inheritdoc/>
	internal JMappedByteBufferObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef) { }

	/// <inheritdoc/>
	protected JMappedByteBufferObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	protected JMappedByteBufferObject(JLocalObject jLocal, JClassObject? jClass = default) : base(jLocal, jClass) { }

	static JMappedByteBufferObject? IReferenceType<JMappedByteBufferObject>.Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JMappedByteBufferObject>(jLocal)) : default;
	static JMappedByteBufferObject? IReferenceType<JMappedByteBufferObject>.
		Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ?
			new(env, JLocalObject.Validate<JMappedByteBufferObject>(jGlobal, env)) :
			default;
}