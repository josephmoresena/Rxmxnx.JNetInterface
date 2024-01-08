namespace Rxmxnx.JNetInterface.Nio;

public partial class JBufferObject
{
	/// <summary>
	/// Stores a <see cref="JBufferObject"/> instance which can be used with JNI Native IO features.
	/// </summary>
	internal sealed record DirectBufferWrapper<TValue> : IViewObject, IDirectBufferObject<TValue>,
		IInterfaceObject<JComparableObject> where TValue : unmanaged, IPrimitiveType<TValue>, IBinaryNumber<TValue>
	{
		/// <summary>
		/// Internal <see cref="JBufferObject{TValue}"/> instance.
		/// </summary>
		private readonly JBufferObject<TValue> _buffer;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="buffer">A <see cref="JBufferObject{TValue}"/> instance.</param>
		public DirectBufferWrapper(JBufferObject<TValue> buffer) => this._buffer = buffer;
		IntPtr IDirectBufferObject.Address => this._buffer.Address.GetValueOrDefault();
		Int64 IDirectBufferObject.Capacity => this._buffer.Capacity;
		JBufferObject IDirectBufferObject.InternalBuffer => this._buffer;

		IObject IViewObject.Object => this._buffer;
	}
}