namespace Rxmxnx.JNetInterface.Native.Values.Functions;

internal readonly partial struct ClassFunctionSet
{
	/// <summary>
	/// Pointer to <c>FindClass</c> function.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	private readonly unsafe struct FindClassPtr
	{
#if !ANDROID
		/// <summary>
		/// Function pointer for Windows Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, Byte*, JClassLocalRef> Windows;
#endif
		/// <summary>
		/// Function pointer for Unix-like Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, Byte*, JClassLocalRef> Unix;
	}
}