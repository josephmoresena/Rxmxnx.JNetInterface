namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate Non-blocking I/O Java buffers through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct NioFunctionSet
{
	/// <summary>
	/// Pointer to <c>NewDirectByteBuffer</c> function.
	/// Allocates and returns a direct <c>java.nio.ByteBuffer</c> referring to the given block of memory.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, IntPtr, Int64, JObjectLocalRef> NewDirectByteBuffer;
	/// <summary>
	/// Pointer to <c>GetDirectBufferAddress</c> function.
	/// Fetches and returns the starting address of the memory region referenced by the given direct <c>java.nio.Buffer</c>.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, IntPtr> GetDirectBufferAddress;
	/// <summary>
	/// Pointer to <c>GetDirectBufferCapacity</c> function.
	/// Fetches and returns the capacity of the memory region referenced by the given direct <c>java.nio.Buffer</c>.
	/// </summary>
	/// <remarks>The capacity is the number of elements that the memory region contains.</remarks>
	public readonly delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, Int64> GetDirectBufferCapacity;
}