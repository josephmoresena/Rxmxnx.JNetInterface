// ReSharper disable ConvertIfStatementToReturnStatement

namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate Java object array through JNI.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct ObjectArrayFunctionSet
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
	/// <c>NewObjectArray</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JObjectArrayLocalRef NewObjectArray(JEnvironmentRef envRef, Int32 length, JClassLocalRef classRef,
		JObjectLocalRef localRef)
	{
#if !ANDROID
		if (SystemInfo.IsWindows)
			return this._windows.NewObjectArray(envRef, length, classRef, localRef);
#endif
		return this._unix.NewObjectArray(envRef, length, classRef, localRef);
	}
	/// <summary>
	/// <c>GetObjectArrayElement</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JObjectLocalRef GetObjectArrayElement(JEnvironmentRef envRef, JObjectArrayLocalRef arrayRef, Int32 index)
	{
#if !ANDROID
		if (SystemInfo.IsWindows)
			return this._windows.GetObjectArrayElement(envRef, arrayRef, index);
#endif
		return this._unix.GetObjectArrayElement(envRef, arrayRef, index);
	}
	/// <summary>
	/// <c>SetObjectArrayElement</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void SetObjectArrayElement(JEnvironmentRef envRef, JObjectArrayLocalRef arrayRef, Int32 index,
		JObjectLocalRef localRef)
	{
#if !ANDROID
		if (SystemInfo.IsWindows)
		{
			this._windows.SetObjectArrayElement(envRef, arrayRef, index, localRef);
			return;
		}
#endif
		this._unix.SetObjectArrayElement(envRef, arrayRef, index, localRef);
	}

#if !ANDROID
	/// <summary>
	/// Windows function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Windows
	{
		/// <summary>
		/// Pointer to <c>NewObjectArray</c> function.
		/// Constructs a new array holding objects in given class.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, Int32, JClassLocalRef, JObjectLocalRef,
			JObjectArrayLocalRef> NewObjectArray;
		/// <summary>
		/// Pointer to <c>GetObjectArrayElement</c> function.
		/// Returns an element of an <c>Object</c> array.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectArrayLocalRef, Int32, JObjectLocalRef>
			GetObjectArrayElement;
		/// <summary>
		/// Pointer to <c>SetObjectArrayElement</c> function.
		/// Sets an element of an <c>Object</c> array.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectArrayLocalRef, Int32, JObjectLocalRef, void
			> SetObjectArrayElement;
	}
#endif

	/// <summary>
	/// Unix function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Unix
	{
		/// <summary>
		/// Pointer to <c>NewObjectArray</c> function.
		/// Constructs a new array holding objects in given class.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, Int32, JClassLocalRef, JObjectLocalRef,
			JObjectArrayLocalRef> NewObjectArray;
		/// <summary>
		/// Pointer to <c>GetObjectArrayElement</c> function.
		/// Returns an element of an <c>Object</c> array.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectArrayLocalRef, Int32, JObjectLocalRef>
			GetObjectArrayElement;
		/// <summary>
		/// Pointer to <c>SetObjectArrayElement</c> function.
		/// Sets an element of an <c>Object</c> array.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectArrayLocalRef, Int32, JObjectLocalRef, void>
			SetObjectArrayElement;
	}
}