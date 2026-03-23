namespace Rxmxnx.JNetInterface.Native.Values;

internal partial struct NativeInterface
{
	/// <summary>
	/// Pointer to <c>GetJavaVM</c> function.
	/// Returns the Java VM interface (used in the Invocation API) associated with the current thread.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	private readonly unsafe struct GetVirtualMachinePtr
	{
		/// <summary>
		/// Function pointer for Windows Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JVirtualMachineRef*, JResult> Windows;
		/// <summary>
		/// Function pointer for Unix-like Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JVirtualMachineRef*, JResult> Unix;
	}
}