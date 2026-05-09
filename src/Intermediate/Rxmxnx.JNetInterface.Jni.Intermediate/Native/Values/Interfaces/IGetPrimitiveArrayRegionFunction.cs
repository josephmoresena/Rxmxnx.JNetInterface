namespace Rxmxnx.JNetInterface.Native.Values.Interfaces;

/// <summary>
/// Function pointer to copy values from a Java primitive array through JNI.
/// </summary>
internal interface IGetPrimitiveArrayRegionFunction
{
	/// <summary>
	/// Pointer to <c>Get&lt;PrimitiveType&gt;ArrayRegion</c> function.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	protected readonly unsafe struct GetPrimitiveArrayRegionFunction
	{
#if !ANDROID
		/// <summary>
		/// Function pointer for Windows Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JArrayLocalRef, Int32, Int32, void*, void>
			Windows;
#endif
		/// <summary>
		/// Function pointer for Unix-like Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JArrayLocalRef, Int32, Int32, void*, void> Unix;
	}
}