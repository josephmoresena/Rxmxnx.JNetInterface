namespace Rxmxnx.JNetInterface.Nio;

using TypeMetadata = JClassTypeMetadata<JDirectByteBufferObject>;

/// <summary>
/// This class represents a local <c>java.nio.DirectByteBuffer</c> instance.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JDirectByteBufferObject : JMappedByteBufferObject, IClassType<JDirectByteBufferObject>,
	IDirectBufferObject<JByte>
{
	/// <summary>
	/// Type information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.DirectByteBufferHash, 25);
	/// <summary>
	/// Type interfaces.
	/// </summary>
	private static readonly ImmutableHashSet<JInterfaceTypeMetadata> typeInterfaces =
	[
		IInterfaceType.GetMetadata<JDirectBufferObject>(),
	];
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = JLocalObject.CreateBuiltInMetadata<JDirectByteBufferObject>(
		JDirectByteBufferObject.typeInfo, IClassType.GetMetadata<JMappedByteBufferObject>(), JTypeModifier.Extensible,
		JDirectByteBufferObject.typeInterfaces);

	static TypeMetadata IClassType<JDirectByteBufferObject>.Metadata => JDirectByteBufferObject.typeMetadata;

	/// <summary>
	/// Internal memory.
	/// </summary>
	private readonly IFixedMemory.IDisposable? _memory;

	/// <summary>
	/// Indicates whether the current instance is disposed.
	/// </summary>
	private Boolean _disposed;

	/// <inheritdoc/>
	internal JDirectByteBufferObject(JClassObject jClass, IFixedMemory.IDisposable memory, JObjectLocalRef localRef) :
		base(jClass, localRef)
	{
		this._memory = memory;
		JBufferObject.InitializeDirect(this, this._memory.Pointer, this._memory.Bytes.Length);
	}

	/// <inheritdoc/>
	protected JDirectByteBufferObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JDirectByteBufferObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JDirectByteBufferObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	/// <inheritdoc cref="JBufferObject.Address"/>
	public new IntPtr Address => base.Address.GetValueOrDefault();

	JBufferObject IDirectBufferObject.InternalBuffer => this;
	IFixedContext<JByte> IDirectBufferObject<JByte>.GetFixedContext()
		=> this._memory as IFixedContext<JByte> ??
			(this._memory?.AsBinaryContext().Transformation<JByte>(out IFixedMemory _) ??
				((ValPtr<JByte>)this.Address).GetUnsafeFixedContext((Int32)this.Capacity));

	/// <inheritdoc/>
	protected override void Dispose(Boolean disposing)
	{
		base.Dispose(disposing);
		if (this._disposed) return;
		this._disposed = true;
		this._memory?.Dispose();
	}

	static JDirectByteBufferObject IClassType<JDirectByteBufferObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JDirectByteBufferObject IClassType<JDirectByteBufferObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JDirectByteBufferObject IClassType<JDirectByteBufferObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}