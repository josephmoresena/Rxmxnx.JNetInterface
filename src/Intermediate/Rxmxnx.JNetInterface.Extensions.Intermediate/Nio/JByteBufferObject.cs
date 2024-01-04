namespace Rxmxnx.JNetInterface.Nio;

/// <summary>
/// This class represents a local <c>java.nio.ByteBuffer</c> instance.
/// </summary>
public class JByteBufferObject : JBufferObject<JByte>, IClassType<JByteBufferObject>,
	IInterfaceImplementation<JByteBufferObject, JComparableObject>
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly JClassTypeMetadata metadata = JTypeMetadataBuilder<JBufferObject>
	                                                      .Create<JByteBufferObject>(
		                                                      UnicodeClassNames.ByteBufferObject(),
		                                                      JTypeModifier.Abstract).Implements<JComparableObject>()
	                                                      .Build();

	static JDataTypeMetadata IDataType.Metadata => JByteBufferObject.metadata;
	/// <inheritdoc/>
	internal JByteBufferObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef) { }

	/// <inheritdoc/>
	protected JByteBufferObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	protected JByteBufferObject(JLocalObject jLocal, JClassObject? jClass = default) : base(jLocal, jClass) { }

	static JByteBufferObject? IReferenceType<JByteBufferObject>.Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JByteBufferObject>(jLocal)) : default;
	static JByteBufferObject? IReferenceType<JByteBufferObject>.Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ?
			new(env, JLocalObject.Validate<JByteBufferObject>(jGlobal, env)) :
			default;
}