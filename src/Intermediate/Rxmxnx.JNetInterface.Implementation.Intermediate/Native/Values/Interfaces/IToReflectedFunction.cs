namespace Rxmxnx.JNetInterface.Native.Values.Interfaces;

/// <summary>
/// Function pointer to convert an accessible identifier to Java accessible object through JNI.
/// </summary>
internal interface IToReflectedFunction
{
	/// <summary>
	/// Pointer to <c>ToReflected&lt;Accessible&gt;</c> function.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	protected readonly unsafe struct ToReflectedFunction
	{
		/// <summary>
		/// Function pointer for Windows Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JClassLocalRef, IntPtr, JBoolean, JObjectLocalRef>
			Windows;
		/// <summary>
		/// Function pointer for Unix-like Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JClassLocalRef, IntPtr, JBoolean, JObjectLocalRef>
			Unix;
	}
}