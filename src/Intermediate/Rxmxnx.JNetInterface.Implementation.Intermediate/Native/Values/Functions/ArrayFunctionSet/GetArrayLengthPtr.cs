namespace Rxmxnx.JNetInterface.Native.Values.Functions;

internal readonly partial struct ArrayFunctionSet
{
	/// <summary>
	/// Pointer to <c>GetArrayLength</c> function.
	/// Returns the number of elements in the array.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	private readonly unsafe struct GetArrayLengthPtr
	{
		/// <summary>
		/// Function pointer for Windows Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JArrayLocalRef, Int32> Windows;
		/// <summary>
		/// Function pointer for Unix-like Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JArrayLocalRef, Int32> Unix;
	}
}