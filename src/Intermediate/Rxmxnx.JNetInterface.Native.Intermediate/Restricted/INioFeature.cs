namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// This interface exposes JNI Native I/O feature.
/// </summary>
public interface INioFeature
{
	/// <summary>
	/// Creates a direct <see cref="JBufferObject{TValue}"/> instance.
	/// </summary>
	/// <typeparam name="TValue">Created buffer elements type.</typeparam>
	/// <param name="memory">A <see cref="IFixedMemory{TValue}"/> instance.</param>
	/// <returns>A direct <see cref="JBufferObject{TValue}"/> instance.</returns>
	JBufferObject<TValue> NewDirectByteBuffer<TValue>(IFixedMemory<TValue>.IDisposable memory)
		where TValue : unmanaged, IPrimitiveType<TValue>, IBinaryNumber<TValue>;
	/// <summary>
	/// Retrieves the starting address of the memory region referenced by <paramref name="buffer"/>.
	/// </summary>
	/// <param name="buffer">A <see cref="JBufferObject"/> instance.</param>
	/// <returns>The starting address of the memory region referenced by <paramref name="buffer"/></returns>
	IntPtr GetAddress(JBufferObject buffer);
	/// <summary>
	/// Retrieves the capacity of the memory region referenced by <paramref name="buffer"/>.
	/// </summary>
	/// <typeparam name="TValue"><paramref name="buffer"/> elements type.</typeparam>
	/// <param name="buffer">A <see cref="JBufferObject"/> instance.</param>
	/// <returns>The capacity of the memory region referenced by <paramref name="buffer"/>.</returns>
	Int32 GetCapacity<TValue>(JBufferObject<TValue> buffer)
		where TValue : unmanaged, IPrimitiveType<TValue>, IBinaryNumber<TValue>;
}