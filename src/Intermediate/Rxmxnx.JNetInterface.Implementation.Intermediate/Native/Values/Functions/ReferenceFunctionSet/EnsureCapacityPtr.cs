namespace Rxmxnx.JNetInterface.Native.Values.Functions;

internal readonly partial struct ReferenceFunctionSet
{
	/// <summary>
	/// Pointer to <c>EnsureLocalCapacity</c> function.
	/// Ensures that at least a given number of local references can be created in the current thread.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	private readonly unsafe struct EnsureLocalCapacityPtr
	{
		/// <summary>
		/// Function pointer for Windows Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, Int32, JResult> Windows;
		/// <summary>
		/// Function pointer for Unix-like Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, Int32, JResult> Unix;
	}
}