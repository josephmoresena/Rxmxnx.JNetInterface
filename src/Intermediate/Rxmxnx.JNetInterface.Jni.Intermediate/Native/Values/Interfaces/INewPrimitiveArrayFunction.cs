namespace Rxmxnx.JNetInterface.Native.Values.Interfaces;

/// <summary>
/// Function pointer to create Java primitive array through JNI.
/// </summary>
internal interface INewPrimitiveArrayFunction
{
	/// <summary>
	/// Pointer to <c>New&lt;PrimitiveType&gt;Array</c> function.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	protected readonly unsafe struct NewPrimitiveArrayFunction
	{
		/// <summary>
		/// Function pointer for Windows Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, Int32, JArrayLocalRef> Windows;
		/// <summary>
		/// Function pointer for Unix-like Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, Int32, JArrayLocalRef> Unix;
	}
}