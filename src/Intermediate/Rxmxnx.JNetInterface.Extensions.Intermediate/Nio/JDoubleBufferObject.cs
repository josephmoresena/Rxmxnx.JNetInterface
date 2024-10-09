namespace Rxmxnx.JNetInterface.Nio;

using TypeMetadata = JClassTypeMetadata<JDoubleBufferObject>;

/// <summary>
/// This class represents a local <c>java.nio.DoubleBuffer</c> instance.
/// </summary>
public class JDoubleBufferObject : JBufferObject<JDouble>, IClassType<JDoubleBufferObject>
{
	/// <summary>
	/// Type information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.DoubleBufferHash, 21);
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = JLocalObject.CreateBuiltInMetadata<JDoubleBufferObject>(
		JDoubleBufferObject.typeInfo, IClassType.GetMetadata<JBufferObject>(), JTypeModifier.Abstract,
		InterfaceSet.ComparableSet);

	static TypeMetadata IClassType<JDoubleBufferObject>.Metadata => JDoubleBufferObject.typeMetadata;

	/// <inheritdoc/>
	protected JDoubleBufferObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JDoubleBufferObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JDoubleBufferObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JDoubleBufferObject IClassType<JDoubleBufferObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JDoubleBufferObject IClassType<JDoubleBufferObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JDoubleBufferObject IClassType<JDoubleBufferObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}