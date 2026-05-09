namespace Rxmxnx.JNetInterface.Native.Values.Interfaces;

/// <summary>
/// Function pointer to release the pointer to elements Java primitive array through JNI.
/// </summary>
internal interface IReleaseArrayElementsFunction
{
	/// <summary>
	/// Pointer to <c>Release&lt;PrimitiveType&gt;Elements</c> function.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	protected readonly unsafe struct ReleaseArrayElementsFunction
	{
#if !ANDROID
		/// <summary>
		/// Function pointer for Windows Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JArrayLocalRef, void*, JReleaseMode, void>
			Windows;
#endif
		/// <summary>
		/// Function pointer for Unix-like Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JArrayLocalRef, void*, JReleaseMode, void> Unix;
	}
}