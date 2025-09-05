namespace Rxmxnx.JNetInterface.Native.Values.Interfaces;

/// <summary>
/// Function pointer to Get the value of Java fields through JNI.
/// </summary>
internal interface IGetFieldFunction
{
	/// <summary>
	/// Pointer to <c>Get&lt;type&gt;Field</c> function.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	protected readonly struct GetFieldFunction
	{
		/// <summary>
		/// Function pointers for Windows Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly Windows Windows;
		/// <summary>
		/// Function pointers for Unix-like Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly Unix Unix;
	}

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
		/// <c>GetBooleanField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JFieldId, Byte> Boolean;
		/// <summary>
		/// <c>GetByteField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JFieldId, SByte> Byte;
		/// <summary>
		/// <c>GetCharField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JFieldId, UInt16> Char;
		/// <summary>
		/// <c>GetDoubleField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JFieldId, Double> Double;
		/// <summary>
		/// <c>GetFloatField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JFieldId, Single> Float;
		/// <summary>
		/// <c>GetIntField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JFieldId, Int32> Int;
		/// <summary>
		/// <c>GetLongField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JFieldId, Int64> Long;
		/// <summary>
		/// <c>GetShortField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JFieldId, Int16> Short;
		/// <summary>
		/// <c>GetObjectField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, IntPtr, JFieldId, JObjectLocalRef> Object;
	}

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
		/// <c>GetBooleanField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JFieldId, Byte> Boolean;
		/// <summary>
		/// <c>GetByteField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JFieldId, SByte> Byte;
		/// <summary>
		/// <c>GetCharField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JFieldId, UInt16> Char;
		/// <summary>
		/// <c>GetDoubleField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JFieldId, Double> Double;
		/// <summary>
		/// <c>GetFloatField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JFieldId, Single> Float;
		/// <summary>
		/// <c>GetIntField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JFieldId, Int32> Int;
		/// <summary>
		/// <c>GetLongField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JFieldId, Int64> Long;
		/// <summary>
		/// <c>GetShortField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JFieldId, Int16> Short;
		/// <summary>
		/// <c>GetObjectField</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, IntPtr, JFieldId, JObjectLocalRef> Object;
	}
}