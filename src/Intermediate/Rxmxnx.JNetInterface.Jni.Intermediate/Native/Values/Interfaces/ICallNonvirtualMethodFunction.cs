namespace Rxmxnx.JNetInterface.Native.Values.Interfaces;

/// <summary>
/// Set of function pointers to call Java non-virtual methods through JNI.
/// </summary>
internal interface ICallNonvirtualMethodFunction
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
		/// Caller <c>CallNonvirtualBooleanMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue
			*, Byte> Boolean;
		/// <summary>
		/// Caller <c>CallNonvirtualByteMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue
			*, SByte> Byte;
		/// <summary>
		/// Caller <c>CallNonvirtualCharMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue
			*, UInt16> Char;
		/// <summary>
		/// Caller <c>CallNonvirtualDoubleMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue
			*, Double> Double;
		/// <summary>
		/// Caller <c>CallNonvirtualFloatMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue
			*, Single> Float;
		/// <summary>
		/// Caller <c>CallNonvirtualIntMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue
			*, Int32> Int;
		/// <summary>
		/// Caller <c>CallNonvirtualLongMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue
			*, Int64> Long;
		/// <summary>
		/// Caller <c>CallNonvirtualShortMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue
			*, Int16> Short;
		/// <summary>
		/// Caller <c>CallNonvirtualVoidMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue
			*, void> Void;
		/// <summary>
		/// Caller <c>CallNonvirtualObjectMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue
			*, JObjectLocalRef> Object;
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
		/// Caller <c>CallNonvirtualBooleanMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*,
			Byte> Boolean;
		/// <summary>
		/// Caller <c>CallNonvirtualByteMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*,
			SByte> Byte;
		/// <summary>
		/// Caller <c>CallNonvirtualCharMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*,
			UInt16> Char;
		/// <summary>
		/// Caller <c>CallNonvirtualDoubleMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*,
			Double> Double;
		/// <summary>
		/// Caller <c>CallNonvirtualFloatMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*,
			Single> Float;
		/// <summary>
		/// Caller <c>CallNonvirtualIntMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*,
			Int32> Int;
		/// <summary>
		/// Caller <c>CallNonvirtualLongMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*,
			Int64> Long;
		/// <summary>
		/// Caller <c>CallNonvirtualShortMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*,
			Int16> Short;
		/// <summary>
		/// Caller <c>CallNonvirtualVoidMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*,
			void> Void;
		/// <summary>
		/// Caller <c>CallNonvirtualObjectMethodA</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*,
			JObjectLocalRef> Object;
	}
}