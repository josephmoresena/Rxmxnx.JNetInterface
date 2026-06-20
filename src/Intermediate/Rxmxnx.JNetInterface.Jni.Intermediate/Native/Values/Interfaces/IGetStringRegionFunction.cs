namespace Rxmxnx.JNetInterface.Native.Values.Interfaces;

/// <summary>
/// Function pointer to copy chars from a Java string object through JNI.
/// </summary>
internal interface IGetStringRegionFunction
{
	/// <summary>
	/// Pointer to <c>GetStringRegionFunction</c> function.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	protected readonly unsafe struct GetStringRegionFunction
	{
#if !ANDROID
		/// <summary>
		/// Function pointer for Windows Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JStringLocalRef, Int32, Int32, void*, void>
			Windows;
#endif
		/// <summary>
		/// Function pointer for Unix-like Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JStringLocalRef, Int32, Int32, void*, void> Unix;
	}
}