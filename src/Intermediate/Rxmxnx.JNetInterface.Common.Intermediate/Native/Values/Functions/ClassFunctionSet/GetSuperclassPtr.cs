namespace Rxmxnx.JNetInterface.Native.Values.Functions;

internal readonly partial struct ClassFunctionSet
{
	/// <summary>
	/// Pointer to <c>GetSuperclass</c> function.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	private readonly unsafe struct GetSuperclassPtr
	{
		/// <summary>
		/// Function pointer for Windows Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JClassLocalRef, JClassLocalRef> Windows;
		/// <summary>
		/// Function pointer for Unix-like Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JClassLocalRef, JClassLocalRef> Unix;
	}
}