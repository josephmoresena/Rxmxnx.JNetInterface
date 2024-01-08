namespace Rxmxnx.JNetInterface.Nio;

/// <summary>
/// This class represents a local <c>java.nio.DirectByteBuffer</c> instance.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public class JDirectByteBufferObject : JMappedByteBufferObject, IClassType<JDirectByteBufferObject>,
	IDirectBufferObject<JByte>
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly JClassTypeMetadata metadata = JTypeMetadataBuilder<JMappedByteBufferObject>
	                                                      .Create<JDirectByteBufferObject>(
		                                                      UnicodeClassNames.DirectByteBufferObject())
	                                                      .Implements<JComparableObject>()
	                                                      .Implements<JDirectBufferObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JDirectByteBufferObject.metadata;

	/// <summary>
	/// Internal memory.
	/// </summary>
	private readonly IFixedMemory.IDisposable? _memory;

	/// <summary>
	/// Indicates whether current instance is disposed.
	/// </summary>
	private Boolean _disposed;

	/// <inheritdoc/>
	internal JDirectByteBufferObject(JClassObject jClass, IFixedMemory.IDisposable memory, JObjectLocalRef localRef) :
		base(jClass, localRef)
		=> this._memory = memory;

	/// <inheritdoc/>
	protected JDirectByteBufferObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	protected JDirectByteBufferObject(JLocalObject jLocal, JClassObject? jClass = default) : base(jLocal, jClass) { }

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

	static JDirectByteBufferObject? IReferenceType<JDirectByteBufferObject>.Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JDirectByteBufferObject>(jLocal)) : default;
	static JDirectByteBufferObject? IReferenceType<JDirectByteBufferObject>.
		Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ?
			new(env, JLocalObject.Validate<JDirectByteBufferObject>(jGlobal, env)) :
			default;
}