namespace Rxmxnx.JNetInterface.Native.Values.Interfaces;

/// <summary>
/// Function pointer to create new object reference through JNI.
/// </summary>
internal interface INewRefFunction
{
	/// <summary>
	/// Function pointer to create new object local reference through JNI.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	protected readonly unsafe struct NewRefFunction
	{
		/// <summary>
		/// Pointer to <c>NewLocalRef</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, IntPtr> Windows;
		/// <summary>
		/// Pointer to <c>NewLocalRef</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, IntPtr> Unix;
	}
}