namespace Rxmxnx.JNetInterface.Nio;

using TypeMetadata = JClassTypeMetadata<JMappedByteBufferObject>;

/// <summary>
/// This class represents a local <c>java.nio.MappedByteBuffer</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public class JMappedByteBufferObject : JByteBufferObject, IClassType<JMappedByteBufferObject>
{
	/// <summary>
	/// Type information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.MappedByteBufferHash, 25);
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = JLocalObject.CreateBuiltInMetadata<JMappedByteBufferObject>(
		JMappedByteBufferObject.typeInfo, IClassType.GetMetadata<JByteBufferObject>(), JTypeModifier.Abstract);

	static TypeMetadata IClassType<JMappedByteBufferObject>.Metadata => JMappedByteBufferObject.typeMetadata;

	/// <inheritdoc/>
	private protected JMappedByteBufferObject(JClassObject jClass, JObjectLocalRef localRef) :
		base(jClass, localRef) { }

	/// <inheritdoc/>
	protected JMappedByteBufferObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JMappedByteBufferObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JMappedByteBufferObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JMappedByteBufferObject IClassType<JMappedByteBufferObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JMappedByteBufferObject IClassType<JMappedByteBufferObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JMappedByteBufferObject IClassType<JMappedByteBufferObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}