namespace Rxmxnx.JNetInterface.Nio;

using TypeMetadata = JClassTypeMetadata<JBufferObject>;

public partial class JBufferObject
{
	/// <summary>
	/// Type information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.BufferHash, 15);
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JLocalObject.CreateBuiltInMetadata<JBufferObject>(JBufferObject.typeInfo, JTypeModifier.Abstract);

	static TypeMetadata IClassType<JBufferObject>.Metadata => JBufferObject.typeMetadata;

	/// <inheritdoc cref="JBufferObject.Address"/>
	private IntPtr? _address;
	/// <inheritdoc cref="JBufferObject.Capacity"/>
	private Int64? _capacity;

	/// <inheritdoc cref="JBufferObject.IsDirect"/>
	private Boolean? _isDirect;

	static JBufferObject IClassType<JBufferObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JBufferObject IClassType<JBufferObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JBufferObject IClassType<JBufferObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}