namespace Rxmxnx.JNetInterface.Nio;

public partial class JBufferObject
{
	/// <summary>
	/// Stores a <see cref="JBufferObject"/> instance which can be used with JNI Native IO features.
	/// </summary>
	internal sealed class DirectBufferWrapper<TValue> : View<JBufferObject<TValue>>, IDirectBufferObject<TValue>,
		IInterfaceObject<JComparableObject> where TValue : unmanaged, IPrimitiveType<TValue>, IBinaryNumber<TValue>
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="buffer">A <see cref="JBufferObject{TValue}"/> instance.</param>
		public DirectBufferWrapper(JBufferObject<TValue> buffer) : base(buffer) { }
		IntPtr IDirectBufferObject.Address => this.Object.Address.GetValueOrDefault();
		Int64 IDirectBufferObject.Capacity => this.Object.Capacity;
		JBufferObject IDirectBufferObject.InternalBuffer => this.Object;
	}
}