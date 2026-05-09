namespace Rxmxnx.JNetInterface.Native.Values.Interfaces;

/// <summary>
/// Set of function pointers to call Java methods through JNI.
/// </summary>
internal interface ICallMethodFunction
{
	/// <summary>
	/// Caller <c>A</c> function.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	protected readonly struct CallMethodFunction
	{
#if !ANDROID
		/// <summary>
		/// Function pointers for Windows Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly Windows Windows;
#endif
		/// <summary>
		/// Function pointers for Unix-like Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly Unix Unix;
	}

#if !ANDROID
	/// <summary>
	/// Windows function set.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	protected readonly unsafe struct Windows
	{
		/// <summary>
		/// Caller <c>CallBooleanMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JMethodId, JValue*, Byte> Boolean;
		/// <summary>
		/// Caller <c>CallByteMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JMethodId, JValue*, SByte> Byte;
		/// <summary>
		/// Caller <c>CallCharMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JMethodId, JValue*, UInt16> Char;
		/// <summary>
		/// Caller <c>CallDoubleMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JMethodId, JValue*, Double> Double;
		/// <summary>
		/// Caller <c>CallFloatMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JMethodId, JValue*, Single> Float;
		/// <summary>
		/// Caller <c>CallIntMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JMethodId, JValue*, Int32> Int;
		/// <summary>
		/// Caller <c>CallLongMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JMethodId, JValue*, Int64> Long;
		/// <summary>
		/// Caller <c>CallShortMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JMethodId, JValue*, Int16> Short;
		/// <summary>
		/// Caller <c>CallVoidMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JMethodId, JValue*, void> Void;
		/// <summary>
		/// Caller <c>CallObjectMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JMethodId, JValue*, JObjectLocalRef>
			Object;
	}
#endif

	/// <summary>
	/// Unix function set.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	protected readonly unsafe struct Unix
	{
		/// <summary>
		/// Caller <c>CallBooleanMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JMethodId, JValue*, Byte> Boolean;
		/// <summary>
		/// Caller <c>CallByteMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JMethodId, JValue*, SByte> Byte;
		/// <summary>
		/// Caller <c>CallCharMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JMethodId, JValue*, UInt16> Char;
		/// <summary>
		/// Caller <c>CallDoubleMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JMethodId, JValue*, Double> Double;
		/// <summary>
		/// Caller <c>CallFloatMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JMethodId, JValue*, Single> Float;
		/// <summary>
		/// Caller <c>CallIntMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JMethodId, JValue*, Int32> Int;
		/// <summary>
		/// Caller <c>CallLongMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JMethodId, JValue*, Int64> Long;
		/// <summary>
		/// Caller <c>CallShortMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JMethodId, JValue*, Int16> Short;
		/// <summary>
		/// Caller <c>CallVoidMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JMethodId, JValue*, void> Void;
		/// <summary>
		/// Caller <c>CallObjectMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JMethodId, JValue*, JObjectLocalRef> Object;
	}
}