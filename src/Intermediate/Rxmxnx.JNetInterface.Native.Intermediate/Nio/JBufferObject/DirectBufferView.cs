namespace Rxmxnx.JNetInterface.Nio;

public partial class JBufferObject
{
	/// <summary>
	/// Stores a <see ref="JBufferObject"/> instance which can be used with JNI Native IO features.
	/// </summary>
	/// <remarks>
	/// Constructor.
	/// </remarks>
	/// <param name="buffer">A <see cref="JBufferObject{TValue}"/> instance.</param>
	internal sealed class DirectBufferWrapper<TValue>(JBufferObject<TValue> buffer)
		: View<JBufferObject<TValue>>(buffer), IDirectBufferObject<TValue>, IInterfaceObject<JComparableObject>
		where TValue : unmanaged, IPrimitiveType<TValue>, IBinaryNumber<TValue>
	{
		IntPtr IDirectBufferObject.Address => this.Object.Address.GetValueOrDefault();
		Int64 IDirectBufferObject.Capacity => this.Object.Capacity;
		JBufferObject IDirectBufferObject.InternalBuffer => this.Object;
	}
}