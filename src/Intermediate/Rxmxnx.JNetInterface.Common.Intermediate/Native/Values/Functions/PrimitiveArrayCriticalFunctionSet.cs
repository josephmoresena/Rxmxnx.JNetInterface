namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate critical memory in Java arrays through JNI.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct PrimitiveArrayCriticalFunctionSet
{
	/// <summary>
	/// Function set for Windows Operating System.
	/// </summary>
	[FieldOffset(0)]
	private readonly Windows _windows;
	/// <summary>
	/// Function set for Unix-like Operating System.
	/// </summary>
	[FieldOffset(0)]
	private readonly Unix _unix;

	/// <summary>
	/// <c>GetPrimitiveArrayCritical</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public IntPtr GetPrimitiveArrayCritical(JEnvironmentRef envRef, JArrayLocalRef arrayRef, out JBoolean isCopy)
	{
		fixed (JBoolean* isCopyPtr = &isCopy)
		{
			return SystemInfo.IsWindows ?
				this._windows.GetPrimitiveArrayCritical(envRef, arrayRef, isCopyPtr) :
				this._unix.GetPrimitiveArrayCritical(envRef, arrayRef, isCopyPtr);
		}
	}
	/// <summary>
	/// <c>ReleasePrimitiveArrayCritical</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void ReleasePrimitiveArrayCritical(JEnvironmentRef envRef, JArrayLocalRef arrayRef, IntPtr dataPtr,
		JReleaseMode mode)
	{
		if (SystemInfo.IsWindows)
			this._windows.ReleasePrimitiveArrayCritical(envRef, arrayRef, dataPtr, mode);
		else
			this._unix.ReleasePrimitiveArrayCritical(envRef, arrayRef, dataPtr, mode);
	}

	/// <summary>
	/// Windows function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Windows
	{
		/// <summary>
		/// Pointer to <c>GetPrimitiveArrayCritical</c> function.
		/// Returns the body of the primitive array.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JArrayLocalRef, JBoolean*, IntPtr>
			GetPrimitiveArrayCritical;
		/// <summary>
		/// Pointer to <c>ReleasePrimitiveArrayCritical</c> function.
		/// Informs the <c>VM</c> that the native code no longer needs access to array elements.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JArrayLocalRef, IntPtr, JReleaseMode, void>
			ReleasePrimitiveArrayCritical;
	}

	/// <summary>
	/// Unix function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Unix
	{
		/// <summary>
		/// Pointer to <c>GetPrimitiveArrayCritical</c> function.
		/// Returns the body of the primitive array.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JArrayLocalRef, JBoolean*, IntPtr>
			GetPrimitiveArrayCritical;
		/// <summary>
		/// Pointer to <c>ReleasePrimitiveArrayCritical</c> function.
		/// Informs the <c>VM</c> that the native code no longer needs access to array elements.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JArrayLocalRef, IntPtr, JReleaseMode, void>
			ReleasePrimitiveArrayCritical;
	}
}