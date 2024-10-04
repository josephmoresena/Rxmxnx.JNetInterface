namespace Rxmxnx.JNetInterface.Nio;

using TypeMetadata = JClassTypeMetadata<JLongBufferObject>;

/// <summary>
/// This class represents a local <c>java.nio.LongBuffer</c> instance.
/// </summary>
public class JLongBufferObject : JBufferObject<JLong>, IClassType<JLongBufferObject>
{
	/// <summary>
	/// Type information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.LongBufferHash, 20);
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = JLocalObject.CreateBuiltInMetadata<JLongBufferObject>(
		JLongBufferObject.typeInfo, IClassType.GetMetadata<JBufferObject>(), JTypeModifier.Abstract,
		InterfaceSet.ComparableSet);

	static TypeMetadata IClassType<JLongBufferObject>.Metadata => JLongBufferObject.typeMetadata;

	/// <inheritdoc/>
	protected JLongBufferObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JLongBufferObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JLongBufferObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JLongBufferObject IClassType<JLongBufferObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JLongBufferObject IClassType<JLongBufferObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JLongBufferObject IClassType<JLongBufferObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}