namespace Rxmxnx.JNetInterface.Nio;

/// <summary>
/// This class represents a local <c>java.nio.Buffer</c> instance.
/// </summary>
public partial class JBufferObject : JLocalObject, IClassType<JBufferObject>
{
	/// <summary>
	/// Indicates whether the current instance is a direct buffer.
	/// </summary>
	public Boolean IsDirect => this._isDirect ??= this.Environment.FunctionSet.IsDirectBuffer(this);
	/// <summary>
	/// Buffer's capacity.
	/// </summary>
	public Int64 Capacity
		=> this._capacity ??= this.IsDirect ?
			this.Environment.NioFeature.GetDirectCapacity(this) :
			this.Environment.FunctionSet.BufferCapacity(this);

	/// <summary>
	/// Direct buffer address.
	/// </summary>
	internal IntPtr? Address
		=> this._address ??= this.IsDirect ? this.Environment.NioFeature.GetDirectAddress(this) : default(IntPtr?);

	/// <inheritdoc/>
	internal JBufferObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef) { }

	/// <inheritdoc/>
	protected JBufferObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JBufferObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JBufferObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	ObjectMetadata ILocalObject.CreateMetadata() => this.CreateMetadata();

	/// <inheritdoc cref="JLocalObject.CreateMetadata()"/>
	protected new virtual BufferObjectMetadata CreateMetadata()
		=> new(base.CreateMetadata())
		{
			IsDirect = JBufferObject.IsDirectBuffer(this), Capacity = this.Capacity, Address = this.Address,
		};

	/// <inheritdoc/>
	protected override void ProcessMetadata(ObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is not BufferObjectMetadata bufferMetadata)
			return;
		this._isDirect ??= bufferMetadata.IsDirect;
		this._capacity ??= bufferMetadata.Capacity;
		this._address ??= bufferMetadata.Address;
	}

	/// <summary>
	/// Indicates whether <paramref name="jBuffer"/> is a direct buffer.
	/// </summary>
	/// <param name="jBuffer">A <see cref="JBufferObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jBuffer"/> is direct; otherwise, <see langword="false"/>.
	/// </returns>
	public static Boolean IsDirectBuffer(JBufferObject jBuffer) => jBuffer is IDirectBufferObject || jBuffer.IsDirect;

	/// <summary>
	/// Creates a <see cref="IDirectBufferObject{TValue}"/> from <paramref name="jBuffer"/> if is direct.
	/// </summary>
	/// <typeparam name="TValue">Primitive type of buffer.</typeparam>
	/// <param name="jBuffer">A <see cref="JBufferObject"/> instance.</param>
	/// <returns>
	/// A <see cref="IDirectBufferObject{TValue}"/> instance if <paramref name="jBuffer"/> is direct; otherwise,
	/// <see langword="null"/>.
	/// </returns>
	private protected static IDirectBufferObject<TValue>? AsDirect<TValue>(JBufferObject<TValue> jBuffer)
		where TValue : unmanaged, IPrimitiveType<TValue>, IBinaryNumber<TValue>
	{
		if (jBuffer is IDirectBufferObject<TValue> direct) return direct;
		return jBuffer.IsDirect ? new DirectBufferWrapper<TValue>(jBuffer) : default(IDirectBufferObject<TValue>?);
	}
	/// <summary>
	/// Initialize <paramref name="jBuffer"/> with direct information.
	/// </summary>
	/// <typeparam name="TValue">Primitive type of buffer.</typeparam>
	/// <param name="jBuffer">A <see cref="JBufferObject"/> instance.</param>
	/// <param name="address">Direct buffer address.</param>
	/// <param name="capacity">Direct buffer capacity.</param>
	private protected static void InitializeDirect<TValue>(JBufferObject<TValue> jBuffer, IntPtr address,
		Int32 capacity) where TValue : unmanaged, IPrimitiveType<TValue>, IBinaryNumber<TValue>
	{
		jBuffer._isDirect = true;
		jBuffer._address = address;
		jBuffer._capacity = capacity;
	}
}

/// <summary>
/// This class represents a local <c>java.nio.Buffer</c> instance.
/// </summary>
/// <typeparam name="TValue">Primitive type of buffer.</typeparam>
public abstract class JBufferObject<TValue> : JBufferObject, IInterfaceObject<JComparableObject>
	where TValue : unmanaged, IPrimitiveType<TValue>, IBinaryNumber<TValue>
{
	/// <inheritdoc/>
	private protected JBufferObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef) { }

	/// <inheritdoc/>
	protected JBufferObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JBufferObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JBufferObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	/// <summary>
	/// Creates a <see cref="IDirectBufferObject{TValue}"/> from current buffer if is direct.
	/// </summary>
	/// <returns>
	/// A <see cref="IDirectBufferObject{TValue}"/> instance if is direct; otherwise,
	/// <see langword="null"/>.
	/// </returns>
	public IDirectBufferObject<TValue>? AsDirect() => JBufferObject.AsDirect(this);
}