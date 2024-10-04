namespace Rxmxnx.JNetInterface.Nio;

using TypeMetadata = JClassTypeMetadata<JFloatBufferObject>;

/// <summary>
/// This class represents a local <c>java.nio.FloatBuffer</c> instance.
/// </summary>
public class JFloatBufferObject : JBufferObject<JFloat>, IClassType<JFloatBufferObject>
{
	/// <summary>
	/// Type information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.FloatBufferHash, 20);
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = JLocalObject.CreateBuiltInMetadata<JFloatBufferObject>(
		JFloatBufferObject.typeInfo, IClassType.GetMetadata<JBufferObject>(), JTypeModifier.Abstract,
		InterfaceSet.ComparableSet);

	static TypeMetadata IClassType<JFloatBufferObject>.Metadata => JFloatBufferObject.typeMetadata;

	/// <inheritdoc/>
	protected JFloatBufferObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JFloatBufferObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JFloatBufferObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JFloatBufferObject IClassType<JFloatBufferObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JFloatBufferObject IClassType<JFloatBufferObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JFloatBufferObject IClassType<JFloatBufferObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}