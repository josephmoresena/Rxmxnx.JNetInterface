namespace Rxmxnx.JNetInterface.Nio;

using TypeMetadata = JClassTypeMetadata<JCharBufferObject>;

/// <summary>
/// This class represents a local <c>java.nio.CharBuffer</c> instance.
/// </summary>
public class JCharBufferObject : JBufferObject<JChar>, IClassType<JCharBufferObject>,
	IInterfaceObject<JAppendableObject>, IInterfaceObject<JReadableObject>
{
	/// <summary>
	/// Type information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.CharBufferHash, 19);
	/// <summary>
	/// Type interfaces.
	/// </summary>
	private static readonly ImmutableHashSet<JInterfaceTypeMetadata> typeInterfaces =
	[
		IInterfaceType.GetMetadata<JComparableObject>(),
		IInterfaceType.GetMetadata<JCharSequenceObject>(),
		IInterfaceType.GetMetadata<JAppendableObject>(),
		IInterfaceType.GetMetadata<JReadableObject>(),
	];
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = JLocalObject.CreateBuiltInMetadata<JCharBufferObject>(
		JCharBufferObject.typeInfo, IClassType.GetMetadata<JBufferObject>(), JTypeModifier.Abstract,
		JCharBufferObject.typeInterfaces);

	static TypeMetadata IClassType<JCharBufferObject>.Metadata => JCharBufferObject.typeMetadata;

	/// <inheritdoc/>
	protected JCharBufferObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JCharBufferObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JCharBufferObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JCharBufferObject IClassType<JCharBufferObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JCharBufferObject IClassType<JCharBufferObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JCharBufferObject IClassType<JCharBufferObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}