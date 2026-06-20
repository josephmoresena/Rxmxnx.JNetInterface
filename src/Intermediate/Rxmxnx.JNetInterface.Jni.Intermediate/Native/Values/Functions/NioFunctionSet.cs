// ReSharper disable ConvertIfStatementToReturnStatement

namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to NIO operations through JNI.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct NioFunctionSet
{
#if !ANDROID
	/// <summary>
	/// Function set for Windows Operating System.
	/// </summary>
	[FieldOffset(0)]
	private readonly Windows _windows;
#endif
	/// <summary>
	/// Function set for Unix-like Operating System.
	/// </summary>
	[FieldOffset(0)]
	private readonly Unix _unix;

	/// <summary>
	/// <c>GetObjectRefType</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JObjectLocalRef NewDirectByteBuffer(JEnvironmentRef envRef, IntPtr buffPtr, Int64 buffSize)
	{
#if !ANDROID
		if (SystemInfo.IsWindows)
			return this._windows.NewDirectByteBuffer(envRef, buffPtr, buffSize);
#endif
		return this._unix.NewDirectByteBuffer(envRef, buffPtr, buffSize);
	}
	/// <summary>
	/// <c>GetDirectBufferAddress</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public IntPtr GetDirectBufferAddress(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
#if !ANDROID
		if (SystemInfo.IsWindows)
			return this._windows.GetDirectBufferAddress(envRef, localRef);
#endif
		return this._unix.GetDirectBufferAddress(envRef, localRef);
	}
	/// <summary>
	/// <c>GetDirectBufferCapacity</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int64 GetDirectBufferCapacity(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
#if !ANDROID
		if (SystemInfo.IsWindows)
			return this._windows.GetDirectBufferCapacity(envRef, localRef);
#endif
		return this._unix.GetDirectBufferCapacity(envRef, localRef);
	}

#if !ANDROID
	/// <summary>
	/// Windows function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Windows
	{
		/// <summary>
		/// Pointer to <c>NewDirectByteBuffer</c> function.
		/// Allocates and returns a direct <c>java.nio.ByteBuffer</c> referring to the given block of memory.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, Int64, JObjectLocalRef>
			NewDirectByteBuffer;
		/// <summary>
		/// Pointer to <c>GetDirectBufferAddress</c> function.
		/// Fetches and returns the starting address of the memory region referenced by the given direct <c>java.nio.Buffer</c>.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, IntPtr> GetDirectBufferAddress;
		/// <summary>
		/// Pointer to <c>GetDirectBufferCapacity</c> function.
		/// Fetches and returns the capacity of the memory region referenced by the given direct <c>java.nio.Buffer</c>.
		/// </summary>
		/// <remarks>The capacity is the number of elements that the memory region contains.</remarks>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, Int64> GetDirectBufferCapacity;
	}
#endif

	/// <summary>
	/// Unix function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Unix
	{
		/// <summary>
		/// Pointer to <c>NewDirectByteBuffer</c> function.
		/// Allocates and returns a direct <c>java.nio.ByteBuffer</c> referring to the given block of memory.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, Int64, JObjectLocalRef> NewDirectByteBuffer;
		/// <summary>
		/// Pointer to <c>GetDirectBufferAddress</c> function.
		/// Fetches and returns the starting address of the memory region referenced by the given direct <c>java.nio.Buffer</c>.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, IntPtr> GetDirectBufferAddress;
		/// <summary>
		/// Pointer to <c>GetDirectBufferCapacity</c> function.
		/// Fetches and returns the capacity of the memory region referenced by the given direct <c>java.nio.Buffer</c>.
		/// </summary>
		/// <remarks>The capacity is the number of elements that the memory region contains.</remarks>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, Int64> GetDirectBufferCapacity;
	}
}