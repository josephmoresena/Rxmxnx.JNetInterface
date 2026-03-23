namespace Rxmxnx.JNetInterface.Native.Values.Interfaces;

/// <summary>
/// Function pointer to copy values to a Java primitive array through JNI.
/// </summary>
internal interface ISetPrimitiveArrayRegionFunction
{
	/// <summary>
	/// Pointer to <c>Set&lt;PrimitiveType&gt;ArrayRegion</c> function.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	protected readonly unsafe struct SetPrimitiveArrayRegionFunction
	{
		/// <summary>
		/// Function pointer for Windows Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JArrayLocalRef, Int32, Int32, void*, void>
			Windows;
		/// <summary>
		/// Function pointer for Unix-like Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JArrayLocalRef, Int32, Int32, void*, void> Unix;
	}
}