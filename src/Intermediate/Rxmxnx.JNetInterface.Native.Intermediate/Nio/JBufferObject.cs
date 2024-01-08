namespace Rxmxnx.JNetInterface.Nio;

/// <summary>
/// This class represents a local <c>java.nio.Buffer</c> instance.
/// </summary>
public partial class JBufferObject : JLocalObject, IClassType<JBufferObject>, ILocalObject
{
	/// <summary>
	/// Indicates whether current instance is a direct buffer.
	/// </summary>
	public Boolean IsDirect => this._isDirect ??= this.Environment.Functions.IsDirectBuffer(this);
	/// <summary>
	/// Buffer's capacity.
	/// </summary>
	public Int64 Capacity
		=> this._capacity ??= this.IsDirect ?
			this.Environment.NioFeature.GetDirectCapacity(this) :
			this.Environment.Functions.BufferCapacity(this);

	/// <summary>
	/// Direct buffer address.
	/// </summary>
	internal IntPtr? Address
		=> this._address ??= this.IsDirect ? this.Environment.NioFeature.GetDirectAddress(this) : default(IntPtr?);

	/// <inheritdoc/>
	internal JBufferObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef) { }

	/// <inheritdoc/>
	protected JBufferObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	protected JBufferObject(JLocalObject jLocal, JClassObject? jClass = default) : base(jLocal, jClass) { }

	ObjectMetadata ILocalObject.CreateMetadata() => this.CreateMetadata();

	/// <inheritdoc cref="JLocalObject.CreateMetadata()"/>
	protected new virtual BufferObjectMetadata CreateMetadata()
	{
		Boolean isDirect = this is IDirectBufferObject || this.IsDirect;
		return new(base.CreateMetadata()) { IsDirect = isDirect, Capacity = this.Capacity, Address = this.Address, };
	}

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
}

/// <summary>
/// This class represents a local <c>java.nio.Buffer</c> instance.
/// </summary>
public abstract class JBufferObject<TValue> : JBufferObject, IInterfaceObject<JComparableObject>
	where TValue : unmanaged, IPrimitiveType<TValue>, IBinaryNumber<TValue>
{
	/// <inheritdoc/>
	internal JBufferObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef) { }

	/// <inheritdoc/>
	protected JBufferObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	protected JBufferObject(JLocalObject jLocal, JClassObject? jClass = default) : base(jLocal, jClass) { }

	/// <summary>
	/// Creates a <see cref="IDirectBufferObject{TValue}"/> from current buffer if is direct.
	/// </summary>
	/// <returns>
	/// A <see cref="IDirectBufferObject{TValue}"/> instance if is direct; otherwise,
	/// <see langword="null"/>.
	/// </returns>
	public IDirectBufferObject<TValue>? AsDirectObject()
	{
		if (this is IDirectBufferObject<TValue> direct) return direct;
		return this.IsDirect ? new DirectBufferWrapper<TValue>(this) : default(IDirectBufferObject<TValue>?);
	}
}