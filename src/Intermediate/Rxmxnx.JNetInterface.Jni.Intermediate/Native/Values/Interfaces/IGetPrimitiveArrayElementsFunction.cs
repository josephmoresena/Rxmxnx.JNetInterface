namespace Rxmxnx.JNetInterface.Native.Values.Interfaces;

/// <summary>
/// Function pointer to get a pointer to elements Java primitive array through JNI.
/// </summary>
internal interface IGetPrimitiveArrayElementsFunction
{
	/// <summary>
	/// Pointer to <c>Get&lt;PrimitiveType&gt;Elements</c> function.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	protected readonly unsafe struct GetPrimitiveArrayElementsFunction
	{
#if !ANDROID
		/// <summary>
		/// Function pointer for Windows Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JArrayLocalRef, JBoolean*, void*> Windows;
#endif
		/// <summary>
		/// Function pointer for Unix-like Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JArrayLocalRef, JBoolean*, void*> Unix;
	}
}