namespace Rxmxnx.JNetInterface.Native.Values.Interfaces;

/// <summary>
/// Function pointer to Set the value of Java fields through JNI.
/// </summary>
internal interface ISetFieldFunction
{
	/// <summary>
	/// Pointer to <c>Set&lt;type&gt;Field</c> function.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	protected readonly struct SetFieldFunction
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
		/// <c>SetBooleanField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JFieldId, Byte, void> Boolean;
		/// <summary>
		/// <c>SetByteField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JFieldId, SByte, void> Byte;
		/// <summary>
		/// <c>SetCharField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JFieldId, UInt16, void> Char;
		/// <summary>
		/// <c>SetDoubleField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JFieldId, Double, void> Double;
		/// <summary>
		/// <c>SetFloatField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JFieldId, Single, void> Float;
		/// <summary>
		/// <c>SetIntField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JFieldId, Int32, void> Int;
		/// <summary>
		/// <c>SetLongField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JFieldId, Int64, void> Long;
		/// <summary>
		/// <c>SetShortField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JFieldId, Int16, void> Short;
		/// <summary>
		/// <c>SetObjectField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JFieldId, JObjectLocalRef, void> Object;
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
		/// <c>SetBooleanField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JFieldId, Byte, void> Boolean;
		/// <summary>
		/// <c>SetByteField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JFieldId, SByte, void> Byte;
		/// <summary>
		/// <c>SetCharField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JFieldId, UInt16, void> Char;
		/// <summary>
		/// <c>SetDoubleField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JFieldId, Double, void> Double;
		/// <summary>
		/// <c>SetFloatField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JFieldId, Single, void> Float;
		/// <summary>
		/// <c>SetIntField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JFieldId, Int32, void> Int;
		/// <summary>
		/// <c>SetLongField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JFieldId, Int64, void> Long;
		/// <summary>
		/// <c>SetShortField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JFieldId, Int16, void> Short;
		/// <summary>
		/// <c>SetObjectField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JFieldId, JObjectLocalRef, void> Object;
	}
}