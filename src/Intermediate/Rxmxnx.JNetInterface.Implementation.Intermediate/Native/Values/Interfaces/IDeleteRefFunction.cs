namespace Rxmxnx.JNetInterface.Native.Values.Interfaces;

/// <summary>
/// Function pointer to delete object reference through JNI.
/// </summary>
public interface IDeleteRefFunction
{
	/// <summary>
	/// Function pointer to delete object local references through JNI.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	protected readonly unsafe struct DeleteRefFunction
	{
		/// <summary>
		/// Pointer to <c>DeleteLocalRef</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, void> Windows;
		/// <summary>
		/// Pointer to <c>DeleteLocalRef</c> function.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, void> Unix;
	}
}